using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.EMMA;
using DTO;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using Web.Components;
using Web.Models;
using Web.Services;

namespace Web.Services
{
    public class ApiService
    {
        private HttpClient http;
        private AuthenticationService authenticationService;
        private CatalogService catalogService;
        public bool UseLocalApi { get; set; } // = true;
        private const string LOCALAPI_HOST = "localhost:7286";
        private const string REMOTEAPI_HOST = "genapi.matiasmasso.es";

        public List<IModel> CurrentModels { get; set; } = new();
        public event Action<Exception?>? OnChange;

        public ApiService(HttpClient http, AuthenticationService authenticationService, CatalogService catalogService)
        {
            this.http = http;
            this.authenticationService = authenticationService;
            this.catalogService = catalogService;

            authenticationService.UserChanged += (newUser) =>
            {
            };
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task<T?> GetAsync<T>(params string[] segments)
        {
            T? retval = default;
            var request = RequestMessage(HttpMethod.Get, segments);
            var response = await http.SendAsync(request);

            var apiresponse = await ResponseMessage<T>(response, request.RequestUri?.ToString() ?? "");
            if (apiresponse.Success())
                retval = apiresponse.Value;
            else
                throw apiresponse?.ProblemDetails?.ToException() ?? new Exception("error http");
            return retval;
        }

        public async Task<T?> PostAsync<U, T>(U payload, params string[] segments)
        {
            T? retval = default;
            var url = ApiUrl(segments);
            string? inputjson = JsonConvert.SerializeObject(payload);

            var request = RequestMessage(HttpMethod.Post, segments);
            request.Content = new StringContent(inputjson ?? "", System.Text.Encoding.UTF8, "application/json");
            var response = await http.SendAsync(request);

            var apiresponse = await ResponseMessage<T>(response, url);
            if (apiresponse.Success())
                retval = apiresponse.Value;
            else
                throw apiresponse?.ProblemDetails?.ToException() ?? new Exception("error http");

            return retval;
        }

        public async Task<T?> PostMultipartAsync<U, T>(U payload, params string[] segments)
        {
            return await PostMultipartAsync<U, T>(new List<DocfileModel>(), payload, segments);
        }

        public async Task<T?> PostMultipartAsync<U, T>(DocfileModel? docfile, U payload, params string[] segments)
        {
            if (docfile == null)
                return await PostMultipartAsync<U, T>(new List<DocfileModel>(), payload, segments);
            else
                return await PostMultipartAsync<U, T>(new List<DocfileModel>() { docfile }, payload, segments);
        }

        public async Task<T?> PostMultipartAsync<U, T>(List<DocfileModel> docfiles, U payload, params string[] segments)
        {
            T? retval = default;
            var url = ApiUrl(segments);

            using var content = new MultipartFormDataContent();
            content.Headers.ContentType!.MediaType = "multipart/form-data";

            //add the files
            foreach (var docfile in docfiles)
            {
                if (docfile?.Document?.Data != null)
                {
                    var filename = docfile.HashFilename();
                    var fileStreamContent = new ByteArrayContent(docfile.Document.Data);
                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(docfile.Document.ContentType());
                    content.Add(fileStreamContent, name: filename, fileName: filename);
                }

                //add the thumbnail
                if (docfile?.Thumbnail?.Data != null)
                {
                    var filename = docfile.HashThumbnailname();
                    var fileStreamContent = new ByteArrayContent(docfile.Thumbnail.Data);
                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(docfile.Thumbnail.ContentType());
                    content.Add(fileStreamContent, name: filename, fileName: filename);
                }
            }

            //add the data
            var inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(payload, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });

            var dataStringContent = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            content.Add(dataStringContent, "Data");

            var request = RequestMessage(HttpMethod.Post, segments);
            request.Content = content;
            var response = await http.SendAsync(request);

            //var response = await http.PostAsync(url, content);
            var apiresponse = await ResponseMessage<T>(response, url);
            if (apiresponse.Success())
                retval = apiresponse.Value;
            else
                throw apiresponse?.ProblemDetails?.ToException() ?? new Exception("error http");

            return retval;
        }

        private static async Task<ApiResponse<T>> ResponseMessage<T>(HttpResponseMessage response, string url)
        {
            ApiResponse<T> retval = new ApiResponse<T>();
            try
            {

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    retval.Value = default;
                }
                else if (response.IsSuccessStatusCode)
                {
                    var contentType = response.Content?.Headers?.ContentType?.MediaType;
                    if (contentType == "application/json" | contentType == "text/html" | contentType == "text/plain")
                    {
                        string responseText = response.Content == null ? "" : await response.Content.ReadAsStringAsync() ?? "";
                        if (typeof(T) == typeof(string))
                            retval.Value = (T)(object)responseText;
                        else
                            retval.Value = JsonConvert.DeserializeObject<T>(responseText);

                    }
                    else
                    {
                        byte[]? result = response.Content == null ? null : await response.Content.ReadAsByteArrayAsync();
                        retval.Value = (T?)Convert.ChangeType(result, typeof(T));
                    }
                }
                else
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(responseText))
                        retval.ProblemDetails = JsonConvert.DeserializeObject<ProblemDetails>(responseText);
                    //retval.ProblemDetails = new ProblemDetails { Title = response.ReasonPhrase };
                    else
                    retval.ProblemDetails = new ProblemDetails { Title = responseText };
                }
            }

            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Its a timeout issue
                var problemDetails = new ProblemDetails
                {
                    Title = "TimeOut Exception on " + url,
                    Detail = ex.Message + ' ' + ex.StackTrace
                };
                retval.ProblemDetails = problemDetails;
            }
            catch (Exception ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Exception on " + url,
                    Detail = ex.Message + ' ' + ex.StackTrace
                };
                retval.ProblemDetails = problemDetails;
            }
            return retval;
        }

        public string ApiUrl(params string?[] segments)
        {
            string retval;
            List<string> cleanSegments = new();
            foreach (var segment in segments)
            {
                if (!string.IsNullOrEmpty(segment)) cleanSegments.Add(segment);
            }

            for (var i = 0; i < segments.Length; i++)
            {

                if (cleanSegments[i].StartsWith("/"))
                    cleanSegments[i] = cleanSegments[i].Remove(0, 1);
                if (cleanSegments[i].EndsWith("/"))
                    cleanSegments[i] = cleanSegments[i].Remove(cleanSegments[i].Length - 1);
            }
            if (cleanSegments.Count > 0 && cleanSegments.First().StartsWith("http"))
                retval = string.Join("/", cleanSegments);
            else
            {
                var host = ApiHost();
                var segmentList = new List<string>() { host };
                segmentList.AddRange(cleanSegments);
                retval = string.Format("https://{0}", string.Join("/", segmentList));
            }
            return retval;
        }

        private HttpRequestMessage RequestMessage(HttpMethod httpMethod, params string[] segments)
        {
            return IsExternalApiCall(segments) ? ExternalApiRequestMessage(httpMethod, segments) : CorporateApiRequestMessage(httpMethod, segments);
        }

        private HttpRequestMessage ExternalApiRequestMessage(HttpMethod httpMethod, params string[] segments)
        {
            return new HttpRequestMessage(httpMethod, AbsolutePath(segments));
        }
        private HttpRequestMessage CorporateApiRequestMessage(HttpMethod httpMethod, params string[] segments)
        {
            var url = $"https://{ApiHost()}/{AbsolutePath(segments)}";
            var retval = new HttpRequestMessage(httpMethod, url);
            AddApiKey(retval);
            return retval;
        }

        private bool IsExternalApiCall(params string[] segments) => segments.Length > 0 && segments.First().StartsWith("http");
        private string ApiHost() => UseLocalApi ? LOCALAPI_HOST : REMOTEAPI_HOST;
        private string AbsolutePath(string[] segments)
        {
            char[] charsToTrim = { '/', ' ' };
            var trimmedSegments = segments
                .Select(x => x.Trim(charsToTrim))
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
            return string.Join("/", trimmedSegments);
        }

        public UserModel? CurrentUser() => authenticationService.CurrentUser;

        private void AddApiKey(HttpRequestMessage request)
        {
            if (CurrentUser() != null)
                request.Headers.Add("ApiKey", CurrentUser()!.Guid.ToString());
        }

        #region Catalog

        private enum Tables
        {
            users,
            persons,
            docs,
            docTargets,
            docSrcs,
            docCods,
            docRels,
            enlaces,
            locations,
            professions,
            firstnoms,
            cognoms,
            pubs,
            citas,
            repos
        }


        //circular reference 

        //public void Reset()
        //{
        //    catalogService.Reset();
        //}

        //public PersonModel? RootPerson() => catalogService.GetPerson(CurrentUser()?.RootPerson);

        //public List<Guid>? RootAncestorGuids() => RootPerson()?.AncestorGuids(catalogService.GetPersons());

        //public List<AncestorModel>? RootAncestors() => RootPerson()?.Ancestors(catalogService.GetPersons());

        public async Task FetchAsync(bool force = false)
        {
            if (force || catalogService.State == CatalogService.States.IsEmpty)
            {
                var t0 = FetchAsync(Tables.users);
                var t1 = FetchAsync(Tables.persons);
                var t2 = FetchAsync(Tables.docs);
                var t3 = FetchAsync(Tables.docTargets);
                var t4 = FetchAsync(Tables.docSrcs);
                var t5 = FetchAsync(Tables.docCods);
                var t6 = FetchAsync(Tables.docRels);
                var t7 = FetchAsync(Tables.enlaces);
                var t8 = FetchAsync(Tables.locations);
                var t9 = FetchAsync(Tables.professions);
                var ta = FetchAsync(Tables.firstnoms);
                var tb = FetchAsync(Tables.cognoms);
                var tc = FetchAsync(Tables.pubs);
                var td = FetchAsync(Tables.citas);
                var te = FetchAsync(Tables.repos);
                await Task.WhenAll(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, ta, tb, tc, td, te);
            }
        }

        void NotifyChange()
        {
            OnChange?.Invoke(null);
        }

        private async Task FetchAsync(Tables table)
        {
            try
            {
                switch (table)
                {
                    case Tables.users:
                        catalogService.Users = await GetAsync<List<UserModel>>("users");
                        break;
                    case Tables.persons:
                        catalogService.Persons = await GetAsync<List<PersonModel>>("persons");
                        break;
                    case Tables.docs:
                        catalogService.Docs = await GetAsync<List<DocModel>>("docs");
                        break;
                    case Tables.docTargets:
                        catalogService.DocTargets = await GetAsync<List<DocTargetModel>>("docTargets");
                        break;
                    case Tables.docSrcs:
                        catalogService.DocSrcs = await GetAsync<List<DocSrcModel>>("docSrcs");
                        break;
                    case Tables.docCods:
                        catalogService.DocCods = await GetAsync<List<DocCodModel>>("docCods");
                        break;
                    case Tables.docRels:
                        catalogService.DocRels = await GetAsync<List<DocRelModel>>("docRels");
                        break;
                    case Tables.enlaces:
                        catalogService.Enlaces = await GetAsync<List<EnlaceModel>>("enlaces");
                        break;
                    case Tables.locations:
                        catalogService.Locations = await GetAsync<List<LocationModel>>("locations");
                        break;
                    case Tables.professions:
                        catalogService.Professions = await GetAsync<List<ProfessionModel>>("professions");
                        break;
                    case Tables.firstnoms:
                        catalogService.Firstnoms = await GetAsync<List<FirstnomModel>>("firstnoms");
                        break;
                    case Tables.cognoms:
                        catalogService.Cognoms = await GetAsync<List<CognomModel>>("cognoms");
                        break;
                    case Tables.pubs:
                        catalogService.Pubs = await GetAsync<List<PubModel>>("pubs");
                        break;
                    case Tables.citas:
                        catalogService.Citas = await GetAsync<List<CitaModel>>("citas");
                        break;
                    case Tables.repos:
                        catalogService.Repos = await GetAsync<List<RepoModel>>("repos");
                        break;
                }
                NotifyChange();
            }

            catch (Exception ex)
            {
                OnChange?.Invoke(ex);
            }
        }

        #region Ancestors
        public PersonModel? RootPerson() => Person(CurrentUser()?.RootPerson);
        public List<Guid>? RootAncestorGuids() => RootPerson()?.AncestorGuids(catalogService.Persons);
        public List<AncestorModel>? RootAncestors() => RootPerson()?.Ancestors(catalogService.Persons);

        public int AncestorDocs()
        {
            int retval = 0;
            var ancestorGuids = RootAncestorGuids();
            if (ancestorGuids != null)
            {
                var docTargets = catalogService?
                    .DocTargets?
                    .Where(x => x.SubjectePassiu && x.Target != null && ancestorGuids.Contains((Guid)x.Target!))
                    .ToList();

                retval = docTargets?.Select(x => x.Doc).Distinct().Count() ?? 0;
            }
            return retval;
        }
        public int? AncestorFirstnoms() => RootAncestors()?
            .Where(x => x.Person.Firstnom != null).Select(x => x.Person.Firstnom)
            .Distinct().Count();
        public int? AncestorCognoms() => RootAncestors()?
            .Where(x => x.Person.Cognom != null).Select(x => x.Person.Cognom)
            .Distinct().Count();
        public int? AncestorProfessions() => RootAncestors()?
            .Where(x => x.Person.Profession != null).Select(x => x.Person.Profession)
            .Distinct().Count();

        public bool IsAncestor(PersonModel? target) => target == null ? false : RootAncestorGuids()?.Contains(target.Guid) ?? false;
        public bool HasGrau(PersonModel? target)
        {
            var targetAncestorGuids = target?.AncestorGuids(Persons());
            var rootAncestors = RootAncestors();
            var retval = rootAncestors?.Any(x => targetAncestorGuids?.Contains(x.Person.Guid) ?? false);
            return retval ?? false;
        }

        public int? RootGrau(PersonModel? target)
        {
            if (target == null) return null;

            var targetAncestorGuids = target.AncestorGuids(Persons());
            var rootAncestors = RootAncestors();
            var firstCommonAncestor = rootAncestors?.FirstOrDefault(x => targetAncestorGuids.Contains(x.Person.Guid));
            var retval = firstCommonAncestor?.Gen();
            return retval;
        }

        public string PersonColor(PersonModel? target)
        {
            string retval = Colors.Black;
            if (target == RootPerson())
                retval = Colors.Green;
            else if (IsAncestor(target))
                retval = Colors.Red;
            else if (HasGrau(target))
                retval = Colors.Navy;
            return retval;
        }

        #endregion

        #region Person
        public List<PersonModel>? Persons() => catalogService.Persons;

        public void RemovePerson(PersonModel? person)
        {
            if (person != null)
            {
                catalogService.Persons?.Remove(person);
                OnChange?.Invoke(null);
            }
        }
        public PersonModel? Person(Guid? guid) => catalogService.Persons?.FirstOrDefault(x => x.Guid == guid);

        public bool HasAncestors(PersonModel? value, List<AncestorModel>? ancestors)
        {
            return ancestors?.Any(x => x.Person.Guid == value?.Pare || x.Person.Guid == value?.Mare) ?? false;
        }

        public List<PersonModel> Males() => catalogService.Persons?.Where(x => x.Sex == PersonModel.Sexs.Male).ToList() ?? new();
        public List<PersonModel> Females() => catalogService.Persons?.Where(x => x.Sex == PersonModel.Sexs.Female).ToList() ?? new();

        public List<PersonModel> Pares(PersonModel? child)
        {
            var retval = new List<PersonModel>();
            if (child != null)
            {
                var pare = Person(child.Pare);
                var mare = Person(child.Mare);
                if (pare != null) retval.Add(pare);
                if (mare != null) retval.Add(mare);
            }
            return retval;
        }

        public List<PersonModel> Avis(PersonModel? grandchild)
        {
            var retval = new List<PersonModel>();
            if (grandchild != null)
            {
                var pare = Person(grandchild.Pare);
                var mare = Person(grandchild.Mare);
                if (pare != null) retval.AddRange(Pares(pare));
                if (mare != null) retval.AddRange(Pares(mare));
            }
            return retval;
        }

        public PersonModel? AviPatern(PersonModel? grandChild) => Person(Person(grandChild?.Pare)?.Pare);
        public PersonModel? AviaPaterna(PersonModel? grandChild) => Person(Person(grandChild?.Pare)?.Mare);
        public PersonModel? AviMatern(PersonModel? grandChild) => Person(Person(grandChild?.Mare)?.Pare);
        public PersonModel? AviaMaterna(PersonModel? grandChild) => Person(Person(grandChild?.Mare)?.Mare);
        public List<PersonModel>? Nets(Guid? grandparent)
        {
            var children = Children(grandparent);
            var retval = Persons()?.Where(x => children?.Any(y => y.Guid == x.Pare || y.Guid == x.Mare) ?? new())
                .OrderBy(x => x.FchLocationFrom?.Fch?.Fch1)
                .ToList();
            return retval;
        }

        public List<PersonModel>? Children(Guid? parent) => parent == null ? null : Persons()?.Where(x => x.Pare == parent || x.Mare == parent).ToList();

        public List<PersonModel>? Germans(Guid? guid) => Germans(Person(guid));
        public List<PersonModel>? Germans(PersonModel? person) => Persons()?.Where(x => x.Guid != person?.Guid && person?.Pare != null && (x.Pare == person?.Pare || person?.Mare != null && x.Mare == person?.Mare)).OrderBy(x => x.FchLocationFrom?.Fch?.Fch1).ToList();
        public List<PersonModel>? CosinsGermans(Guid? guid) => CosinsGermans(Person(guid));
        public List<PersonModel>? CosinsGermans(PersonModel? person)
        {
            var retval = new List<PersonModel>();
            var oncles = Oncles(person);
            foreach(var oncle in oncles ?? new List<PersonModel>())
            {
                retval.AddRange(Children(oncle.Guid) ?? new List<PersonModel>());
            }
            return retval;
        }

        public string? FillOFillaDe(PersonModel? value)
        {
            string? retval = null;
            var parenom = Firstnom(Person(value?.Pare)?.Firstnom)?.Nom;
            var marenom = Firstnom(Person(value?.Mare)?.Firstnom)?.Nom;

            if (!string.IsNullOrEmpty(parenom) && !string.IsNullOrEmpty(marenom)) {
                var hasBothParents = parenom != null && marenom != null;
                var parents = (parenom + (hasBothParents ? " i " : "") + marenom).Trim();
                var qualifier = value?.Sex == PersonModel.Sexs.Female ? "filla" : "fill";
                retval = $"{qualifier} de {parents}";
            }
            return retval;
        }

        public List<PersonModel>? Nebots(Guid? guid) => Nebots(Person(guid));
        public List<PersonModel>? Nebots(PersonModel? person)
        {
            var retval = new List<PersonModel>();
            var germans = Germans(person);
            if (germans == null || germans.Count == 0) return retval;

            foreach (var germa in Germans(person) ?? new())
            {
                var nebots = Children(germa.Guid);
                if (nebots?.Count > 0)
                {
                    retval.AddRange(nebots);
                }
            }
            return retval;
        }
        public List<PersonModel>? Oncles(Guid? guid) => Oncles(Person(guid));
        public List<PersonModel>? Oncles(PersonModel? person)
        {
            var retval = new List<PersonModel>();
            var onclesPaterns = Germans(person?.Pare) ?? new List<PersonModel>();
            var onclesMaterns = Germans(person?.Mare) ?? new List<PersonModel>();
            retval.AddRange(onclesPaterns);
            retval.AddRange(onclesMaterns);
            return retval;
        }
        public List<PersonModel>? Gendres(Guid? guid) => Gendres(Person(guid));
        public List<PersonModel>? Gendres(PersonModel? person)
        {
            var retval = new List<PersonModel>();
            var children = Children(person?.Guid);
            if (children == null || children.Count == 0) return retval;

            //afegeix els conjuges dels fills
            foreach (var child in children)
            {
                var conjuges = Conjuges(child);
                if (conjuges?.Count > 0)
                {
                    retval.AddRange(conjuges.Select(x => Person(x)).Where(x => x != null).Cast<PersonModel>());
                }
            }
            return retval;
        }

        public List<PersonModel>? Sogres(Guid? guid) => Sogres(Person(guid));
        public List<PersonModel>? Sogres(PersonModel? person)
        {
            var retval = new List<PersonModel>();
            foreach (var conjuge in Conjuges(person) ?? new())
            {
                var sogre = Person(Person(conjuge)?.Pare);
                if (sogre != null) retval.Add(sogre);
                var sogra = Person(Person(conjuge)?.Mare);
                if (sogra != null) retval.Add(sogra);
            }
            return retval;
        }


        public List<PersonModel>? Cunyats(Guid? guid) => Cunyats(Person(guid));
        public List<PersonModel>? Cunyats(PersonModel? person)
        {
            var retval = new List<PersonModel>();
            var germans = Germans(person) ?? new();

            //if (germans == null || germans.Count == 0) return retval;

            //afegeix els conjuges dels germans
            foreach (var germa in germans)
            {
                var cunyats = Conjuges(germa.Guid)?
                    .Select(x => Person(x))
                    .Where(x => x != null)
                    .Cast<PersonModel>().ToList();

                if (cunyats?.Count > 0)
                {
                    retval.AddRange(cunyats);
                }
            }

            //afegeix els germans del conjuge
            foreach (var conjuge in Conjuges(person) ?? new())
            {
                retval.AddRange(Germans(conjuge) ?? new());
            }

            return retval;
        }

        public List<Guid>? Conjuges(Guid? guid)
        {
            var person = Person(guid);
            var retval = Conjuges(person);
            return retval;
        }

        public List<Guid>? Conjuges(PersonModel? person)
        {
            List<Guid>? retval;
            if (person?.Sex == PersonModel.Sexs.Male)
                retval = Enlaces()?.Where(x => x.Marit == person.Guid && x.Muller != null).Select(x => (Guid)x.Muller!).ToList();
            else
                retval = Enlaces()?.Where(x => x.Muller == person?.Guid && x.Marit != null).Select(x => (Guid)x.Marit!).ToList();

            return retval;
        }

        public PersonModel NewChild(EnlaceModel? enlace, PersonModel.Sexs? sex = null)
        {
            var pare = Person(enlace?.Marit);
            var mare = Person(enlace?.Muller);
            var cognomPare = Cognom(pare?.Cognom)?.Nom?.ToUpper().RemoveAccents();
            var cognomMare = Cognom(mare?.Cognom)?.Nom?.ToUpper().RemoveAccents();
            var retval = new PersonModel
            {
                Pare = enlace?.Marit,
                Mare = enlace?.Muller,
                Cognom = pare?.Cognom,
                Sex=sex ?? PersonModel.Sexs.NotSet,
                FchLocationFrom=pare?.FchLocationFrom,
                //FchFrom = string.IsNullOrEmpty(enlace?.Fch) || enlace.Fch.Length < 3 ? null : $"{enlace.Fch?.Substring(0, 3)}X",
                FchCreated = DateTime.Now
            };

            //to improve as AFT enlace+9 months
            var minFch = string.IsNullOrEmpty(enlace?.FchLocation?.Fch?.Fch1) || enlace.FchLocation?.Fch?.Fch1.Length < 3 ? null : $"{enlace.FchLocation?.Fch?.Fch1?.Substring(0, 3)}X";
            retval.FchLocationFrom = pare.FchLocationFrom == null ? null : new FchLocationModel
            {
                Fch = new FchModel(minFch, null, FchModel.Qualifiers.AFT),
                Location = pare.FchLocationFrom.Location
            };

            if (string.IsNullOrEmpty(cognomMare))
                retval.Nom = ($"{cognomPare}, {FillOFillaDe(retval)}").Trim();
            else
                retval.Nom = ($"{cognomPare} {cognomMare}, {FillOFillaDe(retval)}").Trim();
            return retval;
        }

        public int GrauDeConsanguinitat(EnlaceModel? enlace) => GrauDeConsanguinitat(Person(enlace?.Marit), Person(enlace?.Muller));
        public int GrauDeConsanguinitat(PersonModel? p1, PersonModel? p2)
        {
            var retval = 0;
            if (p1 != null && p2 != null)
            {
                var maritAncestors = p1?.Ancestors(Persons());
                var mullerAncestors = PersonModel.AncestorGuids(p2, Persons());
                var firstCommonAncestors = maritAncestors?.FirstOrDefault(x => mullerAncestors.Contains(x.Person.Guid));
                if (firstCommonAncestors != null)
                {
                    retval = firstCommonAncestors.Level;
                }
            }
            return retval;
        }

        public List<PersonModel>? RecursivePersons(List<LocationModel>? recursiveLocations)
        {
            List<PersonModel>? retval = null;
            if (recursiveLocations != null && Persons() != null && DocTargets() != null)
            {
                retval = Persons()?.Where(
                    x => recursiveLocations.Any(
                        y => x.FchLocationFrom?.Location?.Guid == y.Guid || x.FchLocationTo?.Location?.Guid == y.Guid || x.FchLocationSepultura?.Location?.Guid == y.Guid
                        )
                    ).ToList();
            }
            return retval;
        }

        public string? FchLocationCaption(FchLocationModel? args)
        {
            string? retval = null;
            string? location = args?.Location?.Guid == null ? null : Location(args.Location?.Guid)?.NomLlarg;
            string? fch = args?.Fch?.ToString()!;

            if (string.IsNullOrEmpty(location) && string.IsNullOrEmpty(fch))
                retval = null;
            else if (string.IsNullOrEmpty(fch))
                retval = location;
            else if (string.IsNullOrEmpty(location))
                retval = fch;
            else
                retval = $"{fch}, {location}";
            return retval;
        }

        public string NormalizedPersonNom(PersonModel person)
        {
            var firstnom = Firstnom(person?.Firstnom)?.Nom;
            var cognom = Cognom(person?.Cognom)?.Nom;
            var retval = (firstnom + " " + cognom).Trim();

            if (person?.Mare != null)
            {
                var mare = Person(person.Mare);
                if (mare != null)
                {
                    var mareCognom = Cognom(mare.Cognom)?.Nom;
                    if (!string.IsNullOrEmpty(mareCognom))
                        retval += " " + mareCognom.Trim();
                }
            }
            return retval;
        }

        public string NormalizedPersonNom(Guid guid)
        {
            var person = Person(guid);
            var firstnom = Firstnom(person?.Firstnom)?.Nom;
            var cognom = Cognom(person?.Cognom)?.Nom;
            var retval = (firstnom + " " + cognom).Trim();

            if (person?.Mare != null)
            {
                var mare = Person(person.Mare);
                if (mare != null)
                {
                    var mareCognom = Cognom(mare.Cognom)?.Nom;
                    if (!string.IsNullOrEmpty(mareCognom))
                        retval += " " + mareCognom.Trim();
                }
            }
            return retval;
        }

        public async Task UpdateAsync(PersonModel value)
        {
            if (value != null)
            {
                if (await PostAsync<PersonModel, bool>(value, "Person"))
                {
                    value.FchLastEdited = DateTime.Now;
                    if (value.IsNew)
                    {
                        value.FchCreated = DateTime.Now;
                        catalogService.Persons!.Add(value);
                        catalogService.Persons = catalogService.Persons.OrderByDescending(x => x.FchCreated).ToList();
                        value.IsNew = false;
                    }
                    else
                    {
                        var previous = catalogService.Persons!.First(x => x.Guid == value.Guid)!;
                        var idx = catalogService.Persons!.IndexOf(previous);
                        catalogService.Persons![idx] = value;
                    }

                    //if(value.Pare != null || value.Mare != null)
                    //{
                    //    if(!(Enlaces() ?? new()).Any(x=>x.Marit == value.Pare && x.Muller == value.Mare))
                    //    {
                    //        var newEnlace = new EnlaceModel { Marit = value.Pare, Muller = value.Mare };
                    //    }
                    //}

                    OnChange?.Invoke(null);
                }
            }
        }
        public async Task DeleteAsync(PersonModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("Person/delete", value.Guid.ToString()))
                {
                    catalogService.Persons?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #region Firstnom
        public List<FirstnomModel>? Firstnoms() => catalogService.Firstnoms;
        public List<FirstnomModel>? Firstnoms(PersonModel.Sexs? sex) => catalogService.Firstnoms?.Where(x => x.Sex == sex)?.ToList();
        public FirstnomModel? Firstnom(Guid? guid) => catalogService.Firstnoms?.FirstOrDefault(x => x.Guid == guid);
        public List<FirstnomModel>? MaleFirstnomsOrDefault() => Firstnoms()?.Where(x => x.Sex != PersonModel.Sexs.Female).ToList();
        public List<FirstnomModel>? FemaleFirstnomsOrDefault() => Firstnoms()?.Where(x => x.Sex != PersonModel.Sexs.Male).ToList();
        public bool HasAncestors(FirstnomModel value, List<AncestorModel>? ancestors)
        {
            return ancestors?.Any(x => x.Person.Firstnom == value.Guid) ?? false;
        }

        public async Task UpdateAsync(FirstnomModel value)
        {
            if (value != null)
            {
                if (await PostAsync<FirstnomModel, bool>(value, "Firstnom"))
                {
                    if (value.IsNew)
                    {
                        catalogService.Firstnoms?.Add(value);
                        catalogService.Firstnoms = catalogService.Firstnoms?.OrderBy(x => x.Nom).ToList();
                        value.IsNew = false;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }
        public async Task DeleteAsync(FirstnomModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("Firstnom/delete", value.Guid.ToString()))
                {
                    catalogService.Firstnoms?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #region Cognom
        public CognomModel? Cognom(Guid? guid) => catalogService.Cognoms?.FirstOrDefault(x => x.Guid == guid);
        public List<CognomModel>? Cognoms() => catalogService.Cognoms;
        public List<CognomModel>? Cognoms(PersonModel root, List<PersonModel>? allPersons)
        {
            var ancestors = root.Ancestors(allPersons);
            var retval = catalogService.Cognoms?.Where(x => ancestors.Any(y => x.Guid == y.Person.Cognom)).ToList();
            return retval;
        }
        public bool HasAncestors(CognomModel value, List<AncestorModel>? ancestors)
        {
            return ancestors?.Any(x => x.Person.Cognom == value.Guid) ?? false;
        }
        public async Task UpdateAsync(CognomModel value)
        {
            if (value != null)
            {
                if (await PostAsync<CognomModel, bool>(value, "Cognom"))
                {
                    if (value.IsNew)
                    {
                        catalogService.Cognoms?.Add(value);
                        catalogService.Cognoms = catalogService.Cognoms?.OrderBy(x => x.Nom).ToList();
                        value.IsNew = false;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }
        public async Task DeleteAsync(CognomModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("Cognom/delete", value.Guid.ToString()))
                {
                    catalogService.Cognoms?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #region Enlace
        public List<EnlaceModel>? Enlaces() => catalogService.Enlaces;
        //public List<EnlaceModel>? Enlaces(PersonModel? person) => person == null ? new() : catalogService.Enlaces?
        //    .Where(x => x.Marit == person?.Guid || x.Muller == person?.Guid).ToList();
        public ICollection<EnlaceModel>? Enlaces(PersonModel? person) {
            var retval = person?.Enlaces(catalogService.Persons, catalogService.Enlaces);
            return retval;
        }
        public EnlaceModel? Enlace(Guid? guid) => catalogService.Enlaces?.FirstOrDefault(x => x.Guid == guid);
        public EnlaceModel? Enlace(PersonModel? marit, PersonModel? muller)
        {
            return Enlaces()?.FirstOrDefault(x => x.Marit == marit?.Guid && x.Muller == muller?.Guid);
        }
        public List<PersonModel>? Children(EnlaceModel? enlace)
        {
            return Persons()?.Where(x => x.Pare == enlace?.Marit && x.Mare == enlace?.Muller)
                .OrderBy(x => x.FchLocationFrom?.Fch?.Fch1)
                .ToList();
        }

        public async Task UpdateAsync(EnlaceModel? value)
        {
            if (value != null)
            {
                if (await PostAsync<EnlaceModel, bool>(value, "Enlace"))
                {
                    var previous = Enlace(value.Guid);
                    if (previous == null) previous = catalogService.Enlaces?.FirstOrDefault(x => x.Marit == value.Marit && x.Muller == value.Muller);
                    if (previous == null)
                    {
                        catalogService.Enlaces?.Add(value);
                        value.IsNew = false;
                    }
                    else
                    {
                        var idx = catalogService.Enlaces!.IndexOf(previous);
                        catalogService.Enlaces[idx] = value;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }
        public async Task DeleteAsync(EnlaceModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("Enlace/delete", value.Guid.ToString()))
                {
                    catalogService.Enlaces?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #region Profession
        public List<ProfessionModel>? Professions() => catalogService.Professions;
        public ProfessionModel? Profession(Guid? guid) => catalogService.Professions?.FirstOrDefault(x => x.Guid == guid);
        public bool HasAncestors(ProfessionModel value, List<AncestorModel>? ancestors)
        {
            return ancestors?.Any(x => x.Person.Profession == value.Guid) ?? false;
        }
        public async Task UpdateAsync(ProfessionModel value)
        {
            if (value != null)
            {
                if (await PostAsync<ProfessionModel, bool>(value, "Profession"))
                {
                    if (value.IsNew)
                    {
                        catalogService.Professions?.Add(value);
                        catalogService.Professions = catalogService.Professions?.OrderBy(x => x.Nom).ToList();
                        value.IsNew = false;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }
        public async Task DeleteAsync(ProfessionModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("Profession/delete", value.Guid.ToString()))
                {
                    catalogService.Professions?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #region Doc
        public List<DocModel>? Docs() => catalogService.Docs;
        public DocModel? Doc(Guid? guid) => catalogService.Docs?.FirstOrDefault(x => x.Guid == guid);
        public List<DocModel>? RecursiveDocs(DocSrcModel? value)
        {
            List<DocModel>? retval = null;
            if (value != null && catalogService.Docs != null)
            {
                var children = RecursiveChildren(value);
                children.Add(value);
                retval = catalogService.Docs.Where(x => children.Any(y => x.Src == y.Guid))
                    .OrderByDescending(x => x.Fch)
                    .ToList();
            }
            return retval;
        }

        public List<DocModel>? RecursiveDocs(List<LocationModel>? recursiveLocations)
        {
            List<DocModel>? retval = null;
            if (recursiveLocations != null && Docs() != null && DocTargets() != null)
            {
                var doctargets = DocTargets()!
                    .Where(x => recursiveLocations.Any(y => x.Target == y.Guid))
                    .ToList();

                retval = Docs()!.Where(x => doctargets.Any(y => x.Guid == y.Doc))
                    .OrderByDescending(x => x.Fch)
                    .ToList();
            }
            return retval;
        }

        public async Task UpdateAsync(DocModel value)
        {
            if (value != null)
            {
                if (await PostMultipartAsync<DocModel, bool>(value.DocFile, value, "Doc"))
                {
                    if (value.IsNew)
                    {
                        value.FchCreated = DateTime.Now;
                        catalogService.Docs!.Add(value);
                        value.IsNew = false;
                    }
                    else
                    {
                        var previous = catalogService.Docs!.First(x => x.Guid == value.Guid)!;
                        var idx = catalogService.Docs!.IndexOf(previous);
                        catalogService.Docs![idx] = value;
                    }

                    value.FchLastEdited = DateTime.Now;
                    catalogService.Docs = catalogService.Docs.OrderByDescending(x => x.FchLastEdited).ToList();
                    OnChange?.Invoke(null);
                }
            }
        }

        public async Task DeleteAsync(DocModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("doc/delete", value.Guid.ToString()))
                {
                    var previous = Doc(value.Guid);
                    if (previous != null)
                        catalogService.Docs?.Remove(previous);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #region DocSrc
        public List<DocSrcModel>? DocSrcs() => catalogService.DocSrcs;
        public DocSrcModel? DocSrc(Guid? guid) => catalogService.DocSrcs?.FirstOrDefault(x => x.Guid == guid);
        public List<DocSrcModel> PotentialParents(DocSrcModel? value)
        {
            List<DocSrcModel> retval = new();
            if (value != null)
            {
                var excludeGuids = RecursiveChildren(value).Select(x => x.Guid).ToList();
                excludeGuids.Add(value.Guid);
                retval = DocSrcs()?.Where(x => !excludeGuids.Contains(x.Guid)).ToList() ?? new();
            }
            return retval;
        }
        public List<DocSrcModel> RecursiveChildren(DocSrcModel? parent)
        {
            var retval = new List<DocSrcModel>();
            if (parent == null)
                retval = catalogService.DocSrcs;
            else if (catalogService.DocSrcs != null)
            {
                var children = catalogService.DocSrcs.Where(x => x.Parent == parent.Guid).OrderBy(x => x.NomLlarg).ToList();
                retval.AddRange(children);
                foreach (var child in children)
                {
                    retval.AddRange(RecursiveChildren(child));
                }
            }
            return retval ?? new();
        }
        public async Task UpdateAsync(DocSrcModel value)
        {
            if (value != null)
            {
                if (await PostMultipartAsync<DocSrcModel, bool>(value.DocFile, value, "DocSrc"))
                {
                    if (value.IsNew)
                    {
                        catalogService.DocSrcs?.Add(value);
                        value.IsNew = false;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }

        public async Task DeleteAsync(DocSrcModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("docsrc/delete", value.Guid.ToString()))
                {
                    catalogService.DocSrcs?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }

        #endregion

        #region DocCod
        public List<DocCodModel>? DocCods() => catalogService.DocCods;
        public DocCodModel? DocCod(Guid? guid) => catalogService.DocCods?.FirstOrDefault(x => x.Guid == guid);
        public bool HasAncestors(DocCodModel value, List<AncestorModel>? ancestors)
        {
            return false; // ancestors?.Any(x => x.Person.DocCod == value.Guid) ?? false;
        }
        public async Task UpdateAsync(DocCodModel value)
        {
            if (value != null)
            {
                if (await PostAsync<DocCodModel, bool>(value, "DocCod"))
                {
                    if (value.IsNew)
                    {
                        catalogService.DocCods?.Add(value);
                        catalogService.DocCods = catalogService.DocCods?.OrderBy(x => x.Nom).ToList();
                        value.IsNew = false;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }
        public async Task DeleteAsync(DocCodModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("DocCod/delete", value.Guid.ToString()))
                {
                    catalogService.DocCods?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #region DocRel
        public List<DocRelModel>? DocRels() => catalogService.DocRels;
        public DocRelModel? DocRel(Guid? guid) => catalogService.DocRels?.FirstOrDefault(x => x.Guid == guid);
        public bool HasAncestors(DocRelModel value, List<AncestorModel>? ancestors)
        {
            return false; // ancestors?.Any(x => x.Person.DocCod == value.Guid) ?? false;
        }
        public async Task UpdateAsync(DocRelModel value)
        {
            if (value != null)
            {
                if (await PostAsync<DocRelModel, bool>(value, "DocRel"))
                {
                    if (value.IsNew)
                    {
                        catalogService.DocRels?.Add(value);
                        catalogService.DocRels = catalogService.DocRels?.OrderBy(x => x.Nom).ToList();
                        value.IsNew = false;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }
        public async Task DeleteAsync(DocRelModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("DocRel/delete", value.Guid.ToString()))
                {
                    catalogService.DocRels?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #region DocTarget
        public List<DocTargetModel>? DocTargets()
        {
            var retval = catalogService.DocTargets;
            var oGuid = new Guid("3D9AAF0E-A74A-4EDE-83CA-378D84C75BC4");
            var tmp = catalogService.DocTargets?.FirstOrDefault(x => x.Guid == oGuid);
            return retval;
        }
        public DocTargetModel? DocTarget(Guid? guid) => catalogService.DocTargets?.FirstOrDefault(x => x.Guid == guid);

        public List<DocTargetModel>? DocTargets(PersonModel? person)
        {
            return DocTargets()?.Where(x => x.Target == person?.Guid)
            .OrderBy(x => x.SubjectePassiu)
            .ToList();
        }
        public List<DocTargetModel>? DocTargets(DocModel? doc)
        {
            return DocTargets()?.Where(x => x.Doc == doc?.Guid)
            .OrderByDescending(x => x.SubjectePassiu)
            .ToList();
        }
        public bool HasAncestors(DocTargetModel value, List<AncestorModel>? ancestors)
        {
            return false; // ancestors?.Any(x => x.Person.DocCod == value.Guid) ?? false;
        }
        public async Task UpdateAsync(DocTargetModel value)
        {
            if (value != null)
            {
                if (await PostAsync<DocTargetModel, bool>(value, "DocTarget"))
                {
                    if (value.IsNew)
                    {
                        catalogService.DocTargets?.Add(value);
                        value.IsNew = false;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }
        public async Task DeleteAsync(DocTargetModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("DocTarget/delete", value.Guid.ToString()))
                {
                    catalogService.DocTargets?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #region Location
        public LocationModel? Location(Guid? guid) => catalogService.Locations?.FirstOrDefault(x => x.Guid == guid);
        public List<LocationModel>? Locations() => catalogService.Locations;
        public List<LocationModel>? Locations(PersonModel root, List<PersonModel>? allPersons)
        {
            //var ancestors = root.Ancestors(allPersons);
            //var retval = catalogService.Locations?.Where(x => ancestors.Any(y => x.Guid == y.Person.Location)).ToList();
            return new(); // retval;
        }
        public bool HasAncestors(LocationModel value, List<AncestorModel>? ancestors)
        {
            return false;
        }

        public List<LocationModel> PotentialParents(LocationModel? value)
        {
            List<LocationModel> retval = new();
            if (value != null)
            {
                var excludeGuids = RecursiveChildren(value).Select(x => x.Guid).ToList();
                excludeGuids.Add(value.Guid);
                retval = Locations()?.Where(x => !excludeGuids.Contains(x.Guid)).ToList() ?? new();
            }
            return retval;
        }

        public List<LocationModel> RecursiveChildren(LocationModel? parent)
        {
            var retval = new List<LocationModel>();
            if (parent == null)
                retval = Locations();
            else if (Locations() != null)
            {
                var children = Locations()!.Where(x => x.Parent == parent.Guid).OrderBy(x => x.NomLlarg).ToList();
                retval.AddRange(children);
                foreach (var child in children)
                {
                    retval.AddRange(RecursiveChildren(child));
                }
            }
            return retval ?? new();
        }
        public async Task UpdateAsync(LocationModel value)
        {
            if (value != null)
            {
                if (await PostAsync<LocationModel, bool>(value, "Location"))
                {
                    if (value.IsNew)
                    {
                        catalogService.Locations?.Add(value);
                        catalogService.Locations = catalogService.Locations?.OrderBy(x => x.Nom).ToList();
                        value.IsNew = false;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }
        public async Task DeleteAsync(LocationModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("Location/delete", value.Guid.ToString()))
                {
                    catalogService.Locations?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #region Pub

        public List<PubModel>? Pubs() => catalogService.Pubs;
        public PubModel? Pub(Guid? guid) => catalogService.Pubs?.FirstOrDefault(x => x.Guid == guid);
        public async Task UpdateAsync(PubModel value)
        {
            if (value != null)
            {
                if (await PostMultipartAsync<PubModel, bool>(value.Docfile, value, "Pub"))
                {
                    if (value.IsNew)
                    {
                        catalogService.Pubs?.Add(value);
                        value.IsNew = false;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }

        public async Task DeleteAsync(PubModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("Pub/delete", value.Guid.ToString()))
                {
                    catalogService.Pubs?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }

        #endregion

        #region Citas
        public List<CitaModel>? Citas() => catalogService.Citas;
        public List<CitaModel>? Citas(Guid? pub) => catalogService.Citas?.Where(x => x.Pub == pub).ToList();
        public CitaModel? Cita(Guid? guid) => catalogService.Citas?.FirstOrDefault(x => x.Guid == guid);
        public async Task UpdateAsync(CitaModel value)
        {
            if (value != null)
            {
                if (await PostAsync<CitaModel, bool>(value, "Cita"))
                {
                    if (value.IsNew)
                    {
                        catalogService.Citas?.Add(value);
                        value.IsNew = false;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }

        public async Task DeleteAsync(CitaModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("Cita/delete", value.Guid.ToString()))
                {
                    catalogService.Citas?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #region Repo
        public RepoModel? Repo(Guid? guid) => catalogService.Repos?.FirstOrDefault(x => x.Guid == guid);
        public List<RepoModel>? Repos() => catalogService.Repos;

        public async Task UpdateAsync(RepoModel value)
        {
            if (value != null)
            {
                if (await PostAsync<RepoModel, bool>(value, "Repo"))
                {
                    if (value.IsNew)
                    {
                        catalogService.Repos?.Add(value);
                        catalogService.Repos = catalogService.Repos?.OrderBy(x => x.Nom).ToList();
                        value.IsNew = false;
                    }
                    OnChange?.Invoke(null);
                }
            }
        }
        public async Task DeleteAsync(RepoModel? value)
        {
            if (value != null)
            {
                if (await GetAsync<bool>("Repo/delete", value.Guid.ToString()))
                {
                    catalogService.Repos?.Remove(value);
                    OnChange?.Invoke(null);
                }
            }
        }
        #endregion

        #endregion
    }

}


