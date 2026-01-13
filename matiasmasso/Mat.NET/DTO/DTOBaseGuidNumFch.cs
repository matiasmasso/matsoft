using System;

namespace DTO
{
    public class DTOBaseGuidNumFch : DTOBaseGuid
    {
        public int Num { get; set; }
        public DateTime Fch { get; set; }

        public DTOBaseGuidNumFch() : base()
        {
        }

        public DTOBaseGuidNumFch(Guid oGuid) : base(oGuid)
        {
        }

        public static string Formatted(DTOBaseGuidNumFch value)
        {
            string retval = VbUtilities.Format(value.Fch.Year, "0000") + "." + VbUtilities.Format(value.Num, "00000");
            return retval;
        }
    }
}
