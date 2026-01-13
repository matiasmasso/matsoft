using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Extensions;
using Newtonsoft.Json.Linq;
using ApiCore.Services;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OauthController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> PostAsync(IFormCollection formCollection)
        {
            IActionResult retval;
            try
            {
                var value = FormCollectionToJson(formCollection);
                string body = JsonConvert.SerializeObject(value);
                await MailService.SendCTO("post received in api2.matiasmasso.es/oauth", body);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        private JObject FormCollectionToJson(IFormCollection obj)
        {
            dynamic json = new JObject();
            if (obj.Keys.Any())
            {
                foreach (string key in obj.Keys)
                {   //check if the value is an array                 
                    if (obj[key].Count > 1)
                    {
                        JArray array = new JArray();
                        for (int i = 0; i < obj[key].Count; i++)
                        {
                            array.Add(obj[key][i]);
                        }
                        json.Add(key, array);
                    }
                    else
                    {
                        var value = obj[key][0];
                        json.Add(key, value);
                    }
                }
            }
            return json;
        }
    }
}
