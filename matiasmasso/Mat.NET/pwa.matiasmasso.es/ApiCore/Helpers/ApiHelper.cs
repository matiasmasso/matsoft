using Azure;
using Newtonsoft.Json;
using System.Net;

namespace Api.Helpers
{
    public class ApiHelper
    {
        public static byte[] GetAsync(string url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            MemoryStream ms = new MemoryStream();
            myResponse.GetResponseStream().CopyTo(ms);
            byte[] retval = ms.ToArray();
            return retval;
        }
        public static async Task<T?> PostAsync<U, T>(U payload, string url)
        {
            var inputjson = JsonConvert.SerializeObject(payload);
            using (var http = new HttpClient())
            {
                var content = new StringContent(inputjson, System.Text.Encoding.UTF8, "application/json");
                var response = await http.PostAsync(url, content);
                return await Result<T>(response);
           }
        }

        private static async Task<T?> Result<T>(HttpResponseMessage response)
        {
            T? retval = default;
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                retval = default;
            }
            else if (response.IsSuccessStatusCode)
            {
                if (IsSerializable(response))
                {
                    string responseText = response.Content == null ? "" : await response.Content.ReadAsStringAsync() ?? "";
                    if (typeof(T) == typeof(string))
                        retval = (T)(object)responseText;
                    else
                        retval = JsonConvert.DeserializeObject<T>(responseText);
                }
                else
                {
                    byte[]? result = response.Content == null ? null : await response.Content.ReadAsByteArrayAsync();
                    retval = (T)Convert.ChangeType(result, typeof(T));
                }
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return retval;
        }

        static bool IsSerializable(HttpResponseMessage responseMessage)
        {
            string[] serializableContentTypes = { "application/json", "text/html", "text/plain" };
            var contentType = responseMessage.Content?.Headers?.ContentType?.MediaType;
            return serializableContentTypes.Contains(contentType);
        }

    }

}
