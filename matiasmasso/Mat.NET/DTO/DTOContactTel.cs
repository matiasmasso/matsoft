using MatHelperStd;
using System;

namespace DTO
{
    public class DTOContactTel : DTOBaseTel
    {
        public Cods Cod { get; set; }
        public int Ord { get; set; }
        public DTOCountry Country { get; set; }
        public bool Privat { get; set; }

        public enum IncludePrefix
        {
            None,
            IfNotCurrent,
            Always
        }


        public enum Cods
        {
            NotSet,
            tel,
            fax,
            movil,
            email
        }

        public DTOContactTel() : base()
        {
            base.ObjCod = DTOBaseTel.ObjCods.Tel;
        }

        public DTOContactTel(Guid oGuid) : base(oGuid)
        {
            base.ObjCod = DTOBaseTel.ObjCods.Tel;
        }

        public static DTOContactTel Factory(DTOContact oContact, DTOContactTel.Cods oCod = DTOContactTel.Cods.tel, string value = "")
        {
            DTOContactTel retval = new DTOContactTel();
            {
                var withBlock = retval;
                withBlock.Cod = oCod;
                withBlock.Country = DTOAddress.Country(oContact.Address);
                withBlock.Value = TextHelper.LeaveJustNumericDigits(value);
            }
            return retval;
        }

        public static string CleanValue(DTOContactTel oContactTel)
        {
            string retval = "";
            if (oContactTel != null)
                retval = TextHelper.LeaveJustNumericDigits(oContactTel.Value);
            return retval;
        }

        public static string Formatted(DTOContactTel value)
        {
            string retval = "";
            if (value != null)
            {
                retval = value.Value;
                if (retval.Length > 6)
                {
                    retval = retval.Insert(6, ".");
                    retval = retval.Insert(3, ".");
                }
                if (value.Country != null)
                {
                    if (!DTOArea.isEsp(value.Country))
                    {
                        if (value.Country.PrefixeTelefonic.isNotEmpty())
                            retval = "(+" + value.Country.PrefixeTelefonic + ") " + retval;
                    }
                }
            }
            return retval;
        }

        public static string HtmlLink(DTOContactTel value)
        {
            string retval = value.Value;
            if (value.Country != null)
            {
                if (value.Country.PrefixeTelefonic.isNotEmpty())
                    retval = string.Format("+{0}{1}", value.Country.PrefixeTelefonic, value.Value);
            }
            return retval;
        }

        public static string Html5Formatted(DTOContactTel oContactTel)
        {
            string retval = "";
            if (oContactTel != null)
            {
                if (oContactTel.Value.isNotEmpty())
                {
                    string sTelnum = System.Text.RegularExpressions.Regex.Replace(oContactTel.Value, "[^0-9]", "");
                    if (oContactTel.Country != null && oContactTel.Country.PrefixeTelefonic.isNotEmpty())
                        retval = string.Format("tel:+{0}{1}", oContactTel.Country.PrefixeTelefonic, sTelnum);
                    else
                        retval = string.Format("tel:{0}", sTelnum);
                }
            }
            return retval;
        }
    }
}
