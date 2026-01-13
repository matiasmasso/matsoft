using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public class RepLiqsModel
    {
        public List<Rep> Reps { get; set; }

        public RepLiqsModel()
        {
            Reps = new List<Rep>();
        }


        public class Rep : Base.GuidNom
        {
            public List<Item> Items { get; set; }
            public Rep()
            {
                Items = new List<Item>();
            }
        }

        public class Item
        {
            public Guid Guid { get; set; }
            public DateTime Fch { get; set; }
            public Decimal Base { get; set; }
            public Decimal Iva { get; set; }
            public Decimal Irpf { get; set; }
        }
    }
}
