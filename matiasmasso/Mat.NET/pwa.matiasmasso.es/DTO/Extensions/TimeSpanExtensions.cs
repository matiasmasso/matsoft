using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class TimeSpanExtensions
{
    public static string ToISO8601Duration(this TimeSpan value)
    {
        var sb = new StringBuilder("P"); // period
        sb.Append("T");
        if (value.TotalHours >= 1)
            sb.AppendFormat("H{0:N0}", value.TotalHours);
        int minutes = (int)value.TotalMinutes - 60 * ((int)value.TotalHours);
        if (minutes >= 1)
            sb.AppendFormat("M{0:N0}", (int)minutes);
        int seconds = (int)value.TotalSeconds - 60 * (int)value.TotalMinutes;
        if (seconds >= 1)
            sb.AppendFormat("S{0:N0}", (int)minutes);
        return sb.ToString();
    }
}
