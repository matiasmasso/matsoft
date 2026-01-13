using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DeliveryListDTO
    {
        public List<int> Years { get; set; } = new();
        public List<Item> Items { get; set; } = new();


        public class Item
        {
            public Guid Guid { get; set; }
            public int Id { get; set; }
            public DateTime Fch { get; set; }
            public GuidNom? Contact { get; set; }
            public Amt? Amt { get; set; }


            public Item() : base() { }


            public bool Matches(string? searchTerm)
            {
                bool retval = true;
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                    var searchTarget = Contact?.Nom ?? "" + " " + Id.ToString() ?? "";
                    retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
                }
                return retval;
            }


            public string PageUrl() => Globals.PageUrl("delivery", Guid.ToString());

            public string DownloadUrl() => Globals.PageUrl("delivery/pdf", Guid.ToString());

            public new string ToString() => string.Format("{DeliveryListDTO.Item: alb.{0:00000} del {1:dd/MM/yy}", Id, Fch);
        }
    }
}
