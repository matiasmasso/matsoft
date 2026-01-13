using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Amt
    {
        public decimal? Eur { get; set; }
        public Curs? Cur { get; set; }
        public decimal? Value { get; set; }

        public enum Curs
        {
            EUR,
            USD,
            GBP,
            ESP
        }

        public Amt():base(){}

        public Amt(decimal value)
        {
            Value = value;
            Cur = Curs.EUR;
            Eur = value;
        }
        public Amt(decimal value, Curs cur = Curs.EUR, decimal? eur = null)
        {
            Value = value;
            Cur = cur;
            Eur = eur == null && cur == Curs.EUR ? value : eur;
        }

        public int Decimals()
        {
            return Cur == Curs.ESP ? 0 : 2;
        }

        public Amt(decimal value, string sCur = "EUR", decimal? eur = null)
        {
            Value = value;
            Curs? cur = (Curs)Enum.Parse(typeof(Curs), sCur);
            Cur = cur == null ? Curs.EUR : (Curs)cur!;
            Eur = eur == null && cur == Curs.EUR ? value : eur;
        }

        public string CurFormatted()
        {
            var retval = string.Empty;
            switch(Cur)
            {
                case Curs.EUR:
                    retval = Eur?.ToString("N2") + " €";
                    break;
                case Curs.USD:
                    retval = Value?.ToString("N2") + " $";
                    break;
                case Curs.GBP:
                    retval = Value?.ToString("N2") + " £";
                    break;
                case Curs.ESP:
                    retval = Value?.ToString("N0") + " Pts";
                    break;
                default:
                    retval = Eur?.ToString("N2") + " €";
                    break;
            }
            return retval;
        }

        public Amt Clone() => new Amt()
        {
            Eur = Eur,
            Value = Value,
            Cur = Cur
        };

        public Amt Add(Amt amtToAdd)
        {
            if (amtToAdd != null)
            {
                if (Eur == 0 & Value == 0)
                    Cur = amtToAdd.Cur;
                Eur += amtToAdd.Eur;
                Value += amtToAdd.Value;
            }
            return this.Clone();
        }

        public Amt Percent(decimal DcTipus) => new Amt
        {
            Eur = Math.Round((Eur ?? 0) * DcTipus / 100, Decimals(), MidpointRounding.AwayFromZero),
            Value =  Math.Round((Value ?? 0) * DcTipus / 100, Decimals(), MidpointRounding.AwayFromZero),
            Cur = Cur
        };

        public bool HasValue()=>Value != 0 || Eur != 0;
        public new string ToString() => String.Format("{DTO.Amt: {0}", CurFormatted());
    }
}
