using System;

namespace DTO
{
    public class DTOStaffPos : DTOBaseGuid
    {
        public DTOLangText LangNom { get; set; }
        public int Ord { get; set; }

        public DTOStaffPos() : base()
        {
            this.LangNom = new DTOLangText(base.Guid, DTOLangText.Srcs.StaffPos);
        }

        public DTOStaffPos(Guid oGuid) : base(oGuid)
        {
            this.LangNom = new DTOLangText(base.Guid, DTOLangText.Srcs.StaffPos);
        }

        public static string Nom(DTOStaffPos oStaffPos, DTOLang oLang)
        {
            string retval = "";
            if (oStaffPos != null)
                retval = oStaffPos.LangNom.Tradueix(oLang);
            return retval;
        }
    }
}
