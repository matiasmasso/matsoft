using System;

namespace DTO
{
    public class DTOCheckedGuidNom : DTOGuidNom
    {
        public Object Tag { get; set; }
        public bool Checked { get; set; }

        public static DTOCheckedGuidNom Factory(Guid oGuid, string sNom = "", bool isChecked = false)
        {
            var retval = new DTOCheckedGuidNom();
            retval.Guid = oGuid;
            retval.Nom = sNom;
            retval.Checked = isChecked;
            return retval;
        }
    }
}
