using System;

namespace DTO
{
    public class DTONoticiaDestacada : DTOBaseGuid
    {
        public DTOBaseGuid Noticia { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public bool Professionals { get; set; }

        public DTONoticiaDestacada() : base()
        {
        }

        public DTONoticiaDestacada(Guid oGuid) : base(oGuid)
        {
        }
    }
}
