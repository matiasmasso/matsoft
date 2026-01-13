using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOImmoble : DTOGuidNom
    {
        public Models.Base.IdNom Emp { get; set; }
        public DTOAddress Address { get; set; }
        public string Cadastre { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public Titularitats Titularitat { get; set; }
        public decimal Part { get; set; }
        public decimal Superficie { get; set; }

        public enum Titularitats
        {
            PlenoDominio,
            NudaPropiedad,
            Usufructo
        }


        public DTOImmoble() : base()
        {
        }

        public DTOImmoble(Guid oGuid) : base(oGuid)
        {
        }

        public class InventariItem : DTOGuidNom
        {
            public DTOImmoble Immoble { get; set; }
            public string Obs { get; set; }

            public InventariItem() : base()
            {
            }

            public InventariItem(Guid oGuid) : base(oGuid)
            {
            }

            public static DTOImmoble.InventariItem factory(DTOImmoble immoble)
            {
                DTOImmoble.InventariItem retval = new DTOImmoble.InventariItem();
                retval.Immoble = immoble;
                return retval;
            }

            public class Collection : List<InventariItem>
            {

            }

        }

        public class Bundle
        {
            public List<DTOImmoble> Immobles { get; set; }
            public List<DTODocFileSrc> DocfileSrcs { get; set; }

            public List<DTOImmoble.InventariItem> InventariItems { get; set; }
        }
    }
}
