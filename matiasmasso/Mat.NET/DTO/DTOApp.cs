using System.Collections.Generic;

namespace DTO
{
    public class DTOApp
    {

        public static DTOApp Current { get; set; }
        public AppTypes Id { get; set; }
        public string Nom { get; set; }
        public string MinVersion { get; set; }
        public string LastVersion { get; set; }
        public List<DTOLang> Langs { get; set; }
        public DTOLang Lang { get; set; } = DTOLang.ESP();


        public DTOCur.Collection Curs { get; set; }
        public DTOCur Cur { get; set; }
        public List<DTOTax> Taxes { get; set; }
        public DTOPgcPlan PgcPlan { get; set; }
        public static string WebRootUrl { get; set; }
        public string WebLocalPort { get; set; }


        public enum AppTypes
        {
            notSet,
            matNet,
            spv,
            web,
            webServices,
            matSched,
            inglesinaSelfiesContest,
            oldAspMatWeb,
            wcf,
            webApi,
            IOS
        }

        public string DebuggerUrl(params string[] args)
        {
            string path = string.Join("/", args);
            string retval = string.Format("https://localhost:{0}/{1}", this.WebLocalPort, path);
            return retval;
        }

        public DTOApp() : base()
        {

        }

        public DTOApp(AppTypes id) : base()
        {
            this.Id = id;
        }
    }
}
