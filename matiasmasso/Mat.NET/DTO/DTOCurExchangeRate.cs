using System;

namespace DTO
{
    public class DTOCurExchangeRate
    {
        public DateTime Fch { get; set; }
        public decimal Rate { get; set; }

        public static DTOCurExchangeRate Factory(DateTime DtFch, decimal DcRate)
        {
            DTOCurExchangeRate retval = new DTOCurExchangeRate();
            {
                var withBlock = retval;
                withBlock.Fch = DtFch;
                withBlock.Rate = DcRate;
            }
            return retval;
        }
    }
}
