using System;

namespace DTO
{
    public class DTOStaffLogTemplate : DTOBaseGuid
    {
        public DTOStaff Staff { get; set; }
        public DayOfWeek WeekDay { get; set; }
        public DTOHourInOut.Collection Items { get; set; }

        public DTOStaffLogTemplate() : base()
        {
            Items = new DTOHourInOut.Collection();
        }

        public static DTOStaffLogTemplate Factory(DTOStaff staff, DayOfWeek weekday)
        {
            DTOStaffLogTemplate retval = new DTOStaffLogTemplate();
            retval.Staff = staff;
            retval.WeekDay = weekday;
            return retval;
        }

    }
}
