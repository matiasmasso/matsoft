using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BrowserHistory // Deprecated
    {
        private static List<string> Urls { get; set; } = new();

        public static void Add(string url)
        {
            if(Urls.Count == 0)
                Urls.Add(url);
            else if ( url != Urls.Last())
                Urls.Add(url);
        }

        public static void RemoveLast()
        {
            Urls.RemoveAt(Urls.Count - 1);
        }

        public static string GetLast() => Urls[Urls.Count - 1];
    }
}
