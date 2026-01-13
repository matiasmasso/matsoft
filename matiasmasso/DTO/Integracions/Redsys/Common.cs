using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO.Integracions.Redsys
{
    public class Common
    {
        public enum Modes
        {
            NotSet,
            Free,
            Alb,
            Pdc,
            Impagat,
            Consumer,
            Shop4moms
        }

        public enum Environments
        {
            Production,
            Development
        }

        public static string NextTpvOrderNum(Environments environment, string? lastNum)
        {
            int lastId = 0;
            if (!string.IsNullOrEmpty(lastNum))
            {
                string[] numbers = Regex.Split(lastNum, @"\D+");
                if (numbers.Length > 0)
                    lastId = Convert.ToInt32(numbers[numbers.Length - 1]);
            }

            var nextId = lastId + 1;
            string prefix = environment == Environments.Development ? "FAKE0" : "9999A";
            string retval = string.Format("{0}{1:D7}", prefix, nextId);
            return retval;
        }



        public static string LangCode(string langISO)
        {
            //Castellano-001, Inglés-002, Catalán-003, Portugués-009

            string retval;
            switch (langISO)
            {
                case "CAT":
                    retval = "003";
                    break;
                case "ENG":
                    retval = "002";
                    break;
                case "POR":
                    retval = "009";
                    break;
                default:
                    retval = "001";
                    break;
            }
            return retval;
        }

        public static LangDTO Lang(string? redsysLangCode)
        {
            //Castellano-001, Inglés-002, Catalán-003, Portugués-009

            LangDTO retval = LangDTO.Esp();
            if (!string.IsNullOrEmpty(redsysLangCode))
            {
                switch (redsysLangCode)
                {
                    case "003":
                        retval = LangDTO.Cat();
                        break;
                    case "002":
                        retval = LangDTO.Eng();
                        break;
                    case "009":
                        retval = LangDTO.Por();
                        break;
                }
            }
            return retval;

        }
    }
}
