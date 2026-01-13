using MatHelperStd;
using Newtonsoft.Json;
using System;
using System.Collections;

namespace DTO
{
    public class DTOTaskSchedule : DTOBaseGuid
    {
        public DTOTask Task { get; set; }
        public bool Enabled { get; set; }
        public bool[] WeekDays { get; set; }
        public Modes Mode { get; set; }
        [JsonIgnore]
        public TimeSpan TimeInterval { get; set; }

        // versió de TimeInterval per enviar en JSON per la Api
        public String ISO8601
        {
            get
            {
                return TimeHelper.ISO8601(TimeInterval);
            }
            set
            {
                TimeInterval = TimeHelper.fromISO8601(value);
            }
        }

        public enum Modes
        {
            GivenTime,
            Interval
        }


        public DTOTaskSchedule() : base()
        {
            WeekDays = new[] { true, true, true, true, true, true, true };
        }

        public DTOTaskSchedule(Guid oGuid) : base(oGuid)
        {
            WeekDays = new[] { true, true, true, true, true, true, true };
        }



        public string FrequencyText()
        {
            string retval = "";
            switch (this.Mode)
            {
                case DTOTaskSchedule.Modes.Interval:
                    {
                        retval = GetIntervalText();
                        break;
                    }

                default:
                    {
                        retval = GetWeekDaysText();
                        break;
                    }
            }
            return retval;
        }

        private string GetIntervalText()
        {
            string retval = "";
            if (TimeInterval.TotalMinutes == 1)
                retval = "cada minuto";
            else
                retval = "cada " + TimeInterval.TotalMinutes.ToString() + " minutos";
            return retval;
        }

        private string GetWeekDaysText()
        {
            DTOLang oLang = new DTOLang(DTOLang.Ids.CAT);
            ArrayList DaysEnabled = new ArrayList();
            ArrayList DaysDisabled = new ArrayList();

            //base 0 = sunday
            for (int i = 0; i < 7; i++)
            {
                if (WeekDays[i])
                    DaysEnabled.Add(i);
                else
                    DaysDisabled.Add(i);
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (DaysEnabled.Count >= DaysDisabled.Count)
            {
                if (DaysDisabled.Count == 0)
                    sb.Append("tots els dies de la setmana");
                else
                {
                    sb.Append("tots els dies menys ");
                    sb.Append(oLang.WeekDay((int)DaysDisabled[0]));
                    if (DaysDisabled.Count > 1)
                    {
                        if (DaysDisabled.Count == 2)
                            sb.Append(" i ");
                        else
                            sb.Append(", ");
                        sb.Append(oLang.WeekDay((int)DaysDisabled[1]));
                        if (DaysDisabled.Count > 2)
                        {
                            sb.Append(" i ");
                            _ = sb.Append(oLang.WeekDay((int)DaysDisabled[2]));
                        }
                    }
                }
            }
            else if (DaysEnabled.Count == 0)
                sb.Append("(cap dia)");
            else
            {
                sb.Append("cada ");
                sb.Append(oLang.WeekDay((int)DaysEnabled[0]));
                if (DaysEnabled.Count > 1)
                {
                    if (DaysEnabled.Count == 2)
                        sb.Append(" i ");
                    else
                        sb.Append(", ");
                    sb.Append(oLang.WeekDay((int)DaysEnabled[1]));
                    if (DaysEnabled.Count > 2)
                    {
                        sb.Append(" i ");
                        sb.Append(oLang.WeekDay((int)DaysEnabled[2]));
                    }
                }
            }

            string retVal = sb.ToString();
            return retVal;
        }



        public static string SpanText(DateTimeOffset DtFch, DTOLang oLang)
        {
            string retval = "";
            if (DtFch != default(DateTimeOffset))
            {
                TimeSpan oTimeSpan = DtFch - DateTimeOffset.Now;
                retval = SpanText(oTimeSpan, oLang);
            }
            return retval;
        }


        public static string SpanText(TimeSpan oTimeSpan, DTOLang oLang)
        {
            string retval = "";
            if (oTimeSpan != default(TimeSpan))
            {
                if (oTimeSpan.Days > 0)
                    retval = string.Format("d'aqui a {0} dies {1} hores {2} min {3}", oTimeSpan.Days, oTimeSpan.Hours, oTimeSpan.Minutes, oTimeSpan.Seconds);
                else if (oTimeSpan.Hours > 2)
                    retval = string.Format("d'aqui a {0} hores {1} min {2}", oTimeSpan.Hours, oTimeSpan.Minutes, oTimeSpan.Seconds);
                else if (oTimeSpan.Hours > 1)
                    retval = string.Format("d'aqui a 1 hora {0} min {1}", oTimeSpan.Minutes, oTimeSpan.Seconds);
                else if (oTimeSpan.Minutes > 0)
                    retval = string.Format("d'aqui a {0} minuts {1}", oTimeSpan.Minutes, oTimeSpan.Seconds);
                else if (oTimeSpan.Seconds > 0)
                    retval = string.Format("d'aqui a {0} segons", oTimeSpan.Seconds);
                else
                    retval = "inminent";
            }
            return retval;
        }

        public string EncodedWeekdays()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (bool item in WeekDays)
                sb.Append(item ? "1" : "0");
            string retval = sb.ToString();
            return retval;
        }

        public static bool[] DecodedWeekdays(string src)
        {
            bool[] retval = new[] { (src.Substring(0, 1) == "1"), (src.Substring(1, 1) == "1"), (src.Substring(2, 1) == "1"), (src.Substring(3, 1) == "1"), (src.Substring(4, 1) == "1"), (src.Substring(5, 1) == "1"), (src.Substring(6, 1) == "1") };
            return retval;
        }
    }
}
