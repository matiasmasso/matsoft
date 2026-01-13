using System;

namespace DTO
{
    public class DTOContractCodi : DTOBaseGuid
    {
        public enum Wellknowns
        {
            NotSet,
            Reps
        }

        public string Nom { get; set; }
        public bool Amortitzable { get; set; }
        public DTOContract.Collection Contracts { get; set; }

        public DTOContractCodi() : base()
        {
        }

        public DTOContractCodi(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOContractCodi Wellknown(DTOContractCodi.Wellknowns id)
        {
            DTOContractCodi retval = null;
            string sGuid = "";
            switch (id)
            {
                case DTOContractCodi.Wellknowns.Reps:
                    {
                        sGuid = "53FA6E40-89BA-4485-8715-F6EB2FCB990E";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOContractCodi(oGuid);
            }
            return retval;
        }
    }
}
