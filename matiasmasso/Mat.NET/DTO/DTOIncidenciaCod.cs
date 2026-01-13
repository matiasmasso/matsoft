using System;

namespace DTO
{
    public class DTOIncidenciaCod : DTOBaseGuid
    {
        public DTOLangText nom { get; set; }
        public Cods cod { get; set; }
        public bool reposicionParcial { get; set; }
        public bool reposicionTotal { get; set; }

        public enum Cods
        {
            notSet,
            averia,
            tancament
        }

        public string Esp
        {
            get
            {
                return nom.Esp;
            }
        }

        public DTOIncidenciaCod(Guid oGuid) : base(oGuid)
        {
            nom = new DTOLangText();
        }
        public DTOIncidenciaCod() : base()
        {
            nom = new DTOLangText();
        }

        public static DTOIncidenciaCod Wellknown(DTOIncidenciaCod.Cods oCod)
        {
            DTOIncidenciaCod retval = null;
            switch (oCod)
            {
                case DTOIncidenciaCod.Cods.averia:
                    {
                        retval = new DTOIncidenciaCod(new Guid("504AA029-D206-41ED-A6F2-CE4C80A20FA4"));
                        break;
                    }
            }
            return retval;
        }

        public new string ToString()
        {
            return nom.Esp;
        }
    }
}
