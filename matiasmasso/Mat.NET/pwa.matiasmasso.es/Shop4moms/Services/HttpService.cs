using DTO;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Authorization;
using Irony.Parsing;
using static System.Net.WebRequestMethods;
using System.Security.Claims;

namespace Shop4moms.Services
{
    public class HttpService
    {
        public static AuthenticationHeaderValue? AuthHeader(AuthenticationState authState)
        {
            AuthenticationHeaderValue? retval = null;
            var user = authState.User;
            var claims = user.Claims;
            var claim = claims.FirstOrDefault(x => x.Type == "UserId");
            if (claim != null)
                retval = new AuthenticationHeaderValue("Bearer", claim.Value);
            return retval;
        }


        public static async Task<T?> Get2Async<T>(HttpClient http, params string[] segments)
        {
            T? retval;
            var apiresponse = await GetAsync<T>(http, segments);
            if (apiresponse.Success())
                retval = apiresponse.Value;
            else
                throw apiresponse?.ProblemDetails?.ToException() ?? new Exception("error http");
            return retval;
        }

        public static async Task<T?> Post2Async<U,T>(HttpClient http, U payload, params string[] segments)
        {
            T? retval;
            var apiresponse = await PostAsync<U,T>(http, payload, segments);
            if (apiresponse.Success())
                retval = apiresponse.Value;
            else
                throw apiresponse?.ProblemDetails?.ToException() ?? new Exception("error http");
            return retval;
        }


        public static async Task<ApiResponse<T>> GetAsync<T>(HttpClient http, params string[] segments)
        {
            string url = Globals.ApiUrl(segments);
            if (!string.IsNullOrEmpty(url))
            {
                var isExternal = segments.Length > 0 && segments.First().StartsWith("http");
                if (!isExternal) SetDefaultHeaders(http);
                try
                {
                    HttpResponseMessage response = await http.GetAsync(url);
                    return await ResponseMessage<T>(response, url);
                }
                catch (Exception ex)
                {
                    return ApiResponse<T>.Factory(ex);
                }
            }
            else
                return ApiResponse<T>.Factory("empty url");
        }


        public static async Task<ApiResponse<T>> PostAsync<U, T>(HttpClient http, U payload, params string[] segments)
        {
            var url = segments.Count() == 1 && segments.First().StartsWith("http") ? segments.First() : Globals.ApiUrl(segments);
            var inputjson = JsonConvert.SerializeObject(payload);
            var content = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            return await PostAsync<T>(http, url, content);
        }

        public static async Task<ApiResponse> PostAsync<U>(HttpClient http, U payload, params string[] segments)
        {
            var url = segments.Count() == 1 && segments.First().StartsWith("http") ? segments.First() : Globals.ApiUrl(segments);
            var inputjson = JsonConvert.SerializeObject(payload);
            var content = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            return await PostAsyncSimpleReturn(http, url ?? "", content);
        }


        public static async Task<ApiResponse<T>> PostAsync<T>(HttpClient http, string url, HttpContent? content = null)
        {
            try
            {
                //SetDefaultHeaders(http);
                HttpResponseMessage response = await http.PostAsync(url, content);
                return await ResponseMessage<T>(response, url);
            }
            catch (Exception ex)
            {
                return ApiResponse<T>.Factory(ex);
            }
        }
        private static async Task<ApiResponse> PostAsyncSimpleReturn(HttpClient http, string url, HttpContent content)
        {
            var retval = new ApiResponse();
            try
            {
                HttpResponseMessage response = await http.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    string responseText = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(responseText))
                        retval.ProblemDetails = new ProblemDetails { Title = response.ReasonPhrase };
                    else
                        retval.ProblemDetails = JsonConvert.DeserializeObject<ProblemDetails>(responseText);
                }

                return retval;
            }
            catch (Exception ex)
            {
                return ApiResponse.Factory(ex);
            }
        }

        public static async Task<T?> PostMultipartAsync<U, T>(HttpClient http, U payload, List<DocFileModel> docfiles, params string[] segments)
        {

            using var content = new MultipartFormDataContent();
            //content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

            //add the files

            foreach(var docfile in docfiles)
            {
                //add the file
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

            var url = Globals.ApiUrl(segments);

            T? retval;
            var apiresponse = await PostAsync<T>(http, url, content);
            if (apiresponse.Success())
                retval = apiresponse.Value;
            else
                throw apiresponse?.ProblemDetails?.ToException() ?? new Exception("error http");
            return retval;
        }

        public static async Task<ApiResponse<T>> PostMultipartAsync<U, T>(HttpClient http, U payload, DocFileModel? docfile = null, params string[] segments)
        {

            using var content = new MultipartFormDataContent();
            content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

            //add the file
            if (docfile?.Document?.Data != null)
            {
                var filename = string.Format("file_{0}", docfile.Hash);
                var fileStreamContent = new ByteArrayContent(docfile.Document.Data);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(docfile.Document.ContentType());
                content.Add(fileStreamContent, name: filename, fileName: filename);
            }

            //add the thumbnail
            if (docfile?.Thumbnail?.Data != null)
            {
                var filename = string.Format("thumbnail_{0}", docfile.Hash);
                var fileStreamContent = new ByteArrayContent(docfile.Thumbnail.Data);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(docfile.Thumbnail.ContentType());
                content.Add(fileStreamContent, name: filename, fileName: filename);
            }

            //add the data
            var inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(payload, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });

            var dataStringContent = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            content.Add(dataStringContent, "Data");

            var url = Globals.ApiUrl(segments);
            return await PostAsync<T>(http, url, content);

        }

        public static async Task<ApiResponse<T>> PostMultipartAsync<U, T>(HttpClient http, U payload, Media? media = null, params string[] segments)
        {

            using var content = new MultipartFormDataContent();
            content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

            //add the file
            if (media?.Data != null)
            {
                var fileStreamContent = new ByteArrayContent(media.Data);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(Media.ContentType((Media.MimeCods)media.Mime));
                content.Add(fileStreamContent, name: "File", fileName: "File");
            }

            //add the data
            var inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var dataStringContent = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            content.Add(dataStringContent, "Data");

            var url = Globals.ApiUrl(segments);
            //url = "https://localhost:44332/pdf/thumbnail";
            return await PostAsync<T>(http, url, content);

        }

        public static async Task<ApiResponse<T>> PostMultipartAsync<U, T>(HttpClient http, U payload, IBrowserFile file, params string[] segments)
        {

            using var content = new MultipartFormDataContent();
            content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

            //add the file
            if (file != null)
            {
                System.IO.Stream stream = file.OpenReadStream();
                var fileStreamContent = new StreamContent(stream);
                var mediaType = MimeHelper.ContentType(file.Name) ?? "application/pdf";
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                content.Add(fileStreamContent, name: "File", fileName: file.Name);
            }

            //add the data
            var inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var dataStringContent = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            content.Add(dataStringContent, "Data");

            var url = Globals.ApiUrl(segments);
            return await PostAsync<T>(http, url, content);
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
                        retval.Value = (T)Convert.ChangeType(result, typeof(T));
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


        protected static void SetDefaultHeaders(HttpClient http)
        {
            SetAuthHeader(http);
            SetLangHeader(http);
        }

        private static void SetAuthHeader(HttpClient http)
        {
            //if (Globals.Token != null)
            //    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Globals.Token);
        }

        private static void SetLangHeader(HttpClient http)
        {
            var cc = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var currentLangId = LangDTO.FromCultureInfo(cc).ToString();
            //var header = http.DefaultRequestHeaders.FirstOrDefault(x => x.Key == "Lang");
            //var httpLangId = header.Value?.FirstOrDefault() ?? "";
            //if (currentLangId != httpLangId)
                //http.DefaultRequestHeaders.Add("Lang", currentLangId.ToString());
        }


    }


    public class ApiResponse<T> : ApiResponse
    {
        public T? Value { get; set; } = default;

        #region FailureConstructors
        public static new ApiResponse<T> Factory(Exception ex) => Factory(ProblemDetails.Factory(ex));

        public static new ApiResponse<T> Factory(ProblemDetails problemDetails)
        {
            return new ApiResponse<T>()
            { ProblemDetails = problemDetails };
        }
        public static new ApiResponse<T> Factory(string errorReason)
        {
            return new ApiResponse<T>()
            { ProblemDetails = new ProblemDetails { Title = errorReason } };
        }

        #endregion

    }

    public class ApiResponse
    {
        public ProblemDetails? ProblemDetails { get; set; }

        #region FailureConstructors
        public static ApiResponse Factory(Exception ex) => Factory(ProblemDetails.Factory(ex));

        public static ApiResponse Factory(ProblemDetails problemDetails)
        {
            return new ApiResponse()
            { ProblemDetails = problemDetails };
        }
        public static ApiResponse Factory(string errorReason)
        {
            return new ApiResponse()
            { ProblemDetails = new ProblemDetails { Title = errorReason } };
        }

        #endregion

        public bool Fail() => !Success();
        public bool Success() => ProblemDetails == null;
    }

    public class ProblemDetails
    {
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public System.Exception? Exception { get; set; }

        public Dictionary<string, string[]> Errors { get; set; } = new();


        public static ProblemDetails Factory(Exception ex) => new ProblemDetails
        {
            Exception = ex,
            Title = ex.Message,
            Detail = ex.InnerException?.Message
        };

        public bool MoreInfoAvailable() => Exception != null | !string.IsNullOrEmpty(Detail);

        public string Details()
        {
            var sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(Detail)) sb.AppendLine(Detail);
            foreach (var err in Errors)
            {
                sb.AppendLine(err.Key.ToString());
                foreach (var val in err.Value)
                {
                    sb.AppendLine(val.ToString());
                }
            }
            return sb.ToString();
        }

        public Exception ToException()
        {
            if(string.IsNullOrEmpty(Detail)) 
                return new Exception(Title);
            else
                return new Exception(Title, new Exception(Detail));
        }
    }

    public class ProblemDetailsArgs : EventArgs
    {
        public ProblemDetails? Value { get; set; }

        public ProblemDetailsArgs(ProblemDetails? value)
        {
            Value = value;
        }
    }

}


