using System;

namespace DTO
{
    public class DTOWebPageAlias : DTOBaseGuid
    {
        public string UrlFrom { get; set; }
        public string UrlTo { get; set; }
        public Domains domain { get; set; }

        public enum Domains
        {
            All,
            OnlyEs,
            OnlyPt
        }


        public DTOWebPageAlias() : base()
        {
        }

        public DTOWebPageAlias(Guid oGuid) : base(oGuid)
        {
        }

        public string FullUrl()
        {
            DTOWebDomain domain = DTOWebDomain.Default(true);
            string retval = domain.Url(this.UrlFrom);
            return retval;
        }
    }
}
