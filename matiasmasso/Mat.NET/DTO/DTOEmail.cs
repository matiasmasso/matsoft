using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOEmail : DTOBaseGuid
    {
        public string EmailAddress { get; set; }
        public DTOCod BadMail { get; set; }
        public string Obs { get; set; }
        public bool Privat { get; set; }
        public bool Obsoleto { get; set; }

        public enum BadMailErrs
        {
            None,
            FullMailBox,
            ServerNotFound,
            UserNotFound,
            BlackList,
            Altres
        }

        public DTOEmail() : base()
        {
        }

        public DTOEmail(Guid oGuid) : base(oGuid)
        {
        }

        public static System.Net.Mail.MailAddressCollection MailAddressCollection(List<DTOEmail> oEmails)
        {
            System.Net.Mail.MailAddressCollection retval = new System.Net.Mail.MailAddressCollection();
            foreach (DTOEmail oEmail in oEmails)
                retval.Add(oEmail.EmailAddress);
            return retval;
        }

        public static string Recipients(List<DTOEmail> oEmails)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (DTOEmail oEmail in oEmails)
            {
                if (oEmail.UnEquals(oEmails.First()))
                    sb.Append("; ");
                sb.Append(oEmail.EmailAddress);
            }
            string retval = sb.ToString();
            return retval;
        }
    }
}
