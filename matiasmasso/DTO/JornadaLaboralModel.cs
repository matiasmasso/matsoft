using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace DTO
{
    public class JornadaLaboralModel:BaseGuid
    {
        public DateTime FchFrom { get; set; }
        public DateTime? FchTo { get; set; }

        public Guid Staff { get; set; }


        public string DayFullName(LangDTO lang)
        {
            var culture = new CultureInfo( lang.Culture2Digits());
            var weekday = FchFrom.DayOfWeek;
            var weekdayName = culture.DateTimeFormat.DayNames[(int)weekday];
            var retval = string.Format("{0:dd/MM/yy}, {1}", FchFrom, weekdayName);
                return retval;
        }
    }
}
