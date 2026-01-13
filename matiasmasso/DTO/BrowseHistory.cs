using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class BrowseHistory
    {
        public string Nom { get; set; }
        public string Url { get; set; }
        public DateTime Fch { get; set; }

        public bool Matches(string searchTerm)
        {
            bool retval = false;
            if (string.IsNullOrEmpty(searchTerm))
                retval = true;
            else
            {
                if (!string.IsNullOrEmpty(Nom) && Nom.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase))
                    retval = true;
            }
            return retval;
        }

        public override string ToString()
        {
            return $"{Fch.ToString("dd/MM/yy")}:{Fch.ToString("HH:mm:ss")} {Nom}";
        }
    }

}
