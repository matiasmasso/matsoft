using System;
using System.Linq;

namespace DTO
{
    public class DTOWebErr : DTOBaseGuid
    {
        public string Url { get; set; }
        public string Referrer { get; set; }
        public string Msg { get; set; }
        public string Ip { get; set; }
        public string Browser { get; set; }

        public DTOUser User { get; set; }
        public DateTime Fch { get; set; }

        public Cods Cod { get; set; }

        public enum Cods
        {
            NotSet,
            ManagedErr,
            PageNotFound,
            Err403,
            Err404,
            Err500
        }

        public DTOWebErr() : base()
        {
        }

        public DTOWebErr(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOWebErr Factory(Cods cod, DTOUser user)
        {
            DTOWebErr retval = new DTOWebErr();
            retval.Cod = cod;
            retval.User = user;
            retval.Fch = DTO.GlobalVariables.Now();
            return retval;
        }
        public static DTOWebErr Factory(Cods cod, string url, string msg = "")
        {
            DTOWebErr retval = new DTOWebErr();
            retval.Url = url;
            retval.Msg = msg;
            return retval;
        }

        public string FormattedBrowser()
        {
            string retval = this.Browser;
            if (retval.Contains("Googlebot"))
                retval = "GoogleBot";
            else if (retval.Contains("iPhone"))
                retval = "iPhone";
            else if (retval.Contains("Bingbot"))
                retval = "BingBot";
            return retval;
        }

        public bool IsBot()
        {
            string browser = this.Browser.ToLower();
            string[] bots = { "bot", "ubermetrics", "facebookexternalhit", "crawler" };
            bool retval = bots.Any(x => browser.Contains(x));
            return retval;
        }
    }
}
