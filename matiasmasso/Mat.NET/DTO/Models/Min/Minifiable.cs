using System;
using System.Collections.Generic;

namespace DTO.Models.Min
{
    public class Minifiable : Dictionary<string, Object>
    {
        public static bool ParseBool(KeyValuePair<string, Object> x)
        {
            int value = int.Parse(x.Value.ToString());
            bool retval = value != 0;
            return retval;
        }
        public static int ParseInt(KeyValuePair<string, Object> x)
        {
            int retval = int.Parse(x.Value.ToString());
            return retval;
        }
        public static Guid ParseGuid(KeyValuePair<string, Object> x)
        {
            Guid retval = new Guid(x.Value.ToString());
            return retval;
        }
        public static decimal ParseDecimal(KeyValuePair<string, Object> x)
        {
            decimal retval = decimal.Parse(x.Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
            return retval;
        }

        public static DTOAmt ParseAmt(KeyValuePair<string, Object> x)
        {
            decimal eur = decimal.Parse(x.Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
            DTOAmt retval = DTOAmt.Factory(eur);
            return retval;
        }

        public static DateTime ParseFch(KeyValuePair<string, Object> x)
        {
            DateTime retval = DateTime.Parse(x.Value.ToString());
            return retval;
        }

        public static string SerializeEur(Decimal eur)
        {
            string retval = eur.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
            return retval;
        }
        public static string SerializeFch(DateTime fch)
        {
            return fch.ToString("yyyy-MM-dd");
        }

        public static string SerializeDateTime(DateTime fch)
        {
            return fch.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static Guid Guid(Dictionary<string, Object> value, int cod)
        {
            Guid retval;
            foreach (KeyValuePair<string, Object> x in value)
            {
                if (x.Key.toInteger() == cod)
                    if (string.IsNullOrEmpty(x.Value.ToString()))
                        retval = System.Guid.Empty;
                    else
                        retval = new Guid(x.Value.ToString());
            }
            return retval;
        }
    }
}
