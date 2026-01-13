using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Vivace
{
    public class Trace
    {
        public string? Url { get; set; }

        public bool MoreInfoAvailable { get; set; }
        public List<Item> Items { get; set; } = new();

        public Trace()
        {
            Items = new Item.Collection();
        }

        public static string TrackingUrl(DeliveryModel delivery)
        {
            string template = "http://laroca.vivacelogistica.com/ivivaldillisa/trackingvivace/trackingvivace.aspx?codcli=1&anodocpedido={0}&numpedido={1}{2}";
            string retval = string.Format(template, delivery.Fch.Year, delivery.Formatted(), DigitDeControl(delivery.Formatted()));
            return retval;
        }

        static int DigitDeControl(string src)
        {
            int idx = 0;
            int sum = 0;
            for (int counter = src.Length - 1; counter >= 0; counter--)
            {
                idx += 1;

                string sChar = src[counter].ToString();
                if (!int.TryParse(sChar, out int iChar))
                    iChar = 1;
                int sumando = EsPar(idx) ? iChar * 1 : iChar * 3;
                sum += sumando;
            }
            //decena superior al resultat
            int decena = sum / 10;
            if (!(decena * 10 == sum))
                decena += 1;

            int retval = 10 * decena - sum;
            return retval;
        }

        static bool EsPar(int numero)
        {
            if ((numero % 2) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public class Item
        {
            public DateTime Fch { get; set; }
            public String? Caption { get; set; }

            public static Item? Factory( string src)
            {
                Item? retval = null;
                string[] parts = src.Split('|');
                if (parts.Length == 2)
                {
                    retval = new Item();
                    retval.Fch = ParseFch(parts[0]);
                    retval.Caption = parts[1];
                }
                else
                    throw new Exception("missing separator at line " + src);

                return retval;
            }

            private static DateTime ParseFch(string src)
            {

                string[] parts = src.Trim().Split(' ');
                string[] sDate = parts[0].Split('/');
                string[] sTime = parts[1].Split(':');
                int day = Convert.ToInt32(sDate[0]);
                int month = Convert.ToInt32(sDate[1]);
                int year = Convert.ToInt32(sDate[2]);
                int hour = Convert.ToInt32(sTime[0]);
                int minute = Convert.ToInt32(sTime[1]);
                int second = Convert.ToInt32(sTime[2]);
                DateTime retval = new DateTime(year, month, day, hour, minute, second);
                return retval;
            }


            public class Collection : List<Item>
            {
                public static Collection Factory(List<Exception> exs, List<string> lines)
                {
                    Collection retval = new Collection();
                    foreach (string line in lines)
                    {
                        Item? item = Item.Factory(line);
                        if(item != null) retval.Add(item);
                    }
                    return retval;
                }
            }
        }
    }

}
