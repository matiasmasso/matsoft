using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOCur
    {
        public string Tag { get; set; }
        public string Symbol { get; set; }
        public int Decimals { get; set; } = 2;
        public DTOCurExchangeRate ExchangeRate { get; set; }
        public bool Obsoleto { get; set; }


        public enum Ids
        {
            NotSet,
            EUR,
            USD,
            GBP,
            ESP
        }

        public DTOCur() : base()
        {
        }

        public static DTOCur Factory(string sTag)
        {
            DTOCur retval = null;
            if (DTOApp.Current == null || DTOApp.Current.Curs == null) // for user controls in design mode
            {
                retval = new DTOCur();
                retval.Tag = "EUR";
            }
            else

                retval = DTOApp.Current.Curs.FirstOrDefault(x => x.Tag == sTag.ToUpper());
            return retval;
        }


        public DTOCur clon()
        {
            var retval = DTOCur.Factory(Tag);
            {
                var withBlock = retval;
                withBlock.Symbol = Symbol;
                withBlock.Decimals = Decimals;
                withBlock.ExchangeRate = ExchangeRate;
                withBlock.Obsoleto = Obsoleto;
            }
            return retval;
        }

        public static DTOCur Eur()
        {
            return DTOCur.Factory(DTOCur.Ids.EUR.ToString());
        }

        public static DTOCur usd()
        {
            return DTOCur.Factory(DTOCur.Ids.USD.ToString());
        }

        public static DTOCur gbp()
        {
            return DTOCur.Factory(DTOCur.Ids.GBP.ToString());
        }

        public string formatString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("#,##0");
            if (Decimals > 0)
                sb.Append("." + string.Concat(Enumerable.Repeat("0", Decimals)));
            if (Symbol.isNotEmpty())
                sb.Append(" " + Symbol);
            else if (Tag == "EUR")
                sb.Append(" €");
            else
                sb.Append(" " + Tag);
            sb.Append(";-" + sb.ToString() + ";#");
            string retval = sb.ToString();
            return retval;
        }


        public bool UnEquals(DTOCur oCur)
        {
            bool retVal = Tag != oCur.Tag;
            return retVal;
        }


        public DTOAmt amtFromEuros(decimal euros, DTOCurExchangeRate oExchangeRate = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oExchangeRate == null)
                oExchangeRate = ExchangeRate;
            decimal Val = Math.Round(euros * oExchangeRate.Rate, 2, MidpointRounding.AwayFromZero);
            var retval = DTOAmt.Factory(euros, this, Val);
            return retval;
        }

        public string exchangeText(DTOCurExchangeRate oExchangeRate = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oExchangeRate == null)
                oExchangeRate = ExchangeRate;
            string retval = string.Format("{0} {1}/€ {2:dd/MM/yy}", oExchangeRate.Rate, Symbol, oExchangeRate.Fch);
            return retval;
        }


        public DTOAmt amtFromDivisa(decimal divisa, DTOCurExchangeRate oExchangeRate = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oExchangeRate == null)
                oExchangeRate = ExchangeRate;
            decimal eur = Math.Round(divisa / (decimal)oExchangeRate.Rate, 2, MidpointRounding.AwayFromZero);
            var retval = DTOAmt.Factory(eur, this, divisa);
            return retval;
        }


        public bool isEur()
        {
            return Tag == "EUR";
        }

        public bool isUSD()
        {
            return Tag == "USD";
        }

        public override string ToString()
        {
            return Tag;
        }

        public class Compact
        {
            public string Tag { get; set; }

            public static Compact Factory(string tag)
            {
                Compact retval = new Compact();
                retval.Tag = tag;
                return retval;
            }

        }

        public class Collection : List<DTOCur>
        {
            public Collection() : base()
            {

            }
        }
    }
}
