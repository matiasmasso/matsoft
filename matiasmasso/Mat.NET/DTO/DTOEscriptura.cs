using System;

namespace DTO
{
    public class DTOEscriptura : DTOBaseGuid
    {
        public DTOEmp Emp { get; set; }
        public Codis Codi { get; set; } = Codis.NotSet;
        public DTOContact Notari { get; set; } = null/* TODO Change to default(_) if this is not a reference type */;
        public int NumProtocol { get; set; } = 0;
        public DateTime FchFrom { get; set; } = DateTime.MinValue;
        public DateTime FchTo { get; set; } = DateTime.MinValue;
        public DTOContact RegistreMercantil { get; set; } = null/* TODO Change to default(_) if this is not a reference type */;
        public int Tomo { get; set; } = 0;
        public int Folio { get; set; } = 0;
        public string Hoja { get; set; } = "";
        public int Inscripcio { get; set; } = 0;
        public string Nom { get; set; } = "";
        public string Obs { get; set; } = "";

        public DTODocFile DocFile { get; set; }
        public bool DirtyDocument { get; set; }
        public DTOEnums.TriState DocExists { get; set; }
        public bool Exists { get; set; }


        public enum Codis
        {
            NotSet,
            Constitucio,
            Adaptacio_Estatuts,
            Canvi_Domicili,
            Nomenament_Administradors,
            Poders,
            Compra_Venda,
            Titularitat_Real,
            Modificacions_Estatuts,
            Reconeixement_deute
        }

        public DTOEscriptura() : base()
        {
        }

        public DTOEscriptura(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOEscriptura Factory(DTOEmp oEmp)
        {
            DTOEscriptura retval = new DTOEscriptura();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
                withBlock.FchFrom = DTO.GlobalVariables.Today();
            }
            return retval;
        }
    }
}
