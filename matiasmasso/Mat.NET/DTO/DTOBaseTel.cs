using MatHelperStd;
using System;
using System.Linq;

namespace DTO
{
    public class DTOBaseTel : DTOBaseGuid
    {
        public string Value { get; set; }
        public string Obs { get; set; }

        public ObjCods ObjCod { get; set; }

        public enum ObjCods
        {
            Tel,
            User
        }

        public DTOBaseTel(Guid oGuid) : base(oGuid)
        {
        }

        public DTOBaseTel() // Constructor sense parametres per serialitzar-lo al pujar les dades via Ajax per exemple de Quiz
    : base()
        {
        }

        public static bool IsPhoneNumber(string src)
        {
            bool retval = true;
            string allowedChars = "+()-.0123456789 ";
            for (var i = 0; i <= src.Length - 1; i++)
            {
                string StringToCheck = src.Substring(i, 1);
                if (!allowedChars.Contains(StringToCheck))
                {
                    retval = false;
                    break;
                }
            }
            return retval;
        }

        public static string Formatted(string src, DTOCountry oCountry = null/* TODO Change to default(_) if this is not a reference type */, bool IncludeSpanishPrefix = false)
        {
            string retval = "";
            if (src.isNotEmpty())
            {
                string onlyDigits = new string(src.Where(x => char.IsDigit(x)).ToArray());
                retval = TextHelper.InsertStringRepeatedly(onlyDigits, ".", 3);
                if (oCountry != null)
                {
                    if (IncludeSpanishPrefix | oCountry.ISO != "ES")
                        retval = string.Format("(+{0}) {1}", oCountry.PrefixeTelefonic, retval);
                }
            }
            return retval;
        }

        public static string Formatted(DTOBaseTel src, bool ShowCountryPrefix = true)
        {
            string retval = "";
            if (src is DTOContactTel)
            {
                DTOContactTel oContactTel = (DTOContactTel)src;
                if (ShowCountryPrefix)
                    retval = Formatted(oContactTel.Value, oContactTel.Country);
                else
                    retval = Formatted(oContactTel.Value);
            }
            else if (src is DTOUser)
            {
                DTOUser oUser = (DTOUser)src;
                retval = oUser.EmailAddress;
            }
            return retval;
        }

        public static string GetObs(DTOBaseTel src)
        {
            string retval = "";
            if (src is DTOContactTel)
            {
                DTOContactTel oContactTel = (DTOContactTel)src;
                retval = oContactTel.Obs;
            }
            else if (src is DTOUser)
            {
                DTOUser oEmail = (DTOUser)src;
                retval = oEmail.Obs;
            }
            return retval;
        }
    }
}
