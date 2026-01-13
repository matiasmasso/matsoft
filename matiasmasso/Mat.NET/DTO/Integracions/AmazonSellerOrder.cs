using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DTO
{
    public class AmazonSellerOrder
    {
        public string Nom { get; set; }
        public string Address { get; set; }
        public string ZipCod { get; set; }
        public string Location { get; set; }
        public string Provincia { get; set; }
        public DTOCountry Country { get; set; }

        public DateTime Fch { get; set; }
        public string OrderNum { get; set; }

        public Item.Collection Items { get; set; }

        public decimal Portes { get; set; }

        public decimal Total { get; set; }

        public DTODocFile Docfile { get; set; }

        private List<string> Lines;
        private List<DTOCountry> Countries;

        public const string urlTemplate = "https://sellercentral.amazon.co.uk/orders";

        public AmazonSellerOrder(DTODocFile docfile, List<string> lines, List<DTOCountry> countries)
        {
            this.Lines = lines;
            this.Countries = countries;
            this.Items = new Item.Collection();
            this.Docfile = docfile;
            ReadFromLines();
        }

        public DTOMarketPlace MarketPlace()
        {
            DTOMarketPlace retval = DTOMarketPlace.Wellknown(DTOMarketPlace.Wellknowns.AmazonSeller);
            retval.Nom = "Amazon Seller";
            return retval;
        }

        private void ReadFromLines()
        {
            this.Nom = ReadNom();
            this.Address = ReadAddress();
            this.Location = ReadLocation();
            this.Country = ReadCountry();
            this.OrderNum = ReadOrderNum();
            this.Items = ReadItems();
            this.Portes = ReadPortes();
            this.Total = ReadTotal();
        }

        private decimal ReadPortes()
        {
            decimal retval = 0;
            string searchkey = "Total del envío";
            string bookmark = BookmarkStartsWith(searchkey);
            if (bookmark.isNotEmpty())
            {
                string value = bookmark.Substring(searchkey.Length + 1).Trim();
                retval = ParseCurrency(value);
            }
            return retval;
        }



        private decimal ReadTotal()
        {
            decimal retval = 0;
            string searchkey = "Suma total:";
            string bookmark = BookmarkStartsWith(searchkey);
            if (bookmark.isNotEmpty())
            {
                string value = bookmark.Substring(searchkey.Length + 1).Trim();
                retval = ParseCurrency(value);
            }
            return retval;
        }

        private static decimal ParseCurrency(string src)
        {
            decimal retval = 0;
            NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            CultureInfo provider = new CultureInfo("es-ES");
            if (src.Substring(src.Length - 2).Trim() == "€")
                src = src.Substring(0, src.Length - 2) + "€";
            if (Decimal.TryParse(src, style, provider, out retval))
            {
            }
            return retval;
        }

        private Item.Collection ReadItems()
        {
            Item.Collection retval = new Item.Collection();
            //first item starts after line with "Cantidad Detalles del producto..."
            //each item ends after "Total del artículo..."
            //first line each item starts with qty followed by space followed by description in same or extra lines
            //each item to read SKU and ASIN on lines that start with SKU: and ASIN:
            //after last item comes line "Suma total"

            string bookmarkStart = BookmarkStartsWith("Cantidad Detalles del producto");
            string bookmarkEnd = BookmarkStartsWith("Suma total");
            if (bookmarkStart.isNotEmpty() && bookmarkEnd.isNotEmpty())
            {
                int idxStart = Lines.IndexOf(bookmarkStart);
                int idxEnd = Lines.IndexOf(bookmarkEnd);
                int idxItemFirstLine = idxStart + 1;
                Item item = new Item();
                for (int i = idxStart + 1; i < idxEnd; i++)
                {
                    if (i == idxItemFirstLine)
                    {
                        item = new Item();
                        item.Qty = readLeadingOInteger(Lines[i]);
                        retval.Add(item);
                    }
                    else if (Lines[i].StartsWith("SKU:"))
                        item.Sku = ReadSecondWord(Lines[i]);
                    else if (Lines[i].StartsWith("ASIN:"))
                        item.Asin = ReadSecondWord(Lines[i]);
                    else if (Lines[i].StartsWith("Total del artículo"))
                        idxItemFirstLine = i + 1;
                }
            }
            return retval;
        }

        private int readLeadingOInteger(string line)
        {
            int retval = 0;
            string[] words = line.Trim().Split(' ');
            if (words.Length > 0)
            {
                retval = words.First().Trim().toInteger();
            }
            return retval;
        }

        private string ReadSecondWord(string line)
        {
            string retval = "";
            string[] words = line.Trim().Split(' ');
            if (words.Length > 1)
            {
                retval = words[1].Trim();
            }
            return retval;
        }

        private string ReadNom()
        {
            string retval = "";
            int idx = BookmarkIdx("Dirección de envío:");
            retval = Lines[idx + 1].Trim();
            return retval;
        }

        private string ReadAddress()
        {

            string retval = "";
            int idx = BookmarkIdx("Dirección de envío:");
            int idxCountry = CountryBookmarkIdx();
            int firstAddressLine = idx + 2;
            int lastAddressLine = idxCountry - 2;

            //idx+1 = nom
            //idx+2 = 1ª linia address
            //idx...
            //idxCountry -2 = darrera linia adress
            //idxCountry -1 = location

            StringBuilder sb = new StringBuilder();
            if (lastAddressLine > firstAddressLine)
            {
                for (int i = firstAddressLine; i <= lastAddressLine; i++)
                    sb.AppendLine(Lines[i].Trim());
            }
            retval = sb.ToString();
            return retval;
        }

        private string ReadLocation()
        {
            string retval = "";
            int idxCountry = CountryBookmarkIdx();
            if (idxCountry > 0)
                retval = Lines[idxCountry - 1].Trim();
            return retval;
        }

        private DTOCountry ReadCountry()
        {
            DTOCountry retval = null;
            string bookmark = CountryBookmark().Trim();
            if (bookmark.isNotEmpty())
                retval = Countries.FirstOrDefault(x => x.LangNom.Esp == bookmark);
            return retval;
        }

        private string ReadOrderNum()
        {
            string retval = "";
            string bookmark = BookmarkStartsWith("Nº de pedido:");
            if (bookmark.isNotEmpty())
            {
                int idx = bookmark.LastIndexOf(" ");
                retval = bookmark.Substring(idx).Trim();
            }
            return retval;
        }

        private int BookmarkIdx(string src)
        {
            string bookmark = BookmarkEquals(src);
            int retval = string.IsNullOrEmpty(bookmark) ? -1 : Lines.IndexOf(bookmark);
            return retval;
        }

        private string BookmarkEquals(string src)
        {
            string retval = Lines.FirstOrDefault(x => x.Trim() == src);
            return retval;
        }

        private string BookmarkStartsWith(string src)
        {
            string retval = Lines.FirstOrDefault(x => x.Trim().StartsWith(src));
            return retval;
        }

        private int CountryBookmarkIdx()
        {
            string bookmark = CountryBookmark();
            int retval = string.IsNullOrEmpty(bookmark) ? -1 : Lines.IndexOf(bookmark);
            return retval;
        }
        private string CountryBookmark()
        {
            string retval = Lines.FirstOrDefault(x => Countries.Any(y => x.Trim() == y.LangNom.Esp));
            return retval;
        }

        public static bool MatchSource(List<string> Lines)
        {
            bool retval = Lines.Any(x => x.Contains("Gracias por comprar en Amazon"));
            return retval;
        }

        public static bool MatchUrl(String url)
        {
            bool retval = url.StartsWith(urlTemplate);
            return retval;
        }

        public class Item
        {
            public int Qty { get; set; }
            public string Sku { get; set; }
            public string Asin { get; set; }
            public decimal Price { get; set; }

            public class Collection : List<Item>
            {

            }
        }
    }
}
