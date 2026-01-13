using System;

namespace DTO
{
    public class DTOBaseGuidCodNom : DTOBaseGuid
    {
        public Cods cod { get; set; }
        public string nom { get; set; }

        public enum Cods
        {
            notSet,
            vehicle,
            productBrand,
            productCategory,
            productSku,
            liniaTelefon
        }

        public DTOBaseGuidCodNom() : base()
        {
        }

        public DTOBaseGuidCodNom(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOBaseGuidCodNom Factory(Guid oGuid, Cods oCod = Cods.notSet, string sNom = "")
        {
            var retval = new DTOBaseGuidCodNom(oGuid);
            {
                var withBlock = retval;
                withBlock.cod = oCod;
                withBlock.nom = sNom;
            }
            return retval;
        }
    }
}
