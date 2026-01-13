using System;

namespace DTO
{
    public class DTOEdiversaGenral : DTOBaseGuid
    {
        public DTOEdiversaFile.IOcods IoCod { get; set; }
        public string Ref { get; set; }
        public DateTime Fch { get; set; }
        public DTOContact Contact { get; set; }
        public string Docnum { get; set; }
        public string Text { get; set; }

        public DTOEdiversaGenral() : base()
        {
        }

        public DTOEdiversaGenral(Guid oGuid) : base(oGuid)
        {
        }

    }
}
