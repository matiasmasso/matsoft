using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOMailMessage
    {
        public IEnumerable<string> To { get; set; }

        public IEnumerable<string> Cc { get; set; }
        public IEnumerable<string> Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BodyUrl { get; set; }
        public MessageBodyFormats BodyFormat { get; set; }
        public List<Attachment> Attachments { get; set; }


        public enum wellknownRecipients
        {
            Admin,
            Info,
            Cuentas,
            Portugal
        }

        public enum MessageBodyFormats
        {
            NotSet,
            ASCII,
            Html
        }

        public DTOMailMessage() : base()
        {
            Cc = new List<string>();
            Bcc = new List<string>();
            BodyFormat = MessageBodyFormats.Html;
            Attachments = new List<Attachment>();
        }

        public static DTOMailMessage Factory(string recipient = "", string Subject = "", string Body = "", DTOMailMessage.MessageBodyFormats bodyFormat = MessageBodyFormats.Html)
        {
            DTOMailMessage retval = new DTOMailMessage();
            {
                if (recipient.isNotEmpty())
                    retval.To = new string[] { recipient }.ToList();

                retval.Subject = Subject;
                retval.Body = Body;
                retval.BodyFormat = bodyFormat;
            }
            return retval;
        }

        public static DTOMailMessage Factory(List<string> sRecipients, string Subject = "", string Body = "")
        {
            DTOMailMessage retval = new DTOMailMessage();
            {
                var withBlock = retval;
                withBlock.To = sRecipients;
                withBlock.Subject = Subject;
                withBlock.Body = Body;
            }
            return retval;
        }

        public void AddAttachment(string friendlyName, byte[] oByteArray)
        {
            var oAttachment = Attachment.Factory(friendlyName, oByteArray);
            Attachments.Add(oAttachment);
        }

        public void AddAttachment(string path, string sFriendlyName = "")
        {
            if (sFriendlyName == "")
                sFriendlyName = System.IO.Path.GetFileName(path);
            var oAttachment = Attachment.Factory(sFriendlyName, path);
            Attachments.Add(oAttachment);
        }

        public static string TemplateBodyUrl(DTODefault.MailingTemplates oTemplate, params string[] UrlSegments)
        {
            List<string> oSegments = new List<string>();
            oSegments.Add("mail");
            oSegments.Add(oTemplate.ToString());
            foreach (var s in UrlSegments)
                oSegments.Add(s);
            string retval = MmoUrl.Factory(true, oSegments.ToArray());
            return retval;
        }


        public static string wellknownAddress(DTOMailMessage.wellknownRecipients oRecipient)
        {
            string retval = "";
            switch (oRecipient)
            {
                case DTOMailMessage.wellknownRecipients.Admin:
                    {
                        retval = "matias@matiasmasso.es";
                        break;
                    }

                case DTOMailMessage.wellknownRecipients.Info:
                    {
                        retval = "info@matiasmasso.es";
                        break;
                    }

                case DTOMailMessage.wellknownRecipients.Cuentas:
                    {
                        retval = "cuentas@matiasmasso.es";
                        break;
                    }

                case DTOMailMessage.wellknownRecipients.Portugal:
                    {
                        retval = "portugal@matiasmasso.es";
                        break;
                    }
            }
            return retval;
        }

        public string ConcatenatedCommaTo()
        {
            var retval = string.Join(",", To.ToArray());
            return retval;
        }

        public string ConcatenatedSemicolonTo()
        {
            var retval = string.Join(";", To.ToArray());
            return retval;
        }

        public class Attachment
        {
            public string Path { get; set; }
            public string Friendlyname { get; set; }
            public byte[] ByteArray { get; set; }

            public static Attachment Factory(string friendlyName, string path)
            {
                Attachment retval = new Attachment();
                {
                    var withBlock = retval;
                    withBlock.Friendlyname = friendlyName;
                    withBlock.Path = path;
                }
                return retval;
            }

            public static Attachment Factory(string friendlyName, byte[] oByteArray)
            {
                Attachment retval = new Attachment();
                {
                    var withBlock = retval;
                    withBlock.Friendlyname = friendlyName;
                    withBlock.ByteArray = oByteArray;
                }
                return retval;
            }
        }
    }
}
