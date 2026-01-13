using System;

namespace DTO
{
    public class DTOEvento : DTONoticia
    {
        public string NomEsp { get; set; }
        public string NomCat { get; set; }
        public string NomEng { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public object Area { get; set; }

        public DTOEvento() : base()
        {
            base.Src = Srcs.Eventos;
        }

        public DTOEvento(Guid oGuid) : base(oGuid)
        {
        }
    }
}
