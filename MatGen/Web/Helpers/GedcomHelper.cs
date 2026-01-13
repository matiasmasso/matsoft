using DocumentFormat.OpenXml.Wordprocessing;
using DTO;
using DTO.Gedcom;
using Microsoft.JSInterop;
using SixLabors.Fonts.Unicode;
using System.Net.WebSockets;
using System.Text;
using Web.Components;
using static DTO.UserModel;

namespace Web.Helpers
{
    public class GedcomHelper
    {

        private Services.ApiService apiService;
        private PersonModel root;
        private DTO.Gedcom.File file;
        private int PrivacyMaxBirthDate = 1840;
        private int PrivacyMaxDocDate = 1899;
        public GedcomHelper(Services.ApiService apiService, PersonModel root)
        {
            this.apiService = apiService;
            this.root = root;
            this.file = new DTO.Gedcom.File();
        }

        public byte[] Bytes()
        {
            file = new DTO.Gedcom.File();
            var ancestors = root.Ancestors(apiService.Persons());
            foreach (var ancestor in ancestors)
            {
                SetEnlaces(ancestor.Person);
            }

            return file.Bytes();
        }

        private void SetEnlaces(PersonModel person)
        {
            FindOrCreateIndi(person);
            var enlaces = apiService.Enlaces(person);
            foreach (var enlace in enlaces ?? new List<EnlaceModel>())
            {
                if (IsMissingFam(enlace))
                {
                    var conjuge = apiService.Person(enlace.Conjuge(person));
                    if (conjuge != null) FindOrCreateIndi(conjuge);

                    var fam = CreateFam(enlace);
                    foreach (var child in apiService.Children(enlace) ?? new())
                    {
                        var indi = FindOrCreateIndi(child);
                        fam.Children.Add(indi);
                        indi.ChildOf.Add(fam);
                        SetEnlaces(child);
                    }

                    var marit = person.Sex == PersonModel.Sexs.Male ? person : conjuge;
                    if (marit?.FchLocationFrom?.Fch?.Fch1?.CompareTo(PrivacyMaxBirthDate.ToString()) < 0)
                    {
                        var muller = person.Sex == PersonModel.Sexs.Female ? person : conjuge;
                        var conjuges = new List<PersonModel?> { marit, muller }.ToList();

                        var nuviRel = DocRelModel.Wellknown(DocRelModel.Wellknowns.Nuvi);
                        var nuviaRel = DocRelModel.Wellknown(DocRelModel.Wellknowns.Nuvia);
                        var rels = new List<DocRelModel> { nuviRel!, nuviaRel! }.ToList();

                        var nuviDocs = apiService.DocTargets()?
                            .Where(x => x.Target == marit?.Guid && x.Rel == nuviRel?.Guid)
                            .Select(x => x.Doc)
                            .ToList();

                        var nuviaDocs = apiService.DocTargets()?
                            .Where(x => x.Target == muller?.Guid && x.Rel == nuviaRel?.Guid)
                            .Select(x => x.Doc)
                            .ToList();

                        var marrDocs = nuviDocs?.Where(x => nuviaDocs?.Contains(x) ?? false).ToList();
                        var citations = Citations(marrDocs ?? new());
                        fam.Citations.AddRange(citations);
                    }

                }
            }

        }



        private Indi FindOrCreateIndi(PersonModel person)
        {
            Indi? retval = FindIndi(person.Guid);
            if (retval == null) retval = CreateIndi(person);
            return retval;
        }
        private Indi? FindIndi(Guid? guid) => file.Indis.FirstOrDefault(x => x.Guid == guid);
        private Indi CreateIndi(PersonModel person)
        {
            var mare = apiService.Person(person.Mare);
            var cognomPatern = apiService.Cognom(person.Cognom)?.Nom;
            var cognomMatern = apiService.Cognom(mare?.Cognom)?.Nom;
            var surnames = SurnamesBuilder(cognomPatern, cognomMatern);
            //var docTargets = apiService.DocTargets(person) ?? new();

            var indi = file.AddIndi(person.Guid);
            indi.Givn = apiService.Firstnom(person.Firstnom)?.Nom;
            indi.Surn = surnames;
            indi.Name = NameBuilder(indi.Givn, surnames);

            indi.BirthDate = Fch(person.FchLocationFrom);
            indi.DeathDate = Fch(person.FchLocationTo);
            indi.BurialDate = Fch(person.FchLocationSepultura);
            //indi.BirthDate = Fch(person.FchFrom, person.LocationFrom);
            //indi.DeathDate = Fch(person.FchTo, person.LocationTo);
            //indi.BurialDate = Fch(person.FchSepultura, person.LocationSepultura);
            indi.Sex = (Indi.Sexs)person.Sex;
            indi.Occupation = apiService.Profession(person.Profession)?.Nom;

            var batejat = DocRelModel.Wellknown(DocRelModel.Wellknowns.Batejat)!;
            var batejada = DocRelModel.Wellknown(DocRelModel.Wellknowns.Batejada)!;
            var nuvi = DocRelModel.Wellknown(DocRelModel.Wellknowns.Nuvi)!;
            var nuvia = DocRelModel.Wellknown(DocRelModel.Wellknowns.Nuvia)!;
            var difunt = DocRelModel.Wellknown(DocRelModel.Wellknowns.Difunt)!;

            if (person?.FchLocationFrom?.Fch?.Fch1?.CompareTo(PrivacyMaxBirthDate.ToString()) < 0)
            {
                var docTargets = apiService.DocTargets(person) ?? new();
                foreach (var docTarget in docTargets)
                {
                    if (docTarget.Rel != nuvi.Guid && docTarget.Rel != nuvia.Guid)
                    {
                        if (docTarget.Rel == batejat.Guid)
                            indi.BirthDate?.Citations.Add(Citation(docTarget)!);
                        else if (docTarget.Rel == batejada.Guid)
                            indi.BirthDate?.Citations.Add(Citation(docTarget)!);
                        else if (docTarget.Rel == difunt.Guid)
                            indi.DeathDate?.Citations.Add(Citation(docTarget)!);
                        else
                        {

                            var doc = apiService.Doc(docTarget.Doc);
                            if (doc?.Fch?.CompareTo(PrivacyMaxDocDate.ToString()) < 0)
                            {
                                var cod = apiService.DocCod(doc?.Cod);
                                var evento = new Event(cod?.Nom ?? "Custom");
                                evento.Fch = Fch(new FchLocationModel(null, doc?.Fch));
                                evento.Citations.Add(Citation(docTarget)!);
                                indi.Events.Add(evento);
                            }
                        }
                    }
                }
            }
            return indi;
        }

        private List<Citation> Citations(List<Guid?> docGuids)
        {
            List<Citation> retval = new();
            foreach (var docGuid in docGuids)
            {
                var citation = Citation(docGuid);
                if (citation != null) retval.Add(citation);
            }
            return retval;
        }

        private Citation? Citation(DocTargetModel docTarget)
        {
            Citation retval = Citation(docTarget.Doc)!;
            retval.Role = apiService.DocRel(docTarget?.Rel)?.Nom;
            return retval;
        }

        private Citation? Citation(Guid? docGuid)
        {
            Citation? retval = null;
            if (docGuid != null)
            {
                var doc = apiService.Doc(docGuid);
                var docSrc = apiService.DocSrc(doc?.Src);
                if (docSrc == null) docSrc = new DocSrcModel();

                var sour = file.Sours.FirstOrDefault(x => x.Guid == docSrc.Guid);
                if (sour == null)
                {
                    sour = file.AddSour(docSrc.Guid);
                    sour.Titl = docSrc.NomLlarg;
                }

                if (docSrc.Repo != null)
                {
                    var apiRepo = apiService.Repo(docSrc.Repo);
                    var repo = file.Repos.FirstOrDefault(x => x.Guid == docSrc.Repo);
                    if (repo == null && apiRepo != null)
                    {
                        repo = file.AddRepo((Guid)docSrc.Repo);
                        repo.Name = apiRepo.Nom;
                        repo.Adr1 = apiRepo.Adr;
                        repo.City = apiRepo.Location;
                        repo.Post = apiRepo.Zip;
                        //repo.Stae
                        repo.Ctry = apiRepo.Country;
                    }
                    sour.Repo = repo?.Id;
                }

                retval = new Citation();
                retval.Sour = sour;
                retval.Page = doc?.SrcDetail;
                retval.Title = doc?.Tit;
                if (string.IsNullOrEmpty(doc?.ExternalUrl))
                    retval.Url = doc?.DownloadUrl();
                else
                    retval.Url = doc?.ExternalUrl;
            }
            return retval;
        }

        private bool IsMissingFam(EnlaceModel enlace)
        {
            var husb = FindIndi(enlace.Marit);
            var wife = FindIndi(enlace.Muller);
            return file.IsMissingFam(husb, wife);
        }

        private Fam CreateFam(EnlaceModel enlace)
        {
            var husb = FindIndi(enlace.Marit);
            var wife = FindIndi(enlace.Muller);
            var retval = file.AddFam(husb, wife, enlace.NupciesMarit, enlace.NupciesMuller);
            retval.Fch = Fch(enlace.FchLocation);
            return retval;
        }

        private DTO.Gedcom.Repo FindOrCreateRepo(RepoModel repo)
        {
            DTO.Gedcom.Repo? retval = FindRepo(repo.Guid);
            if (retval == null) retval = CreateRepo(repo);
            return retval;
        }
        private DTO.Gedcom.Repo? FindRepo(Guid? guid) => file.Repos.FirstOrDefault(x => x.Guid == guid);
        private DTO.Gedcom.Repo CreateRepo(RepoModel repo)
        {
            var retval = file.AddRepo(repo.Guid);
            retval.Name = repo.Nom;
            retval.Adr1 = repo.Adr;
            retval.City = repo.Location;
            retval.Post = repo.Zip;
            //retval.Stae = repo.
            retval.Ctry = repo.Country;
            return retval;
        }


        public DTO.Gedcom.Fch? Fch(FchLocationModel? args)
        {
            DTO.Gedcom.Fch? retval = null;
            var src = args?.Fch?.Fch1;
            var locationGuid = args?.Location?.Guid;
            try
            {
                if (src?.Length >= 4 && Char.IsDigit(src, 0) && Char.IsDigit(src, 1) && Char.IsDigit(src, 2))
                {
                    retval = new DTO.Gedcom.Fch();
                    retval.Modifier = DTO.Gedcom.Fch.Modifiers.approximate;
                    int year, month = 1, day = 1;
                    var sYear = src.Substring(0, 4).Replace("X", "5");
                    year = Convert.ToInt16(sYear);
                    retval.Value = new DateTime(year, 6, 30);
                    retval.DisplayValue = $"ABT {year}";

                    if (Char.IsDigit(src, 3) && src.Length >= 6)
                    {
                        var sMonth = src.Substring(4, 2);
                        if (!sMonth.Contains('X'))
                        {
                            month = Convert.ToInt16(sMonth);
                            var tmp = new DateTime(year, month, 1);
                            retval.DisplayValue = $"ABT {tmp:MMM} {year}";
                            if (src.Length >= 8)
                            {
                                var sDay = src.Substring(6, 2);
                                if (sDay.Contains('X'))
                                {
                                    retval.Value = new DateTime(year, month, 15);
                                    retval.DisplayValue = $"ABT {retval.Value:MMM yyyy}";
                                }
                                else
                                {
                                    try
                                    {
                                        retval.Modifier = DTO.Gedcom.Fch.Modifiers.exact;
                                        day = Convert.ToInt16(sDay);
                                        retval.Value = new DateTime(year, month, day);
                                        retval.DisplayValue = $"{retval.Value:d MMM yyyy}";
                                    }
                                    catch (Exception ex)
                                    {
                                        retval = null;
                                    }
                                }
                            }
                        }
                    }
                }

                if (locationGuid != null)
                {
                    if (retval == null) retval = new DTO.Gedcom.Fch();
                    retval.Location = apiService.Location(locationGuid)?.NomLlarg;
                }
            }
            catch (Exception ex)
            {

            }
            return retval;
        }


        //public DTO.Gedcom.Fch? Fch(string? src, Guid? locationGuid = null)
        //{
        //    DTO.Gedcom.Fch? retval = null;
        //    try
        //    {
        //        if (src?.Length >= 4 && Char.IsDigit(src, 0) && Char.IsDigit(src, 1) && Char.IsDigit(src, 2))
        //        {
        //            retval = new DTO.Gedcom.Fch();
        //            retval.Modifier = DTO.Gedcom.Fch.Modifiers.approximate;
        //            int year, month = 1, day = 1;
        //            var sYear = src.Substring(0, 4).Replace("X", "5");
        //            year = Convert.ToInt16(sYear);
        //            retval.Value = new DateTime(year, 6, 30);
        //            retval.DisplayValue = $"ABT {year}";

        //            if (Char.IsDigit(src, 3) && src.Length >= 6)
        //            {
        //                var sMonth = src.Substring(4, 2);
        //                if (!sMonth.Contains('X'))
        //                {
        //                    month = Convert.ToInt16(sMonth);
        //                    var tmp = new DateTime(year, month, 1);
        //                    retval.DisplayValue = $"ABT {tmp:MMM} {year}";
        //                    if (src.Length >= 8)
        //                    {
        //                        var sDay = src.Substring(6, 2);
        //                        if (sDay.Contains('X'))
        //                        {
        //                            retval.Value = new DateTime(year, month, 15);
        //                            retval.DisplayValue = $"ABT {retval.Value:MMM yyyy}";
        //                        }
        //                        else
        //                        {
        //                            try
        //                            {
        //                                retval.Modifier = DTO.Gedcom.Fch.Modifiers.exact;
        //                                day = Convert.ToInt16(sDay);
        //                                retval.Value = new DateTime(year, month, day);
        //                                retval.DisplayValue = $"{retval.Value:d MMM yyyy}";
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                retval = null;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (locationGuid != null)
        //        {
        //            if (retval == null) retval = new DTO.Gedcom.Fch();
        //            retval.Location = apiService.Location(locationGuid)?.NomLlarg;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return retval;
        //}

        public static string? NameBuilder(string? givenname, string? surnames)
        {
            string? retval = surnames == null ? givenname : $"{givenname} /{surnames}/";
            return retval?.Trim();
        }

        public static string? SurnamesBuilder(string? fathersurname, string? mothersurname)
        {
            string? retval = null;
            if (fathersurname != null && mothersurname != null)
                retval = $"{fathersurname} {mothersurname}";
            else
                retval = fathersurname == null ? mothersurname : fathersurname;
            return retval?.Trim();
        }

    }
}
