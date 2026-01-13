using Newtonsoft.Json;
using System;

namespace DTO
{
    public class DTOCert : DTOBaseGuid
    {
        [JsonIgnore]
        public byte[] Stream { get; set; }
        [JsonIgnore]
        public Byte[] Image { get; set; }
        public string Pwd { get; set; }
        public string Ext { get; set; }
        public DateTime Caduca { get; set; }
        public Uri ImageUri { get; set; }

        public DTOCert() : base()
        {
        }

        public DTOCert(Guid oGuid) : base(oGuid)
        {
        }

        public System.IO.MemoryStream memoryStream()
        {
            return new System.IO.MemoryStream(Stream);
        }
    }
}
