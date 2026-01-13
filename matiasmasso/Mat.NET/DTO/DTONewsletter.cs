using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTONewsletter : DTOBaseGuid
    {
        public int Id { get; set; }
        public DateTime Fch { get; set; }
        public string Title { get; set; }

        private DTOLang _lang;
        public DTOLang Lang {
            get
            {
                return _lang;
            }
            set
            {
                _lang = value;
                foreach(DTONewsletterSource src in Sources)
                {
                    var url = src.Url;
                    if (url.Contains("/"))
                    {
                        //clean lang segment if exists
                        var lastSegment = url.Substring(url.LastIndexOf("/") );
                        if("/es,/ca,/en,/pt".Contains(lastSegment))
                            url=url.Substring(0,url.Length-lastSegment.Length);
                    }
                    src.Url = String.Format("{0}/{1}", url, _lang.ISO6391());
                }
            }
        }
        public List<DTONewsletterSource> Sources { get; set; }

        public DTONewsletter() : base()
        {
            Sources = new List<DTONewsletterSource>();
        }

        public DTONewsletter(Guid oGuid) : base(oGuid)
        {
        }


    }

    public class DTONewsletterSource
    {
        public bool Pro { get; set; }
        public int Ord { get; set; }
        public Cods Cod { get; set; }
        public string ImageUrl { get; set; }
        public DTOLangText Title { get; set; }
        public DTOLangText Excerpt { get; set; }
        public string Url { get; set; }
        public object Tag { get; set; }

        public enum Cods
        {
            NotSet,
            Blog,
            News,
            Events,
            Promo
        }
    }
}
