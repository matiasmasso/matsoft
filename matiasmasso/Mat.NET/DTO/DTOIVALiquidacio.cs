using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOIVALiquidacio
    {
        public DTOExercici Exercici { get; set; }
        public int Month { get; set; }
        public DTOCca Cca { get; set; }
        public List<Item> Items { get; set; }

        public static DTOIVALiquidacio Factory(DTOExercici oExercici, int iMonth)
        {
            DTOIVALiquidacio retval = new DTOIVALiquidacio();
            {
                var withBlock = retval;
                withBlock.Exercici = oExercici;
                withBlock.Month = iMonth;
                withBlock.Items = new List<Item>();
            }
            return retval;
        }

        public DTOYearMonth YearMonth()
        {
            return new DTOYearMonth(Exercici.Year, (DTOYearMonth.Months)Month);
        }

        public DateTime Fch()
        {
            return YearMonth().LastFch();
        }

        public DTOCca CcaFactory(DTOUser oUser, List<DTOPgcCta> oAllCtas)
        {
            var retval = DTOCca.Factory(this.Fch(), oUser, DTOCca.CcdEnum.IVA, YearMonth().RawTag().toInteger());
            retval.Concept = string.Format("Hisenda mod.303 declaració IVA {0} {1}", DTOLang.CAT().Mes(Month), Exercici.Year);

            var oItemRep = Items.FirstOrDefault(x => x.Cod == Item.Cods.Repercutit);
            var oItemReq = Items.FirstOrDefault(x => x.Cod == Item.Cods.RecarrecEquivalencia);
            var oItemCom = Items.FirstOrDefault(x => x.Cod == Item.Cods.IntraComunitari);
            var oItemSop = Items.FirstOrDefault(x => x.Cod == Item.Cods.SoportatNacional);

            retval.AddDebit(oItemRep.Quota, DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaRepercutitNacional));
            retval.AddDebit(oItemReq.Quota, DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaRecarrecEquivalencia));
            retval.AddDebit(oItemCom.Quota, DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaRepercutitIntracomunitari));
            retval.AddCredit(oItemSop.Quota, DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaSoportatNacional));
            retval.AddCredit(oItemCom.Quota, DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaSoportatIntracomunitari));
            retval.AddSaldo(DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaDeutor));
            return retval;
        }

        public class Item
        {
            public Cods Cod { get; set; }
            public DTOAmt Base { get; set; }
            public DTOAmt Quota { get; set; }
            public decimal Tipus { get; set; }
            public DTOAmt Saldo { get; set; }

            public enum Cods
            {
                Repercutit,
                RecarrecEquivalencia,
                SoportatNacional,
                IntraComunitari,
                Importacions
            }

            public static Item Factory(Cods oCod, DTOAmt oBase, decimal DcTipus = 0, DTOAmt oQuota = null/* TODO Change to default(_) if this is not a reference type */, DTOAmt oSaldo = null/* TODO Change to default(_) if this is not a reference type */)
            {
                Item retval = new Item();
                {
                    var withBlock = retval;
                    withBlock.Cod = oCod;
                    withBlock.Base = oBase;
                    withBlock.Tipus = DcTipus;
                    withBlock.Quota = oQuota;
                    withBlock.Saldo = oSaldo;
                }
                return retval;
            }
        }
    }
}
