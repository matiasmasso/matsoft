using System;

namespace DTO
{
    public class DTOStaffHoliday : DTOBaseGuid
    {
        public DTOEmp Emp { get; set; }
        public DTOStaff Staff { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public Cods Cod { get; set; }
        public string Obs { get; set; }


        public enum Cods
        {
            Treball,
            Festiu,
            Pont,
            Personal,
            Recuperable
        }

        public DTOStaffHoliday() : base()
        {
        }

        public DTOStaffHoliday(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOStaffHoliday Factory(DTOEmp oEmp, DTOStaff oStaff)
        {
            DTOStaffHoliday retval = new DTOStaffHoliday();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
                withBlock.Staff = oStaff;
                withBlock.FchFrom = DTO.GlobalVariables.Today().Date;
                withBlock.FchTo = DTO.GlobalVariables.Today().Date;
                withBlock.Cod = Cods.Festiu;
            }
            return retval;
        }

        public string TitularNom()
        {
            if (Staff == null)
                return Emp.Nom;
            else
                return Staff.Abr;
        }

        public bool HasSpecificHourFrom()
        {
            return (FchFrom.Hour != 0 | FchFrom.Minute != 0);
        }
        public bool HasSpecificHourTo()
        {
            return (FchTo.Hour != 23 | FchTo.Minute != 59);
        }

        public bool SeveralDays()
        {
            return FchFrom.DayOfYear != FchTo.DayOfYear;
        }
    }
}
