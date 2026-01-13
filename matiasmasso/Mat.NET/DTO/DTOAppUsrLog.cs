using System;

namespace DTO
{
    public class DTOAppUsrLog
    {
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }

        public enum IOs
        {
            notset,
            entrance,
            exit
        }
        public class Request : DTOBaseGuid
        {
            public DTOApp.AppTypes AppId { get; set; }
            public String AppVersion { get; set; }
            public String OS { get; set; }
            public String DeviceModel { get; set; }
            public String DeviceId { get; set; }
            public Guid UserGuid { get; set; } // this is what IOS App is sending
            public DTOGuidNom User { get; set; } // this is to read logs
            public DateTime Fch { get; set; }
            public IOs IO { get; set; }
        }

        public class Response
        {
            public DTORol Rol { get; set; }
            public String AppMinVersion { get; set; }
            public String AppLastVersion { get; set; }
        }
    }
}
