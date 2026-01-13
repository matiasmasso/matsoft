using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOProductStat : DTOProduct
    {
        public List<DTOYearMonth> Items { get; set; }

        public DTOProductStat() : base()
        {
            Items = new List<DTOYearMonth>();
        }

        public static DTOProductStat Factory(DTOProduct oProduct, List<Exception> exs)
        {
            DTOProductStat retval = new DTOProductStat();
            DTOBaseGuid.CopyPropertyValues<DTOProduct>(oProduct, retval, exs);
            return retval;
        }
    }
}
