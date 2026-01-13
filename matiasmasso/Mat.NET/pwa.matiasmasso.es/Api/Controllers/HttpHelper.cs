using Api.Services;
using Api.Shared;
using DTO;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Api.Controllers
{
    public class HttpHelper
    {
        public static LangDTO Lang(HttpRequest request)
        {
            LangDTO? retval = null;
            var headers = request.Headers;
            var lang = headers["Lang"];
            if (lang.Count > 0)
                retval = new LangDTO(lang.First());
            else
                retval = LangDTO.Default();
            return retval;
        }

        public static UserModel? User(HttpRequest request)
        {
            UserModel? retval = null;
            var token = Token(request);
            if(!string.IsNullOrEmpty(token))
            {
                var guid = UserId(token);
                if (guid != null)
                {
                    retval = Cache.Users.FirstOrDefault(x => x.Guid.Equals(guid));
                    if (retval == null)
                    {
                        retval = UserService.Find((Guid)guid);
                        if (retval != null) 
                            Cache.Users.Add(retval);
                    }
                }
            }
            return retval;
        }

        private static Guid? UserId(string token)
        {
            //returns userId from authorization header either if comes from Bearer JWT (Maui) or from Api Key (spa)
            Guid? retval = null;
            Guid guid;
            if (Guid.TryParse(token, out guid)) //Api Key
                retval = guid;
            else //bearer token
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var claims = jwtSecurityToken.Claims.ToList();
                var userIdClaim = claims.FirstOrDefault(x => x.Type == "UserId");
                if (userIdClaim != null)
                    retval = new Guid(userIdClaim.Value);

            }

            return retval;
        }

        private static string? Token(HttpRequest request)
        {
            string? retval = null;
            var headers = request.Headers;

            var authHeaderName = HeaderNames.Authorization;
            if (headers.ContainsKey(authHeaderName))
            {
                var authHeader = AuthenticationHeaderValue.Parse(headers[authHeaderName]);
                retval = authHeader?.Parameter;
            }
            return retval;
        }

    }

}
