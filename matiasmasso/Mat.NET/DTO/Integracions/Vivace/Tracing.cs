using System;
using System.Collections.Generic;

namespace DTO.Integracions.Vivace
{
    public class Trace
    {
        public const string UrlTemplate = "http://laroca.vivacelogistica.com/ivivaldillisa/trackingvivace/trackingvivace.aspx?codcli=1&anodocpedido=2021&numpedido={0}";
        public string Url { get; set; }

        public bool MoreInfoAvailable { get; set; }
        public Item.Collection Items { get; set; }

        public Trace()
        {
            Items = new Item.Collection();
        }


        public class Item
        {
            public DateTime Fch { get; set; }
            public String Caption { get; set; }

            public static Item Factory(List<Exception> exs, string src)
            {
                Item retval = null;
                string[] parts = src.Split('|');
                if (parts.Length == 2)
                {
                    retval = new Item();
                    retval.Fch = ParseFch(parts[0]);
                    retval.Caption = parts[1];
                }
                else
                {
                    exs.Add(new Exception("missing separator at line " + src));
                }
                return retval;
            }

            private static DateTime ParseFch(string src)
            {

                string[] parts = src.Trim().Split(' ');
                string[] sDate = parts[0].Split('/');
                string[] sTime = parts[1].Split(':');
                int day = sDate[0].toInteger();
                int month = sDate[1].toInteger();
                int year = sDate[2].toInteger();
                int hour = sTime[0].toInteger();
                int minute = sTime[1].toInteger();
                int second = sTime[2].toInteger();
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
                        Item item = Item.Factory(exs, line);
                        retval.Add(item);
                    }
                    return retval;
                }
            }
        }
    }
}
