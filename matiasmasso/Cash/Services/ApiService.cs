using Cash.Models;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DTO;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Cash.Services
{
    public class ApiService
    {
        public EmpModel.EmpIds? Emp { get; set; }
        private HttpClient http;
        private AuthenticationService authenticationService;
        private CatalogService catalogService;
        public bool UseLocalApi { get; set; } // = true;
        private const string LOCALAPI_HOST = "localhost:7111";
        private const string REMOTEAPI_HOST = "api2.matiasmasso.es";

        public event Action<Exception?>? OnChange;

        public ApiService(HttpClient http, AuthenticationService authenticationService, CatalogService catalogService)
        {
            this.http = http;
            this.authenticationService = authenticationService;
            this.catalogService = catalogService;

            authenticationService.UserChanged += (newUser) =>
            {
                this.Emp = CurrentUser()?.DefaultEmp();
            };
        }

        public async Task<T?> GetAsync<T>(params string[] segments)
        {
            T? retval = default;
            var request = RequestMessage(HttpMethod.Get, segments);
            var response = await http.SendAsync(request);

            //var url = ApiUrl(segments);
            //var response = await http.GetAsync(url);
            var apiresponse = await ResponseMessage<T>(response, request.RequestUri?.ToString() ?? "");
            if (apiresponse.Success())
                retval = apiresponse.Value;
            else
                throw apiresponse?.ProblemDetails?.ToException(segments) ?? new Exception("error http");
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

            //var content = new StringContent(inputjson ?? "", System.Text.Encoding.UTF8, "application/json");
            //var response = await http.PostAsync(url, content);
            var apiresponse = await ResponseMessage<T>(response, url);
            if (apiresponse.Success())
                retval = apiresponse.Value;
            else
                throw apiresponse?.ProblemDetails?.ToException(segments) ?? new Exception("error http");

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
                throw apiresponse?.ProblemDetails?.ToException(segments) ?? new Exception("error http");

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
                        retval.ProblemDetails = new ProblemDetails { Title = response.ReasonPhrase };
                    else
                        retval.ProblemDetails = JsonConvert.DeserializeObject<ProblemDetails>(responseText);
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
        public EmpModel.EmpIds? DefaultEmp()
        {
            return this.Emp;
        }

        public EmpModel.EmpIds? DefaultEmp(List<EmpModel.EmpIds>? availableEmps)
        {
            var userEmps = CurrentUser()?.Emps;
            if (userEmps == null)
                return null;
            else
            {
                var authorisedEmps = availableEmps == null ? userEmps : availableEmps.Where(x => userEmps.Contains(x)).ToList();
                if (authorisedEmps.Contains((EmpModel.EmpIds)this.Emp!))
                    return this.Emp;
                else
                {
                    this.Emp = authorisedEmps.First();
                    return this.Emp;
                }
            }
            //=> CurrentUser()?.DefaultEmp(emps);
        }
        public List<EmpModel>? FilteredEmps(List<EmpModel.EmpIds>? availableEmps)
        {
            var ids = availableEmps == null ? CurrentUser()!.Emps : availableEmps?.Where(x => CurrentUser()!.Emps.Contains(x)).ToList();
            var retval = GetEmps()?
                .Where(x => ids?.Any(y => x.Id == y) ?? false)
                .OrderBy(x => x.SortOrder())
                .ToList();
            return retval;
        }

        private void AddApiKey(HttpRequestMessage request)
        {
            if (CurrentUser() != null)
                request.Headers.Add("ApiKey", CurrentUser()!.Guid.ToString());
        }

        #region Catalog

        public void Reset()
        {
            catalogService.Contacts = null;
            catalogService.PgcCtas = null;
            catalogService.Projectes = null;
            catalogService.Countries = null;
            catalogService.Zonas = null;
            catalogService.Locations = null;
            catalogService.Zips = null;
        }

        public List<EmpModel>? GetEmps()
        {
            if (catalogService.Emps == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.Emps));

            return catalogService.Emps;
        }
        public EmpModel? GetEmp(EmpModel.EmpIds emp) => GetEmps()?.FirstOrDefault(x => x.Id == emp);

        public List<ContactModel>? GetContacts(EmpModel.EmpIds? emp = null)
        {
            if (catalogService.Contacts == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.Contacts));

            if (emp == null)
                return catalogService.Contacts;
            else
                return catalogService.Contacts?.Where(x => x.Emp == emp).ToList();
        }

        public ContactModel? GetContact(Guid? guid)
        {
            if (catalogService.Contacts == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.Contacts));
            return catalogService.Contacts?.FirstOrDefault(x => x.Guid == guid);
        }

        public List<PgcCtaModel>? GetPgcCtas()
        {
            if (catalogService.PgcCtas == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.PgcCtas));
            return catalogService.PgcCtas;
        }

        public List<PgcCtaModel>? GetCurrentPgcCtas()
        {
            if (catalogService.PgcCtas == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.PgcCtas));
            return catalogService.PgcCtas?.Where(x => x.Plan == PgcPlanModel.Default().Guid).ToList();
        }


        public PgcCtaModel? GetPgcCta(Guid? guid)
        {
            PgcCtaModel? retval = null;
            if (guid != null)
            {
                if (catalogService.PgcCtas == null)
                    _ = Task.Run(async () => await FetchAsync(CatalogService.Items.PgcCtas));
                retval = catalogService.PgcCtas?.FirstOrDefault(x => x.Guid == guid);
            }
            return retval;
        }

        public PgcCtaModel? GetPgcCta(PgcCtaModel.Cods cod)
        {
            if (catalogService.PgcCtas == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.PgcCtas));
            return catalogService.PgcCtas?.FirstOrDefault(x => x.Cod == cod);
        }

        public List<PgcClassModel>? GetPgcClasses()
        {
            if (catalogService.PgcClasses == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.PgcClasses));
            return catalogService.PgcClasses;

        }

        public List<ProjecteModel>? GetProjectes(EmpModel.EmpIds? emp = null)
        {
            if (catalogService.Projectes == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.Projectes));
            if (emp == null)
                return catalogService.Projectes;
            else
                return catalogService.Projectes?.Where(x => x.Emp == emp).ToList();

        }

        public CountryModel? GetCountry(Guid? guid)
        {
            if (catalogService.Countries == null)
                _ = Task.Run(async () =>
                {
                    await FetchAsync(CatalogService.Items.Countries);
                });
            return catalogService.Countries?.FirstOrDefault(x => x.Guid == guid);
        }

        public List<CountryModel>? GetCountries()
        {
            if (catalogService.Countries == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.Countries));
            return catalogService.Countries;
        }

        public ZonaModel? GetZona(Guid? guid)
        {
            if (catalogService.Zonas == null)
                _ = Task.Run(async () =>
                {
                    await FetchAsync(CatalogService.Items.Zonas);
                });
            return catalogService.Zonas?.FirstOrDefault(x => x.Guid == guid);
        }

        public List<ZonaModel>? GetZonas()
        {
            if (catalogService.Zonas == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.Zonas));
            return catalogService.Zonas;

        }

        public LocationModel? GetLocation(Guid? guid)
        {
            if (catalogService.Locations == null)
                _ = Task.Run(async () =>
                {
                    await FetchAsync(CatalogService.Items.Locations);
                });
            return catalogService.Locations?.FirstOrDefault(x => x.Guid == guid);
        }

        public List<LocationModel>? GetLocations()
        {
            if (catalogService.Locations == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.Locations));
            return catalogService.Locations;
        }

        public List<ZipModel>? GetZips()
        {
            if (catalogService.Zips == null)
                _ = Task.Run(async () => await FetchAsync(CatalogService.Items.Zips));
            return catalogService.Zips;
        }

        public ZipDTO? GetFullZip(Guid? guid)
        {
            var tmp = GetZip(guid);
            if (tmp == null)
                return null;
            var location = GetLocation(tmp?.Location);
            var zona = GetZona(location?.Zona);
            var country = GetCountry(zona?.Country);
            var retval = new ZipDTO()
            {
                Guid = tmp!.Guid,
                ZipCod = tmp.ZipCod,
                Location = new LocationDTO()
                {
                    Guid = (Guid)location!.Guid,
                    Nom = location.Nom,
                    Zona = new ZonaDTO()
                    {
                        Guid = (Guid)zona!.Guid,
                        Nom = zona.Nom,
                        Country = new CountryDTO()
                        {
                            Guid = (Guid)country!.Guid,
                            Nom = new LangTextDTO(country.Nom?.Esp)
                        }
                    }
                }
            };
            return retval;
        }

        public string? BuildContactFullNom(ContactModel value)
        {
            var retval = value.RaoSocial;
            if (!string.IsNullOrEmpty(value.NomComercial))
                retval = $"{value.RaoSocial} \"{value.NomComercial}\"";
            if (value.Address?.ZipGuid != null)
            {
                var location = GetLocation(GetZip(value.Address.ZipGuid)?.Location);
                if (location != null)
                {
                    var zona = GetZona(location?.Zona);
                    var country = GetCountry(zona?.Country);
                    if (country?.IsSpain() ?? true)
                    {
                        if (!string.IsNullOrEmpty(zona?.Nom) && zona.Nom != location!.Nom)
                            retval = $"{retval} ({location!.Nom},{zona!.Nom})";
                        else
                            retval = $"{retval} ({location!.Nom})";
                    } else
                        retval = $"{retval} ({location!.Nom},{country!.Nom?.Tradueix(LangDTO.Cat())})";
                }
            }
            return retval;

        }

        public ZipModel? GetZip(Guid? guid)
        {
            if (catalogService.Zips == null)
                _ = Task.Run(async () =>
                {
                    await FetchAsync(CatalogService.Items.Zips);
                });
            return catalogService.Zips?.FirstOrDefault(x => x.Guid == guid);
        }

        public List<GuidNom>? GetZipNoms()
        {
            var retval = new List<GuidNom>();

            if (catalogService.Zips == null)
                _ = Task.Run(async () =>
                {
                    await FetchAsync(CatalogService.Items.Zips);
                });

            foreach (var country in GetCountries() ?? new())
            {
                foreach (var zona in GetZonas()?.Where(x => x.Country == country.Guid).ToList() ?? new())
                {
                    foreach (var location in GetLocations()?.Where(x => x.Zona == zona.Guid).ToList() ?? new())
                    {
                        foreach (var zip in GetZips()?.Where(x => x.Location == location.Guid).ToList() ?? new())
                        {
                            var nom = $"{zip.ZipCod} {location.Nom}";
                            if (country.IsSpain())
                            {
                                if (zona.Nom?.CompareTo(location.Nom) != 0) nom = $"{nom} ({zona.Nom})";
                            }
                            else
                                nom = $"{nom} ({country.Nom?.Tradueix(LangDTO.Cat())})";
                            var guidnom = new GuidNom(zip.Guid, nom);
                            retval.Add(guidnom);
                        }
                    }
                }
            }
            return retval.OrderBy(x => x.Nom).ToList();
        }



        public void NotifyChange()
        {
            OnChange?.Invoke(null);
        }

        public async Task FetchAsync(CatalogService.Items item)
        {
            if (catalogService.States[(int)item] != DbState.IsLoading)
            {
                catalogService.States[(int)item] = DbState.IsLoading;
                try
                {
                    switch (item)
                    {
                        case CatalogService.Items.Emps:
                            catalogService.Emps = await GetAsync<List<EmpModel>>("Emps") ?? new();
                            break;
                        case CatalogService.Items.Contacts:
                            catalogService.Contacts = await GetAsync<List<ContactModel>>("Contacts/all") ?? new();
                            break;
                        case CatalogService.Items.PgcCtas:
                            catalogService.PgcCtas = await GetAsync<List<PgcCtaModel>>("PgcCtas") ?? new();
                            break;
                        case CatalogService.Items.PgcClasses:
                            var plan = PgcPlanModel.Default();
                            catalogService.PgcClasses = await GetAsync<List<PgcClassModel>>("PgcClasss", plan.Guid.ToString()) ?? new();
                            break;
                        case CatalogService.Items.Projectes:
                            catalogService.Projectes = await GetAsync<List<ProjecteModel>>("Projectes") ?? new();
                            break;
                        case CatalogService.Items.Countries:
                            catalogService.Countries = await GetAsync<List<CountryModel>>("Countries") ?? new();
                            break;
                        case CatalogService.Items.Zonas:
                            catalogService.Zonas = await GetAsync<List<ZonaModel>>("Zonas") ?? new();
                            break;
                        case CatalogService.Items.Locations:
                            catalogService.Locations = await GetAsync<List<LocationModel>>("Locations") ?? new();
                            break;
                        case CatalogService.Items.Zips:
                            catalogService.Zips = await GetAsync<List<ZipModel>>("Zips") ?? new();
                            break;
                    }
                    catalogService.States[(int)item] = DbState.StandBy;
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    catalogService.States[(int)item] = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
            }
        }

        public async Task UpdateAsync(ZonaModel value)
        {
            await PostAsync<ZonaModel, bool>(value, "Zona");
            var existingValue = catalogService.Zonas?.FirstOrDefault(x => x.Guid == value.Guid);
            if (existingValue == null)
                catalogService.Zonas!.Add(value);
            else
                catalogService.Zonas![catalogService.Zonas.IndexOf(existingValue)] = value;
        }

        public async Task UpdateAsync(LocationModel value)
        {
            await PostAsync<LocationModel, bool>(value, "Location");
            var existingValue = catalogService.Locations?.FirstOrDefault(x => x.Guid == value.Guid);
            if (existingValue == null)
                catalogService.Locations!.Add(value);
            else
                catalogService.Locations![catalogService.Locations.IndexOf(existingValue)] = value;
        }

        public async Task UpdateAsync(ZipModel value)
        {
            await PostAsync<ZipModel, bool>(value, "Zip");
            var existingValue = catalogService.Zips?.FirstOrDefault(x => x.Guid == value.Guid);
            if (existingValue == null)
                catalogService.Zips!.Add(value);
            else
                catalogService.Zips![catalogService.Zips.IndexOf(existingValue)] = value;
        }
        #endregion


    }

}
