using System.Collections.Generic;
using System.Drawing;

namespace DTO
{
    public class DTOContactMenu
    {
        public bool IsClient { get; set; }
        public bool IsProveidor { get; set; }
        public bool IsRep { get; set; }
        public bool IsStaff { get; set; }
        public bool IsTransportista { get; set; }
        public bool IsInsolvent { get; set; }
        public bool IsObsoleto { get; set; }

        public List<DTOContactTel> Tels { get; set; }
        public List<DTOUser> Emails { get; set; }

        public DTOContactMenu()
        {
            Tels = new List<DTOContactTel>();
            Emails = new List<DTOUser>();
        }

        public Color BackColor()
        {
            Color retval = Color.White;

            if (IsInsolvent)
                retval = Color.LightSalmon;
            else if (IsObsoleto)
                retval = Color.LightGray;

            return retval;
        }
    }
}
