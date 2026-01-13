using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOEdiGenral : DTOBaseGuid
    {
        public DateTime Fch { get; set; }
        public IOCodes IOCod { get; set; }
        public DTOContact Contact { get; set; }
        public string Docnum { get; set; }
        public string Text { get; set; }
        public enum IOCodes
        {
            Inbox,
            Outbox
        }

        public DTOEdiGenral() : base() { }
        public DTOEdiGenral(Guid guid) : base(guid) { }

        public static DTOEdiGenral Factory(IOCodes ioCod, DateTime fch, DTOContact contact, string docnum, string text)
        {
            DTOEdiGenral retval = new DTOEdiGenral();
            retval.IOCod = ioCod;
            retval.Fch = fch;
            retval.Contact = contact;
            retval.Docnum = docnum;
            retval.Text = text;
            return retval;
        }
        public class Collection : List<DTOEdiGenral>
        {

        }
    }


}
