using System;

namespace DTO
{
    public class DTOMailingLog
    {
        public DTOUser user { get; set; }
        public DateTime fch { get; set; }

        public DateTime roundedFch() // arrodoneix als 5 minuts mes propers
        {
            // split into date + time parts
            var datepart = fch.Date;
            var timepart = fch.TimeOfDay;

            // round time to the nearest 5 minutes
            timepart = TimeSpan.FromMinutes(Math.Floor((timepart.TotalMinutes + 2.5) / 5.0) * 5.0);

            // combine the parts
            var retval = datepart.Add(timepart);
            return retval;
        }
    }
}
