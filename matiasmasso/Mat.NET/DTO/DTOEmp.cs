namespace DTO
{
    public class DTOEmp
    {
        public class Compact
        {
            public Ids Id { get; set; }
            public string Nom { get; set; }
        }

        public Ids Id { get; set; }
        public string Nom { get; set; }
        public string Abr { get; set; }
        public string Cnae { get; set; }
        public string DadesRegistrals { get; set; }
        public string Domini { get; set; }
        public string Ip { get; set; }
        public string MsgFrom { get; set; }
        public string MailboxPwd { get; set; }
        public string MailboxUsr { get; set; }
        public int MailBoxPort { get; set; }
        public string MailBoxSmtp { get; set; }
        public DTOContact Org { get; set; }
        public DTOMgz Mgz { get; set; }
        public DTOTaller Taller { get; set; }


        public bool IsNew { get; set; }
        public bool IsLoaded { get; set; }

        public enum Ids
        {
            NotSet,
            MatiasMasso,
            FrontFred,
            BebeDigital,
            PicaPaquet,
            EspaiBlau,
            MMC,
            Tatita,
            iWannaBit,
            Prim,
            Rosa,
            Horta
        }

        public DTOEmp() : base()
        {
            IsNew = true;
        }

        public DTOEmp(Ids oId) : base()
        {
            Id = oId;
        }

        public DTOEmp Trimmed()
        {
            DTOEmp retval = new DTOEmp(Id);
            return retval;
        }



        public DTOCountry defaultCountry()
        {
            var retval = Org.Address.Zip.Location.Zona.Country;
            return retval;
        }

        public new bool Equals(object oEmp)
        {
            bool retval = false;
            if (oEmp != null)
            {
                if (oEmp is DTOEmp)
                    retval = ((DTOEmp)oEmp).Id == this.Id;
            }
            return retval;
        }

        public string absoluteUrl(params string[] UrlSegments)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("https://www.{0}", this.Domini);

            for (int intLoopIndex = 0; intLoopIndex < UrlSegments.Length; intLoopIndex++)
            {
                string sSegment = UrlSegments[intLoopIndex].Trim();
                if (!sb.ToString().EndsWith("/"))
                    sb.Append("/");
                if (sSegment.StartsWith("/"))
                    sSegment = sSegment.Substring(1);
                sb.Append(sSegment);
            }

            string retval = sb.ToString();
            return retval;
        }
    }
}
