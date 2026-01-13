using System.Collections.Generic;
using System.Web;

namespace DTO
{
    public class DTOBrowser
    {
        public string IP { get; set; }
        public string OS { get; set; }
        public string Browser { get; set; }
        public string UserName { get; set; }
        public string SessionId { get; set; }
        public string Rol { get; set; }

        public Dictionary<string, string> Cookies { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public static DTOBrowser Factory(string IP, string BrowserName, string BrowserVersion, DTOUser oUser)
        {
            DTOBrowser retval = new DTOBrowser();
            {
                var withBlock = retval;
                withBlock.IP = IP;
                withBlock.Browser = string.Format("{0} {1}", BrowserName, BrowserVersion);
                if (oUser != null)
                {
                    withBlock.UserName = DTOUser.NicknameOrElse(oUser);
                    withBlock.Rol = oUser.Rol.id.ToString();
                }
            }
            return retval;
        }

        public static string EmailLink(DTOBrowser oBrowser, bool BlScriptsEnabled)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string s in List(oBrowser, BlScriptsEnabled))
                sb.AppendLine(s);

            string src = sb.ToString();
            string sBody = HttpUtility.UrlEncode(src.ToString());
            string sSubject = HttpUtility.UrlEncode("prueba de navegador de usuario");
            string sTo = "matias@matiasmasso.es";
            string retval = "mailto:" + sTo + "?subject=" + sSubject + "&body=" + sBody;
            return retval;
        }

        public static List<string> List(DTOBrowser oBrowser, bool BlScriptsEnabled)
        {
            List<string> retval = new List<string>();
            {
                var withBlock = oBrowser;
                retval.Add("IP: " + withBlock.IP);
                // retval.Add("OS: " & .OS)
                retval.Add("Browser: " + withBlock.Browser);
                retval.Add("Scripts: " + (BlScriptsEnabled ? "SI" : "NO"));
                retval.Add("User name: " + withBlock.UserName);
                retval.Add("Session: " + withBlock.SessionId);
                retval.Add("Rol: " + withBlock.Rol);
            }
            return retval;
        }
    }
}
