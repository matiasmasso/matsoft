using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOHourInOut
    {
        public int HourFrom { get; set; }
        public int MinuteFrom { get; set; }
        public int HourTo { get; set; }
        public int MinuteTo { get; set; }

        public DTOHourInOut(int hourFrom = 9, int minuteFrom = 0, int hourTo = 14, int minuteTo = 0)
        {
            HourFrom = hourFrom;
            MinuteFrom = minuteFrom;
            HourTo = hourTo;
            MinuteTo = minuteTo;
        }

        public string FormattedFrom()
        {
            return string.Format("{0:00}:{1:00}", HourFrom, MinuteFrom);
        }
        public string FormattedTo()
        {
            return string.Format("{0:00}:{1:00}", HourTo, MinuteTo);
        }
        public string Formatted()
        {
            return string.Format("{0}-{1}", FormattedFrom(), FormattedTo());
        }

        public TimeSpan Duration()
        {
            int minutesFrom = 60 * HourFrom + MinuteFrom;
            int minutesTo = 60 * HourTo + MinuteTo;
            double minutesGap = minutesTo - minutesFrom;
            TimeSpan retval = TimeSpan.FromMinutes(minutesGap);
            return retval;
        }

        public class Collection : List<DTOHourInOut>
        {
            public string Formatted()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DTOHourInOut item in this)
                {
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(item.Formatted());
                }
                string retval = sb.ToString();
                return retval;
            }

            public TimeSpan Duration()
            {
                TimeSpan retval = TimeSpan.FromMinutes(0);
                foreach (DTOHourInOut item in this)
                {
                    retval = retval.Add(item.Duration());
                }
                return retval;
            }

        }

    }
}
