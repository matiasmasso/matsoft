using System;
using System.Collections.Generic;

namespace DTO
{


    public class DTOWtbolSerp : DTOBaseGuid
    {
        public DTOSession Session { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public string CountryCode { get; set; }
        public DTOProduct Product { get; set; }
        public DateTime Fch { get; set; }
        public List<Item> Items { get; set; }

        public DTOWtbolSerp() : base()
        {
            Items = new List<Item>();
        }

        public DTOWtbolSerp(Guid oGuid) : base(oGuid)
        {
            Items = new List<Item>();
        }

        public class Item
        {
            public int Pos { get; set; }
            public DTOWtbolSite Site { get; set; }
        }
    }
}