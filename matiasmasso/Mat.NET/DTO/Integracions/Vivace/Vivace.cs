namespace DTO.Integracions.Vivace
{
    public class Vivace
    {
        public static string TrackingUrl(DTODelivery delivery)
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

    }
}
