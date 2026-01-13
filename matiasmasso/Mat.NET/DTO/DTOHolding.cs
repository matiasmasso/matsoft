using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOHolding : DTOGuidNom
    {
        public DTOEmp emp { get; set; }
        public List<DTOContact> companies { get; set; }
        public List<DTOCustomerCluster> clusters { get; set; }

        public enum Wellknowns
        {
            notSet,
            elCorteIngles,
            prenatal
        }

        public DTOHolding() : base()
        {
            companies = new List<DTOContact>();
            clusters = new List<DTOCustomerCluster>();
        }

        public DTOHolding(Guid oGuid) : base(oGuid)
        {
            companies = new List<DTOContact>();
            clusters = new List<DTOCustomerCluster>();
        }

        public static DTOHolding Factory(DTOEmp oEmp, string sNom)
        {
            DTOHolding retval = new DTOHolding();
            {
                var withBlock = retval;
                withBlock.emp = oEmp;
                withBlock.Nom = sNom;
            }
            return retval;
        }

        public static DTOHolding Wellknown(Wellknowns id)
        {
            DTOHolding retval = null;
            switch (id)
            {
                case Wellknowns.elCorteIngles:
                    {
                        retval = new DTOHolding(new Guid("8B07C540-1DA6-48E2-B137-563BFDD4218B"));
                        break;
                    }

                case Wellknowns.prenatal:
                    {
                        retval = new DTOHolding(new Guid("1908D0F6-4B81-464F-B5CC-BE1B70AC8B3B"));
                        break;
                    }
            }
            return retval;
        }
    }
}
