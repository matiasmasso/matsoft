using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTODept : DTOProduct
    {
        private DTOLangText _LangNom;

        public new DTOProductBrand Brand { get; set; }
        public int Ord { get; set; }
        public List<DTOCnap> cnaps { get; set; }
        [JsonIgnore]
        public Byte[] Banner { get; set; }

        public const int IMAGEWIDTH = 410;
        public const int IMAGEHEIGHT = 410;

        public List<DTOProductCategory> Categories { get; set; }
        public DTOUsrLog UsrLog { get; set; }

        public DTODept() : base()
        {
            base.SourceCod = SourceCods.Dept;
            cnaps = new List<DTOCnap>();
            Categories = new List<DTOProductCategory>();
            UsrLog = new DTOUsrLog();
        }

        public DTODept(Guid oGuid) : base(oGuid)
        {
            base.SourceCod = SourceCods.Dept;
            cnaps = new List<DTOCnap>();
            _LangNom = new DTOLangText(base.Guid, DTOLangText.Srcs.ProductNom);
            Categories = new List<DTOProductCategory>();
            UsrLog = new DTOUsrLog();
        }

        public static DTODept Factory(DTOProductBrand oBrand)
        {
            DTODept retval = new DTODept();
            retval.Brand = oBrand;
            return retval;
        }

        public string getUrl(DTOProduct.Tabs oTab = DTOProduct.Tabs.general, bool AbsoluteUrl = false, DTOLang lang = null)
        {
            var sSegment = MatHelperStd.UrlHelper.EncodedUrlSegment(base.Nom.Esp);

            var retval = MmoUrl.Factory(AbsoluteUrl, DTOProductBrand.urlSegment(this.Brand), sSegment);

            if (oTab != DTOProduct.Tabs.general)
                retval = retval + "/" + TabUrlSegment(oTab).Tradueix(lang);

            return retval;
        }

        public string BannerUrl()
        {
            return MmoUrl.ApiUrl("dept/banner", base.Guid.ToString());
        }

        public string url(DTOFilter.Item filterItem, Boolean AbsoluteUrl = false)
        {
            var sSegment = MatHelperStd.UrlHelper.EncodedUrlSegment(base.Nom.Esp);

            var retval = MmoUrl.Factory(AbsoluteUrl, DTOProductBrand.urlSegment(this.Brand), sSegment);
            return retval;
        }

        public new class Collection : List<DTODept>
        {

        }
    }
}
