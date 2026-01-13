using System;

namespace DTO.Models.Compact
{
    public class UsrLog
    {
        public Base.GuidNom UsrCreated { get; set; }

        public UsrLog(Guid usrCreatedGuid, string usrCreatedNom = "")
        {
            UsrCreated = new Base.GuidNom(usrCreatedGuid, usrCreatedNom);
        }
    }
}
