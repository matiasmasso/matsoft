using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class FchModel
    {
        public string? Fch1 { get; set; }
        public string? Fch2 { get; set; }
        public Qualifiers? Qualifier { get; set; } = Qualifiers.EXC;

        public enum Qualifiers
        {
            EXC,
            ABT,
            CAL,
            EST,
            AFT,
            BEF,
            BET
        }

        public FchModel(string? fch1 = null, string? fch2 = null, Qualifiers? qualifier = null)
        {
            Fch1 = fch1;
            Fch2 = fch2;
            Qualifier = qualifier;
        }
        public static bool IsValid(string? fch)
        {
            var retval = false;
            if (fch == null)
                retval = true;
            else if (fch.Length >= 4)
            {
                var year1 = fch.Substring(0, 1);
                var year2 = fch.Substring(1, 1);
                var year3 = fch.Substring(2, 1);
                var year4 = fch.Substring(3, 1);
                int iYear = 0;
                int iMonth = 0;
                retval = (year1 == "1" && Char.IsDigit(fch, 1)) || fch.Substring(0, 2) == "20";
                if (year3 != "X" && !Char.IsDigit(fch, 2)) retval = false;
                if (year4 != "X" && !Char.IsDigit(fch, 3)) retval = false;

                if (retval && fch.Length >= 6)
                {
                    var month = fch.Substring(4, 2);
                    if (month != "XX")
                    {
                        iYear = Convert.ToInt16(fch.Substring(0, 4));
                        iMonth = Convert.ToInt16(month);
                        retval = iMonth > 0 && iMonth <= 12;
                    }

                    if (retval && fch.Length >= 8)
                    {
                        var day = fch.Substring(6, 2);
                        if (day != "XX")
                        {
                            var iDay = Convert.ToInt16(day);
                            var formats = new[] { "dd/MM/yyyy" };
                            var tempDate = $"{iDay:00}/{iMonth:00}/{iYear:0000}";
                            retval = DateTime.TryParseExact(tempDate, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _);
                        }
                    }
                }
            }
            return retval;
        }

        public FchModel AddYears(int years)
        {
            var retval = new FchModel();
            if (!string.IsNullOrEmpty(Fch1) && Fch1.Length >= 4)
            {
                string tmp = Fch1?.Replace("X", "0") ?? string.Empty;
                if (int.TryParse(tmp.Truncate(4), out int year))
                    retval.Fch1 = (year + years).ToString();
            }
            return retval;
        }

        public override string? ToString()
        {
            string? retval = null;
            if (!string.IsNullOrEmpty(Fch2))
                retval = $"{Fch1}-{Fch2}";
            else
                switch (Qualifier)
                {
                    case null:
                    case Qualifiers.EXC:
                        retval = Fch1;
                        break;
                    case Qualifiers.ABT:
                        retval = $"ABT {Fch1}";
                        break;
                    case Qualifiers.CAL:
                        retval = $"CAL {Fch1}";
                        break;
                    case Qualifiers.EST:
                        retval = $"EST {Fch1}";
                        break;
                    case Qualifiers.AFT:
                        retval = $"> {Fch1}";
                        break;
                    case Qualifiers.BEF:
                        retval = $"< {Fch1}";
                        break;
                    case Qualifiers.BET:
                        retval = $"{Fch1}-{Fch2}";
                        break;
                }
            return retval;
        }
    }
}
