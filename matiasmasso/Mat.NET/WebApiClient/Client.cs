using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiClient
{
    public class Client
    {

        public static async Task<Resource> GetResource(string url)
        {
            using (var httpClient = new HttpClient())
            {
                //httpClient.BaseAddress = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                    return null;
                var resourceJson = await response.Content.ReadAsStringAsync();
                return JsonUtility.FromJson<Resource>(resourceJson);
            }
        }

    }
}
