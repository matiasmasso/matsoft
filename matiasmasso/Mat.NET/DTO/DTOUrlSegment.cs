using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOUrlSegment
    {
        public DTOBaseGuid Target { get; set; }
        public string Segment { get; set; }
        public DTOLang Lang { get; set; }
        public bool Canonical { get; set; }


        public static DTOUrlSegment Factory(string segment, DTOLang lang = null, bool canonical = false)
        {
            DTOUrlSegment retval = new DTOUrlSegment();
            retval.Segment = segment;
            retval.Lang = lang;
            retval.Canonical = canonical;
            return retval;
        }

        public class Collection : List<DTOUrlSegment>
        {
            public Collection() : base()
            {

            }

            public static Collection Factory(List<DTOUrlSegment> src)
            {
                Collection retval = new Collection();
                retval.AddRange(src);
                return retval;
            }
            public DTOUrlSegment Add(string segment, DTOLang lang = null, bool canonical = false)
            {
                DTOUrlSegment retval = DTOUrlSegment.Factory(segment, lang, canonical);
                this.Add(retval);
                return retval;
            }

            public DTOLangText Canonical()
            {
                List<DTOUrlSegment> tmp = this.Where(x => x.Canonical == true).ToList();
                DTOUrlSegment oEsp = tmp.FirstOrDefault(x => x.Lang.Equals(DTOLang.ESP()));
                DTOUrlSegment oCat = tmp.FirstOrDefault(x => x.Lang.Equals(DTOLang.CAT()));
                DTOUrlSegment oEng = tmp.FirstOrDefault(x => x.Lang.Equals(DTOLang.ENG()));
                DTOUrlSegment oPor = tmp.FirstOrDefault(x => x.Lang.Equals(DTOLang.POR()));

                string esp = oEsp == null ? "" : oEsp.Segment;
                string cat = oCat == null ? "" : oCat.Segment;
                string eng = oEng == null ? "" : oEng.Segment;
                string por = oPor == null ? "" : oPor.Segment;

                DTOLangText retval = new DTOLangText(esp, cat, eng, por);
                return retval;
            }
        }


    }
}
