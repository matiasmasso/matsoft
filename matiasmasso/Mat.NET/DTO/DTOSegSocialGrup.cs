using System;

namespace DTO
{
    public class DTOSegSocialGrup : DTOBaseGuid
    {
        public string nom { get; set; }
        public int id { get; set; }

        public DTOSegSocialGrup() : base()
        {
        }

        public DTOSegSocialGrup(Guid oGuid) : base(oGuid)
        {
        }
    }
}
