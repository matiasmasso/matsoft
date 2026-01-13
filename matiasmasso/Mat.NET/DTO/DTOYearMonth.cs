using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOYearMonth
    {
        public int Year { get; set; }
        public Months Month { get; set; }

        public decimal Eur { get; set; }

        public enum Months
        {
            NotSet,
            January,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        public DTOYearMonth() : base()
        {
        }

        public DTOYearMonth(string s4DigitsChar) : base()
        {
            if (VbUtilities.isNumeric(s4DigitsChar))
            {
                if (s4DigitsChar.Length == 4)
                {
                    Year = s4DigitsChar.Substring(0, 2).toInteger();
                    Month = (DTOYearMonth.Months)s4DigitsChar.Substring(2, 2).toInteger();
                }
                else if (s4DigitsChar.Length == 6)
                {
                    Year = s4DigitsChar.Substring(0, 4).toInteger();
                    Month = (DTOYearMonth.Months)s4DigitsChar.Substring(4, 2).toInteger();
                }
            }
        }

        public DTOYearMonth(int iYear, Months iMonth, decimal eur = 0) : base()
        {
            Year = iYear;
            Month = iMonth;
            Eur = eur;
        }

        public new bool Equals(object oCandidate)
        {
            bool retval = false;
            if (oCandidate != null)
            {
                if (oCandidate.GetType() == typeof(DTOYearMonth))
                    retval = (((DTOYearMonth)oCandidate).Year == Year & ((DTOYearMonth)oCandidate).Month == Month);
            }
            return retval;
        }

        public int MonthNum()
        {
            return System.Convert.ToInt32(Month);
        }

        public string Tag()
        {
            string sYea = VbUtilities.Format(Year, "0000");
            string sMes = VbUtilities.Format((int)Month, "00");
            string retval = string.Format("Y{0}M{1}", sYea, sMes);
            return retval;
        }

        public string RawTag()
        {
            string retval = string.Format("{0:0000}{1:00}", Year, (int)Month);
            return retval;
        }

        public override string ToString()
        {
            return Caption(DTOLang.CAT());
        }

        public string Caption(DTOLang oLang)
        {
            return string.Format("{0:0000} {1}", Year, oLang.MesAbr((int)Month));
        }

        public static DTOYearMonth Current()
        {
            DTOYearMonth retval = FromFch(DTO.GlobalVariables.Today());
            return retval;
        }
        public static DTOYearMonth Previous(int Months = 1)
        {
            DTOYearMonth retval = FromFch(DTO.GlobalVariables.Today().AddMonths(-Months));
            return retval;
        }

        public static DTOYearMonth FromTag(string sTag)
        {
            DTOYearMonth retval = null;
            if (sTag.Length == 8)
            {
                string sYea = sTag.Substring(1, 4);
                string sMes = sTag.Substring(6, 2);
                if (VbUtilities.isNumeric(sYea) & VbUtilities.isNumeric(sMes))
                {
                    int iMes = System.Convert.ToInt32(sMes);
                    if (iMes >= 1 & iMes <= 12)
                    {
                        int iYea = System.Convert.ToInt32(sYea);
                        Months oMonth = (DTOYearMonth.Months)iMes;
                        retval = new DTOYearMonth(iYea, oMonth);
                    }
                }
            }
            else if (sTag.Length == 6)
            {
                string sYea = sTag.Substring(0, 4);
                string sMes = sTag.Substring(4, 2);
                if (VbUtilities.isNumeric(sYea) & VbUtilities.isNumeric(sMes))
                {
                    int iMes = System.Convert.ToInt32(sMes);
                    if (iMes >= 1 & iMes <= 12)
                    {
                        int iYea = System.Convert.ToInt32(sYea);
                        Months oMonth = (DTOYearMonth.Months)iMes;
                        retval = new DTOYearMonth(iYea, oMonth);
                    }
                }
            }
            return retval;
        }

        public static DTOYearMonth FromFch(DateTime DtFch)
        {
            DTOYearMonth retval = new DTOYearMonth(DtFch.Year, (DTOYearMonth.Months)DtFch.Month);
            return retval;
        }



        public DateTime FchFrom()
        {
            DateTime retval = new DateTime(Year, (int)Month, 1);
            return retval;
        }

        public DateTime FchTo()
        {
            DateTime retval = FchFrom().AddMonths(1).AddDays(-1);
            return retval;
        }

        public DTOYearMonth Addmonths(int iMonthsToAdd)
        {
            DTOYearMonth retval = this;
            if (iMonthsToAdd > 0)
            {
                for (int i = 1; i <= iMonthsToAdd; i++)
                    retval = retval.NextMonth();
            }
            else
                for (int i = 1; i <= Math.Abs(iMonthsToAdd); i++)
                    retval = retval.Previousmonth();
            return retval;
        }

        public DTOYearMonth NextMonth()
        {
            DTOYearMonth retval;
            if (Month < Months.December)
                retval = new DTOYearMonth(Year, Month + 1);
            else
                retval = new DTOYearMonth(Year + 1, Months.January);
            return retval;
        }

        public DTOYearMonth Previousmonth()
        {
            DTOYearMonth retval;
            if (Month > Months.January)
                retval = new DTOYearMonth(Year, Month - 1);
            else
                retval = new DTOYearMonth(Year - 1, Months.December);
            return retval;
        }

        public bool isOutdated6monthsOrMore()
        {
            DateTime DtFch = LastFch();
            int mesos = TimeHelper.monthsdiff(DtFch, DTO.GlobalVariables.Today());
            bool retval = (mesos > 6);
            return retval;
        }

        public DateTime FirstFch()
        {
            DateTime retval = new DateTime(Year, (int)Month, 1);
            return retval;
        }

        public DateTime LastFch()
        {
            DateTime DtFch = FirstFch();
            DateTime retval = DtFch.AddMonths(1).AddDays(-1);
            return retval;
        }

        public static bool HasFch(DTOYearMonth oYearmonth, DateTime DtFch)
        {
            bool retval = false;
            if (DtFch.Year == oYearmonth.Year && DtFch.Month == (int)oYearmonth.Month)
                retval = true;
            return retval;
        }

        public bool IsInRange(DateTime minFch, DateTime maxFch)
        {
            DTOYearMonth oMin = new DTOYearMonth(minFch.Year, (DTOYearMonth.Months)minFch.Month);
            DTOYearMonth oMax = new DTOYearMonth(maxFch.Year, (DTOYearMonth.Months)maxFch.Month);
            string myTag = this.Tag();
            bool retval = myTag.isGreaterOrEqualThan(oMin.Tag()) && myTag.isLowerOrEqualThan(oMax.Tag());
            return retval;
        }

        public bool IsInRange(List<DTOYearMonth> oRange)
        {
            bool retval = oRange.Any(x => x.Equals(this));
            return retval;
        }

        public static List<DTOYearMonth> Range(DTOYearMonth oYearmonthFrom, DTOYearMonth oYearmonthTo)
        {
            List<DTOYearMonth> retval = new List<DTOYearMonth>();
            var item = new DTOYearMonth(oYearmonthFrom.Year, oYearmonthFrom.Month);
            while (item.isLowerOrEqualThan(oYearmonthTo))
            {
                retval.Add(item);
                item = item.Addmonths(1);
            }
            return retval;
        }

        public static List<DTOYearMonth> Range(string tagFrom, string tagTo)
        {
            List<DTOYearMonth> retval = new List<DTOYearMonth>();
            var itemTo = DTOYearMonth.FromTag(tagTo);
            var item = DTOYearMonth.FromTag(tagFrom);
            while (item.isLowerOrEqualThan(itemTo))
            {
                retval.Add(item);
                item = item.NextMonth();
            }
            return retval;
        }

        public bool IsLowerThan(DTOYearMonth oCandidate)
        {
            bool retval = false;
            if (Year < oCandidate.Year)
                retval = true;
            else if (Year == oCandidate.Year & Month < oCandidate.Month)
                retval = true;
            return retval;
        }

        public bool IsGreaterThan(DTOYearMonth oCandidate)
        {
            bool retval = false;
            if (Year > oCandidate.Year)
                retval = true;
            else if (Year == oCandidate.Year & Month > oCandidate.Month)
                retval = true;
            return retval;
        }

        public bool isLowerOrEqualThan(DTOYearMonth oCandidate)
        {
            bool retval = false;
            if (Year < oCandidate.Year)
                retval = true;
            else if (Year == oCandidate.Year & Month <= oCandidate.Month)
                retval = true;
            return retval;
        }

        public bool IsGreaterOrEqualThan(DTOYearMonth oCandidate)
        {
            bool retval = false;
            if (Year > oCandidate.Year)
                retval = true;
            else if (Year == oCandidate.Year & Month >= oCandidate.Month)
                retval = true;
            return retval;
        }

        public int DaysInmonth()
        {
            int retval = System.DateTime.DaysInMonth(Year, (int)Month);
            return retval;
        }

        public int DaysToEndmonth()
        {
            int retval = (FchTo() - DTO.GlobalVariables.Today()).Days;
            return retval;
        }

        public List<DTOYearMonth> Last12Yearmonths()
        {
            List<DTOYearMonth> retval = new List<DTOYearMonth>();
            DTOYearMonth item = this;
            for (int i = 1; i <= 12; i++)
            {
                retval.Insert(0, item);
                item = item.Previousmonth();
            }
            return retval;
        }

        public string Formatted(DTOLang oLang)
        {
            string retval = string.Format("{0} {1}", oLang.MesAbr((int)Month), Year);
            return retval;
        }

        public static string Formatted(DTOYearMonth oYearMonth, DTOLang oLang)
        {
            string retval = string.Format("{0} {1}", oLang.MesAbr((int)oYearMonth.Month), oYearMonth.Year);
            return retval;
        }

        public static int MonthsDiff(DTOYearMonth oFirstYearmonth, DTOYearMonth oSecondYearmonth)
        {
            int iFirstmonth = 12 * oFirstYearmonth.Year + (int)oFirstYearmonth.Month;
            int iSecondmonth = 12 * oSecondYearmonth.Year + (int)oSecondYearmonth.Month;
            int retval = iSecondmonth - iFirstmonth;
            return retval;
        }

        public static DTOYearMonth Max(IEnumerable<DTOYearMonth> values)
        {
            var year = values.Max(x => x.Year);
            var month = values.Where(x => x.Year == year).Max(y => y.Month);
            var retval = values.FirstOrDefault(x => x.Year == year & x.Month == month);
            return retval;
        }

        public static DTOYearMonth Min(IEnumerable<DTOYearMonth> values)
        {
            var year = values.Min(x => x.Year);
            var month = values.Where(x => x.Year == year).Min(y => y.Month);
            var retval = values.FirstOrDefault(x => x.Year == year & x.Month == month);
            return retval;
        }
    }
}
