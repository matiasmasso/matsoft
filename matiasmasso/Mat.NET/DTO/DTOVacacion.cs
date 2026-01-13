using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOVacacion
    {
        public DTOMonthDay MonthDayFrom { get; set; }
        public DTOMonthDay MonthDayTo { get; set; }
        public DTOMonthDay MonthDayResult { get; set; }

        public enum Segments
        {
            FromMes,
            FromDia,
            UntilMes,
            UntilDia,
            ForwardMes,
            ForwardDia
        }

        public static bool Match(DTOVacacion oVacacion, DateTime SrcVto)
        {
            DateTime FchFrom = new DateTime(SrcVto.Year, oVacacion.MonthDayFrom.Month, oVacacion.MonthDayFrom.Day);
            DateTime FchTo = new DateTime(SrcVto.Year, oVacacion.MonthDayTo.Month, oVacacion.MonthDayTo.Day);
            if (FchTo < FchFrom)
                FchTo = FchTo.AddYears(1);
            bool retval = (SrcVto >= FchFrom && SrcVto <= FchTo);
            return retval;
        }

        public static string Text(DTOVacacion item, DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("{0} {1:00}/{2:00}", oLang.Tradueix("del", "del", "from"), item.MonthDayFrom.Day, item.MonthDayFrom.Month);
            sb.AppendFormat(" {0} {1:00}/{2:00}", oLang.Tradueix("al", "al", "to"), item.MonthDayTo.Day, item.MonthDayTo.Month);
            if (item.MonthDayResult.Month == 0 & item.MonthDayResult.Day == 0)
                sb.Append(oLang.Tradueix(" aplaza 30 dias", " aplaça 30 dies", " add 30 days"));
            else
                sb.AppendFormat(" {0} {1:00}/{2:00}", oLang.Tradueix("aplaza al", "aplaça al", "delay to"), item.MonthDayResult.Day, item.MonthDayResult.Month);
            string retval = sb.ToString();
            return retval;
        }


        public static DateTime Result(List<DTOVacacion> items, DateTime SrcVto)
        {
            DateTime retval = SrcVto;
            if (items != null)
            {
                foreach (DTOVacacion item in items)
                {
                    DateTime FchFrom = new DateTime(SrcVto.Year, item.MonthDayFrom.Month, item.MonthDayFrom.Day);
                    DateTime FchTo = new DateTime(SrcVto.Year, item.MonthDayTo.Month, item.MonthDayTo.Day);
                    if (FchTo < FchFrom)
                        FchTo = FchTo.AddYears(1);
                    if (SrcVto >= FchFrom && SrcVto <= FchTo)
                    {
                        if (item.MonthDayResult.Month == 0)
                        {
                            if (item.MonthDayResult.Day == 0)
                                // default: aplaza un mes
                                retval = SrcVto.AddMonths(1);
                            else
                                // mismo dia del mes indicado
                                retval = new DateTime(FchTo.Year, item.MonthDayResult.Month, SrcVto.Day);
                        }
                        else
                            retval = new DateTime(FchTo.Year, item.MonthDayResult.Month, item.MonthDayResult.Day);
                        break;
                    }
                }
            }
            return retval;
        }
    }
}
