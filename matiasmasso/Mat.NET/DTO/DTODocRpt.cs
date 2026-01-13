using System.Collections.Generic;

namespace DTO
{
    public class DTODocRpt
    {
        public Estilos Estilo { get; set; }
        public FuentePapel Papel { get; set; }
        public List<DTODoc> Docs { get; set; }

        public enum Estilos
        {
            Comanda,
            Albara,
            Factura,
            Proforma
        }

        public enum FuentePapel
        {
            Copia,
            Original
        }

        public enum Ensobrados
        {
            No,
            Sencillo
        }

        public DTODocRpt(Estilos oEstilo, FuentePapel oPapel = FuentePapel.Copia) : base()
        {
            Estilo = oEstilo;
            Papel = oPapel;
            Docs = new List<DTODoc>();
        }
    }
}
