using Newtonsoft.Json;

using System;

namespace DTO
{
    public class DTOVisaEmisor : DTOBaseGuid
    {
        public string Nom { get; set; }
        [JsonIgnore]
        public Byte[] Logo { get; set; }

        public DTOVisaEmisor() : base()
        {
        }

        public DTOVisaEmisor(Guid oGuid) : base(oGuid)
        {
        }
    }
}
