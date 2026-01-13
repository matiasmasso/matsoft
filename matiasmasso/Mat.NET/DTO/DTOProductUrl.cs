using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class DTOProductUrl
    {
        public DTOUrlSegment BrandSegment { get; set; }
        public DTOUrlSegment DeptSegment { get; set; }
        public DTOUrlSegment CategorySegment { get; set; }
        public DTOUrlSegment SkuSegment { get; set; }

        public DTOLang Lang { get; set; }

        public static DTOProductUrl Factory(DTOLang lang, string brandEsp, string deptEsp, string categoryEsp, string skuEsp)
        {
            DTOProductUrl retval = new DTOProductUrl();
            retval.Lang = lang;

            retval.BrandSegment = DTOUrlSegment.Factory(brandEsp, lang);
            retval.DeptSegment = DTOUrlSegment.Factory(deptEsp, lang);
            retval.CategorySegment = DTOUrlSegment.Factory(categoryEsp, lang);
            retval.SkuSegment = DTOUrlSegment.Factory(skuEsp, lang);

            return retval;
        }

        public static DTOProductUrl Factory(DTOLang lang, DTOUrlSegment.Collection brands, DTOUrlSegment.Collection depts, DTOUrlSegment.Collection categories, DTOUrlSegment.Collection skus)
        {
            DTOProductUrl retval = new DTOProductUrl();
            retval.Lang = lang;

            retval.BrandSegment = LangSegment(lang, brands);
            retval.DeptSegment = LangSegment(lang, depts);
            retval.CategorySegment = LangSegment(lang, categories);
            retval.SkuSegment = LangSegment(lang, skus);

            return retval;
        }

        public string Url( bool absoluteUrl = false)
        {
            string retval = "";
                List<string> parameters = this.Parameters();
                retval = Lang.Domain(absoluteUrl).Url(parameters.ToArray());
            return retval;
        }

        private List<string> Parameters()
        {
            List<string> retval = new List<string>();
            retval.Add(this.BrandSegment.Segment);
            if (this.DeptSegment != null)
                retval.Add(this.DeptSegment.Segment);
            if (this.CategorySegment != null)
                retval.Add(this.CategorySegment.Segment);
            if (this.SkuSegment != null)
                retval.Add(this.SkuSegment.Segment);
            return retval;
        }

        private static DTOUrlSegment LangSegment(DTOLang lang, DTOUrlSegment.Collection segments)
        {
            DTOUrlSegment retval = segments.FirstOrDefault(x => x.Lang.Equals(lang));
            if (retval == null)
                retval = segments.FirstOrDefault(x => x.Lang.Equals(DTOLang.ESP()));
            return retval;
        }


        public class Collection:List<DTOProductUrl>
        {
            public static Collection Factory(DTOUrlSegment.Collection brands, DTOUrlSegment.Collection depts, DTOUrlSegment.Collection categories, DTOUrlSegment.Collection skus)
            {
                Collection retval = new Collection();
                retval.Add(DTOProductUrl.Factory(DTOLang.ESP(), brands, depts, categories, skus));
                retval.Add(DTOProductUrl.Factory(DTOLang.CAT(), brands, depts, categories, skus));
                retval.Add(DTOProductUrl.Factory(DTOLang.ENG(), brands, depts, categories, skus));
                retval.Add(DTOProductUrl.Factory(DTOLang.POR(), brands, depts, categories, skus));
                return retval;
            }
            public string Url(DTOLang lang, bool absoluteUrl = false)
            {
                string retval = "";
                DTOProductUrl oUrl = this.FirstOrDefault(x => x.Lang.Equals(lang));
                if (oUrl != null)
                {
                    List<string> parameters = oUrl.Parameters();
                    retval = lang.Domain(absoluteUrl).Url(parameters.ToArray());
                }
                return retval;
            }

            public string Canonical(DTOLang lang, bool absoluteUrl = false)
            {
                string retval = "";
                DTOProductUrl oUrl = this.FirstOrDefault(x => x.Lang.Equals(lang));
                if (oUrl != null)
                {
                    List<string> parameters = new List<string>();
                    if (lang.id != DTOLang.Ids.POR)
                    {
                        parameters.Add(lang.ISO6391());
                    }
                    parameters.AddRange(oUrl.Parameters());
                    retval = lang.Domain(absoluteUrl).Url(parameters.ToArray());
                }
                return retval;

            }


        }
    }


}
