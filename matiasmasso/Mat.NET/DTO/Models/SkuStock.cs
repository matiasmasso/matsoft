using System;

namespace DTO.Models
{
    public class SkuStock
    {
        public Guid Guid { get; set; }
        public int Stock { get; set; }
        public int Clients { get; set; }
        public int ClientsAlPot { get; set; }
        public int ClientsEnProgramacio { get; set; }
        public int ClientsBlockStock { get; set; }
        public int Proveidors { get; set; }

        public int StockAvailable()
        {
            int retval = Stock - (Clients - ClientsAlPot - ClientsEnProgramacio);
            if (retval < 0)
                retval = 0;
            return retval;
        }
    }
}