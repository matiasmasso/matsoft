using System;

namespace DTO
{
    public class DTOApiLog
    {
        public enum Cods
        {
            NotSet,
            BritaxStoreLocator
        }

        public Cods Cod { get; set; }
        public DateTime Fch { get; set; }
        public string Ip { get; set; }

        public static DTOApiLog Factory(DTOApiLog.Cods oCod, string Ip)
        {
            DTOApiLog retval = new DTOApiLog();
            {
                var withBlock = retval;
                withBlock.Cod = oCod;
                withBlock.Ip = Ip;
            }
            return retval;
        }
    }
}
