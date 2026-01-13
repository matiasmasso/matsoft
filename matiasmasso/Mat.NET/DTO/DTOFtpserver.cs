using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOFtpserver
    {
        public DTOGuidNom Owner { get; set; }
        public String Servername { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public Int16 Port { get; set; }
        public Boolean PassiveMode { get; set; }
        public Boolean SSL { get; set; }
        public List<Path> Paths { get; set; }
        public Boolean IsLoaded { get; set; }

        public DTOFtpserver()
        {
            Port = 21;
            PassiveMode = true;
        }
        public DTOFtpserver(DTOBaseGuid owner)
        {
            Owner = new DTOGuidNom(owner.Guid);
            if (owner is DTOGuidNom)
                Owner.Nom = ((DTOGuidNom)owner).Nom;
            Port = 21;
            PassiveMode = true;
            Paths = new List<Path>();
        }

        public class Path
        {
            public Cods cod;
            public string value;

            public enum Cods
            {
                NotSet,
                Orders,
                Ordrsp,
                Desadv,
                Inbox,
                Outbox
            }
        }
    }
}
