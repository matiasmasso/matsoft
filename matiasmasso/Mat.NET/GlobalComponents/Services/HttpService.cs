//using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net;
using static System.Net.WebRequestMethods;
using System.Security.Claims;
using Newtonsoft.Json;

namespace GlobalComponents.Services
{
    public class HttpService
    {
        private readonly HttpClient http;
        
        public HttpService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
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


        public  async Task<ApiResponse<T>> PostAsync<U, T>(U payload, string url)
        {
            var inputjson = JsonConvert.SerializeObject(payload);
            var content = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            return await PostAsync<T>( url, content);
        }

        public  async Task<ApiResponse> PostAsync<U>(U payload, string url)
        {
            var inputjson = JsonConvert.SerializeObject(payload);
            var content = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
            return await PostAsyncSimpleReturn(url ?? "", content);
        }

        private  async Task<ApiResponse<T>> PostAsync<T>(string url, HttpContent? content = null)
        {
            try
            {
                HttpResponseMessage response = await http.PostAsync(url, content);
                return await ResponseMessage<T>(response, url);
            }
            catch (Exception ex)
            {
                return ApiResponse<T>.Factory(ex);
            }
        }
        private  async Task<ApiResponse> PostAsyncSimpleReturn(string url, HttpContent content)
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

        private  async Task<ApiResponse<T>> ResponseMessage<T>(HttpResponseMessage response, string url)
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


