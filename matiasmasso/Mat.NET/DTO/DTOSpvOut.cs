using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOSpvOut
    {
        public int Yea { get; set; }
        //private int pId;
        public DTOCustomer Customer { get; set; }
        public string SuProveedorNum { get; set; }
        public DateTime Fch { get; set; }
        public int Kgs { get; set; }
        public decimal M3 { get; set; }
        public int Bts { get; set; }
        public DTOCustomer.PortsCodes PortsCod { get; set; }
        public int Cod { get; set; } // reparacions=4
        public int Cfp { get; set; }
        public List<DTOSpv> Spvs { get; set; }
        public DTOUser Usr { get; set; }
        public string Etq { get; set; }
        public string Nom { get; set; }
        public string Adr { get; set; }
        public string Cit { get; set; }
        public string Tel { get; set; }
        public string ECB { get; set; }
        public bool Cash { get; set; }
        public decimal Val { get; set; }
        public float Dto { get; set; }
        public float Dpp { get; set; }
        public float Iva { get; set; }
        public float Req { get; set; }
        public bool Recogeran { get; set; }
        public DTODelivery Delivery { get; set; }


        public static DTOSpvOut Factory(DTOUser oUser, DateTime DtFch)
        {
            DTOSpvOut retval = new DTOSpvOut();
            {
                var withBlock = retval;
                withBlock.Usr = oUser;
                withBlock.Fch = DtFch;
            }
            return retval;
        }

        public void RestoreObjects()
        {
            if (Delivery != null)
                Delivery.RestoreObjects();
            foreach (var oSpv in Spvs)
                oSpv.restoreObjects();
        }
    }
}
