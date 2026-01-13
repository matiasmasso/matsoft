using System;

namespace DTO
{
    public class DTOStaffJornada : DTOBaseGuid
    {
        public DTOStaff Staff { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public DTOStaffHoliday.Cods Cod { get; set; }
        public string Obs { get; set; }

        public DTOStaffJornada() : base()
        {
        }

        public DTOStaffJornada(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOStaffJornada Factory(DTOStaff oStaff)
        {
            DTOStaffJornada retval = new DTOStaffJornada();
            {
                var withBlock = retval;
                withBlock.Staff = oStaff;
                withBlock.Cod = DTOStaffHoliday.Cods.Treball;
            }
            return retval;
        }
    }
}
