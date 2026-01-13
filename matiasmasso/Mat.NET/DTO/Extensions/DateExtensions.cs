using System;
using System.Globalization;

namespace DTO
{
    public static class DateExtensions
    {

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int weekOfYear(this DateTime value)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(value);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                value = value.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }


        public static bool isBetween(this DateTime value, DateTime fchStart, DateTime fchEnd)
        {
            bool retval = false;
            if (fchStart == null && fchEnd == null)
                retval = false;
            else if (fchStart == null)
                retval = value <= fchEnd;
            else if (fchEnd == null)
                retval = value >= fchStart;
            else
                retval = (fchStart <= value) && (value <= fchEnd);
            return retval;
        }

    }


}
