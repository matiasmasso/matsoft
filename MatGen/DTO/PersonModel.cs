using DTO.Gedcom;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static DTO.MenuModel;

namespace DTO
{
    public class PersonModel : BaseGuid, IModel
    {
        public string? Nom { get; set; }
        public Sexs Sex { get; set; }
        public Guid? Pare { get; set; }
        public Guid? Mare { get; set; }
        public FchLocationModel? FchLocationFrom { get; set; }
        public FchLocationModel? FchLocationTo { get; set; }
        public FchLocationModel? FchLocationSepultura { get; set; }

        public static readonly int AverageYearsFromParentToChild = 30;

        //DEPRECATED===================================
        //public string? FchFrom { get; set; }
        //public Guid? LocationFrom { get; set; }
        //public string? FchTo { get; set; }
        //public Guid? LocationTo { get; set; }
        //public string? FchSepultura { get; set; }
        //public Guid? LocationSepultura { get; set; }
        //=============================================

        public Guid? Firstnom { get; set; }
        public Guid? Cognom { get; set; }
        public Guid? Profession { get; set; }
        public string? Obs { get; set; }
        public DateTime? FchCreated { get; set; } = DateTime.Now;
        public DateTime? FchLastEdited { get; set; }
        public enum Sexs
        {
            NotSet,
            Male,
            Female
        }

        public PersonModel() : base() { }
        public PersonModel(Guid guid) : base(guid) { }
        public static PersonModel Factory() => new PersonModel { Nom = "(nova persona)" };

        public static string CollectionPageUrl() => Globals.PageUrl("persons");
        public string PropertyPageUrl() => Globals.PageUrl("person", Guid.ToString());
        public string CreatePageUrl() => Globals.PageUrl("person");
        public static string CreatePageUrl(FirstnomModel firstnom) => Globals.PageUrl("person/fromFirstnom", firstnom.Guid.ToString());
        public static string CreatePageUrl(CognomModel? cognom) => Globals.PageUrl(cognom == null ? "person" : string.Format("person/fromCognom/{0}", cognom.Guid.ToString()));
        public static string CreatePageUrl(ProfessionModel profession) => Globals.PageUrl("person/fromProfession", profession.Guid.ToString());

        public string? YearFrom => FchLocationFrom?.Year;
        public string? YearTo => FchLocationTo?.Year;


        public List<PersonModel>? PareCandidates(List<PersonModel>? allPersons)
        {
            List<PersonModel>? retval = null;
            if (allPersons != null)
            {
                retval = allPersons.Where(x => x.Sex == PersonModel.Sexs.Male).ToList(); // get all males
                retval.Remove(this); // self cannot be his father
                var descendants = Descendants();
                retval.RemoveAll(x => descendants.Any(y => x.Guid == y.Guid)); //descendants cannot be the father
            }
            return retval;
        }
        public List<PersonModel>? MareCandidates(List<PersonModel>? allPersons)
        {
            List<PersonModel>? retval = null;
            if (allPersons != null)
            {
                retval = allPersons.Where(x => x.Sex == PersonModel.Sexs.Female).ToList(); // get all females
                retval.Remove(this); // self cannot be his mother
                var descendants = Descendants();
                retval.RemoveAll(x => descendants.Any(y => x.Guid == y.Guid)); //descendants cannot be the mother
            }
            return retval;
        }

        public List<AncestorModel> Ancestors(List<PersonModel>? allPersons) => PersonModel.Ancestors(this, 0, 1, allPersons);
        public static List<AncestorModel> Ancestors(PersonModel person, int level, int id, List<PersonModel>? allPersons)
        {
            var retval = new List<AncestorModel>();
            var ancestor = new AncestorModel(person, level, id)
            {
                Pare = person.Pare == null ? null : allPersons?.FirstOrDefault(x => x.Guid == person.Pare),
                Mare = person.Mare == null ? null : allPersons?.FirstOrDefault(x => x.Guid == person.Mare),
            };
            if (ancestor.Pare != null) retval.AddRange(Ancestors(ancestor.Pare, level + 1, id * 2, allPersons));
            if (ancestor.Mare != null) retval.AddRange(Ancestors(ancestor.Mare, level + 1, id * 2 + 1, allPersons));

            retval.Add(ancestor);
            retval = retval.OrderBy(x => x.Id).ToList();
            return retval;
        }
        public List<Guid> AncestorGuids(List<PersonModel>? allPersons) => AncestorGuids(this, allPersons);
        public static List<Guid> AncestorGuids(PersonModel? person, List<PersonModel>? allPersons)
        {
            var retval = new List<Guid>();
            if (person != null)
            {
                var pare = person.Pare == null ? null : allPersons?.FirstOrDefault(x => x.Guid == person.Pare);
                if (pare != null) retval.AddRange(AncestorGuids(pare, allPersons));
                var mare = person.Mare == null ? null : allPersons?.FirstOrDefault(x => x.Guid == person.Mare);
                if (mare != null) retval.AddRange(AncestorGuids(mare, allPersons));
                retval.Add(person.Guid);
            }
            return retval;
        }

        public List<PersonModel> Descendants() => PersonModel.Descendants(this, new List<PersonModel>());
        public static List<PersonModel> Descendants(PersonModel parent, List<PersonModel> allPersons)
        {
            var retval = new List<PersonModel>();
            var children = allPersons.Where(x => x.Pare == parent.Guid || x.Mare == parent.Guid)
                .OrderBy(x => x.FchLocationFrom?.Fch?.Fch1)
                .ToList();

            retval.AddRange(children);
            foreach (var child in children)
            {
                retval.AddRange(PersonModel.Descendants(child, allPersons));
            }
            return retval;
        }

        public ICollection<EnlaceModel>? Enlaces(ICollection<PersonModel>? allPersons, ICollection<EnlaceModel>? allEnlaces)
        {
            var retval = allEnlaces?.Where(x => x.Marit == Guid || x.Muller == Guid).ToList();

            if(allPersons != null)
            {
                retval?.AddRange(allPersons
                    .Where(x => (x.Pare == Guid || x.Mare == Guid) && !retval.Any(y => x.Pare == y.Marit && x.Mare == y.Muller))
                     .GroupBy(x => new { x.Pare, x.Mare })
                     .Select(x => new EnlaceModel
                     {
                         Marit = x.Key.Pare,
                         Muller = x.Key.Mare
                     }).ToList());
            }

            if (this.Sex == Sexs.Male)
                retval = retval?.OrderBy(x => x.NupciesMarit).ThenBy(x => x.FchLocation?.Fch?.Fch1).ToList();
            else
                retval = retval?.OrderBy(x => x.NupciesMuller).ThenBy(x => x.FchLocation?.Fch?.Fch1).ToList();

            return retval;
        }

        public bool IsDifuntAt(string? fch)
        {
            if (FchLocationTo?.Fch?.Fch1 == null)
            {
                if (FchLocationFrom?.Fch?.Fch1?.Length >= 2 && fch?.Length >= 2)
                {
                    int.TryParse(FchLocationFrom.Fch.Fch1.Substring(0, 2), out int segleFrom);
                    int.TryParse(fch.Substring(0, 2), out int segleFch);
                    return segleFch - segleFrom > 1;
                }
                return false;
            }
            else
                return string.Compare(FchLocationTo?.Fch?.Fch1, fch) < 0;
        }

        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTarget = Nom ?? "";
                var targetLower = searchTarget.ToLower().RemoveAccents();
                var targetYear = YearFrom;
                var searchParams = new SearchParams(searchTerm);
                retval = searchParams.Terms?.All(term => targetLower.Contains(term.ToLower().RemoveAccents())) ?? true;
                if (retval && searchParams.FromYear != null)
                    retval = string.Compare(targetYear, searchParams.FromYear,  CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace) >= 0;
                if (retval && searchParams.ToYear != null)
                    retval = string.Compare(targetYear, searchParams.ToYear,  CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace) <= 0;
            }
            return retval;
        }

        public string FullNom() => string.Format("{0} {1}", YearFrom, Nom);
        public string FullNom2() => string.Format("{0} ({1}-{2})", Nom, YearFrom, YearTo);
        public string Caption() => FullNom2(); //To implement iModel Interface for property grid selectors
        public bool ParesUnknown() => Pare == null && Mare == null;
        public override string ToString() => Nom ?? "?";

        private class SearchParams
        {
            public string[]? Terms { get; set; }
            public string? FromYear { get; set; }
            public string? ToYear { get; set; }

            public string? RawTerm { get; set; }

            public SearchParams(string? input)
            {
                RawTerm = input;
                if (!string.IsNullOrWhiteSpace(input))
                {
                    ParseSearchTerms();
                    ParseFromYear();
                    ParseToYear();
                }
            }

            private void ParseSearchTerms()
            {
                int indexGt = RawTerm!.IndexOf('>');
                int indexLt = RawTerm!.IndexOf('<');

                // Find the first occurrence of either character
                int firstIndex = -1;
                if (indexGt >= 0 && indexLt >= 0)
                    firstIndex = Math.Min(indexGt, indexLt);
                else if (indexGt >= 0)
                    firstIndex = indexGt;
                else if (indexLt >= 0)
                    firstIndex = indexLt;

                var cleanSearchTerm= firstIndex >= 0 ? RawTerm.Substring(0, firstIndex) : RawTerm;
                Terms = cleanSearchTerm.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
            }

            private void ParseToYear()
            {
                string pattern = @"(?<=<)\d+(?=>|$)";
                MatchCollection matches = Regex.Matches(RawTerm!, pattern);
               ToYear = matches.FirstOrDefault()?.Value;
            }
            private void ParseFromYear()
            {
                string pattern = @"(?<=>)\d+(?=<|$)";
                MatchCollection matches = Regex.Matches(RawTerm!, pattern);
                FromYear = matches.FirstOrDefault()?.Value;
            }

        }
    }
}
