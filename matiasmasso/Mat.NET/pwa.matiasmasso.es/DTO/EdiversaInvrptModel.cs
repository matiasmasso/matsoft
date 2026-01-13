using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO
{
    public class EdiversaInvrptDTO
    {
        public List<EdiversaInvrptModel> Values { get; set; } = new();
        public List<int> Years { get; set; } = new();

    }

    public class EdiversaInvrptModel : BaseGuid
    {
        public DateTime FchDoc { get; set; }
        public DateTime FchReceived { get; set; }
        public string? DocNum { get; set; }
        //public Guid? Customer { get; set; } // message sender
        public string? NadGy { get; set; } // stock holder party

        public GuidNom? StockHolder { get; set; }
        public GuidNom? Ccx { get; set; }

        public List<Item> Items { get; set; } = new();

        public EdiversaInvrptModel() : base() { }
        public EdiversaInvrptModel(Guid guid) : base(guid) { }

        public class Item
        {
            public int Lin { get; set; }
            public int Qty { get; set; }
            public string? Ean { get; set; }
            public string? SupplierRef { get; set; }
            public string? CustomerRef { get; set; }
        }
    }
}
