using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DTO
{
    public class SelloutDTO
    {

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<MenuItemModel> MenuItems { get; set; } = new();

        public enum Modes:int
        {
            Diari,
            Atlas,
            Products,
            Channels,
            Reps,
            Cnaps
        }

        public class Request
        {
            public UserModel? User { get; set; }

            public int EmpId { get; set; } = (int)EmpModel.EmpIds.MatiasMasso;
            public DateTime? Fch { get; set; }
            public List<Filter> Filters { get; set; } = new();

            public Request(UserModel? user=null)
            {
                User = user;
            }


        }

        public class Filter
        {
            public Cods Cod { get; set; }
            public GuidNom Value { get; set; }
            public enum Cods
            {
                Proveidor,
                Holding,
                Ccx,
                Client,
                Rep,
                Product
            }
        }

        public class Item
        {
            public int Year { get; set; }

            public int Month { get; set; }

            public int Day { get; set; }

            public decimal Eur { get; set; }

            public Guid Country { get; set; }
            public Guid Zona { get; set; }
            public Guid Location { get; set; }

            public int Pdcs { get; set; } // orders count
            public Item() { }

        }

        public class Order
        {
            public Guid Guid { get; set; }
            public DateTime FchCreated { get; set; }
            public string CustomerName { get; set; }
            public string Concept { get; set; }
            public string Location { get; set; }
            public Guid Channel { get; set; }
            public string Cnap { get; set; }
            public Guid Rep { get; set; }
            public int Qty { get; set; }
            public decimal Eur { get; set; }

            public string Caption(LangDTO lang)
            {
                return $"{lang.Tradueix("Pedido","Comanda","P.Order")} {CustomerName}";
            }
            public bool Matches(string? searchTerm)
            {
                return string.IsNullOrEmpty(searchTerm)
                    || CustomerName.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
                    || Location.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
                    || Concept.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase);
            }

        }
    }
}

