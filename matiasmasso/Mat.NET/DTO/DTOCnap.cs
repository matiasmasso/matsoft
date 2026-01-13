using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOCnap : DTOBaseGuid
    {
        public string Id { get; set; }
        public DTOCnap Parent { get; set; }
        public List<DTOCnap> Children { get; set; }
        public DTOLangText NomShort { get; set; }
        public DTOLangText NomLong { get; set; }
        public List<string> Tags { get; set; }
        public List<DTOProduct> Products { get; set; }

        public DTOCnap() : base()
        {
            NomShort = new DTOLangText(base.Guid, DTOLangText.Srcs.CnapShort);
            NomLong = new DTOLangText(base.Guid, DTOLangText.Srcs.CnapLong);
        }

        public DTOCnap(Guid oGuid) : base(oGuid)
        {
            NomShort = new DTOLangText(base.Guid, DTOLangText.Srcs.CnapShort);
            NomLong = new DTOLangText(base.Guid, DTOLangText.Srcs.CnapLong);
        }

        public string ShortFullNom(DTOLang oLang)
        {
            string retval = string.Format("{0} {1}", Id, NomShort.Tradueix(oLang));
            return retval;
        }

        public string FullNom(DTOLang oLang)
        {
            string sNom = NomLong.Tradueix(oLang);
            if (sNom == "")
                sNom = NomShort.Tradueix(oLang);
            string retval = string.Format("{0} {1}", Id, sNom);
            return retval;
        }

        public static string FullNom(DTOCnap cnap, DTOLang lang)
        {
            string retval = "";
            if (cnap != null)
            {
                retval = cnap.FullNom(lang);
            }
            return retval;
        }

        public List<DTOCnap> Parents()
        {
            List<DTOCnap> retval = new List<DTOCnap>();
            DTOCnap item = this;
            while (item.Parent != null)
            {
                retval.Add(item.Parent);
                item = item.Parent;
            }
            return retval;
        }
        public List<DTOCnap> Descendants()
        {
            List<DTOCnap> retval = new List<DTOCnap>();
            retval.Add(this);
            foreach (DTOCnap child in this.Children)
            {
                retval.AddRange(child.Descendants());
            }
            return retval;
        }

        public bool IsSelfOrChildOf(DTOCnap oParent)
        {
            bool retval = oParent.Equals(this);
            if (!retval)
                retval = IsChildOf(oParent);
            return retval;
        }

        public bool IsSelfOrChildOfAny(List<DTOCnap> oParents)
        {
            bool retval = oParents.Any(x => x.Equals(this) || this.IsChildOf(x));
            return retval;
        }

        public bool IsChildOf(DTOCnap oParent)
        {
            bool retval = this.Parents().Any(x => x.Equals(oParent));
            return retval;
        }

        public List<DTOProductCategory> MatchingCategories(List<DTOProductCategory> src)
        {
            List<DTOProductCategory> retval = src.Where(x => this.Descendants().Any(y => x.Cnap.Guid.Equals(y.Guid))).ToList();
            return retval;
        }
    }
}
