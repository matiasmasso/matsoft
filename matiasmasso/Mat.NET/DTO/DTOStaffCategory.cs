using System;

namespace DTO
{
    public class DTOStaffCategory : DTOBaseGuid
    {
        public DTOSegSocialGrup SegSocialGrup { get; set; }
        public string Nom { get; set; }
        public int Ord { get; set; }

        public DTOStaffCategory() : base()
        {
        }

        public DTOStaffCategory(Guid oGuid) : base(oGuid)
        {
        }
    }
}
