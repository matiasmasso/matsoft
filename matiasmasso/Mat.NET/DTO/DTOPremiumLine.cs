using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOPremiumLine : DTOBaseGuid
    {
        public string nom { get; set; }
        public DateTime fch { get; set; }
        public Codis Codi { get; set; }

        public List<DTOProductCategory> products { get; set; }

        public enum Wellknowns
        {
            notSet,
            romerPremiumLine
        }

        public enum Codis
        {
            notSet,
            defaultInclude // Inclou els clients que no estiguin exclosos
    ,
            defaultExclude // exclou els clients que no estiguin inclosos
        }

        public DTOPremiumLine() : base()
        {
            products = new List<DTOProductCategory>();
        }

        public DTOPremiumLine(Guid oGuid) : base(oGuid)
        {
            products = new List<DTOProductCategory>();
        }

        public static DTOPremiumLine Wellknown(DTOPremiumLine.Wellknowns owellknown)
        {
            DTOPremiumLine retval = null;
            switch (owellknown)
            {
                case DTOPremiumLine.Wellknowns.romerPremiumLine:
                    {
                        retval = new DTOPremiumLine(new Guid("95B72BC7-60B3-4D57-9997-B1A38DDA2A89"));
                        break;
                    }
            }
            return retval;
        }

        public string UrlStoreLocator(DTOLang lang = null, bool absoluteUrl = true)
        {
            string retval = DTOWebDomain.Factory(lang, absoluteUrl).Url("StoreLocator/premium", base.Guid.ToString());
            return retval;
        }
    }

    public class DTOPremiumCustomer : DTOBaseGuid
    {
        public DTOPremiumLine premiumLine { get; set; }
        public DTOCustomer customer { get; set; }
        public Codis codi { get; set; }

        public string obs { get; set; }
        public DTODocFile docFile { get; set; }
        public DTOUsrLog UsrLog { get; set; }

        public enum Codis
        {
            notSet,
            included,
            excluded
        }

        public DTOPremiumCustomer() : base()
        {
            UsrLog = new DTOUsrLog();
        }

        public DTOPremiumCustomer(Guid oGuid) : base(oGuid)
        {
            UsrLog = new DTOUsrLog();
        }
    }
}
