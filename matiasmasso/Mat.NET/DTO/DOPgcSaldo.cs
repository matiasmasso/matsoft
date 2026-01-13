using System.Collections.Generic;

namespace DTO
{
    public class DTOPgcSaldo
    {
        public DTOExercici Exercici { get; set; }
        public DTOPgcEpgBase Epg { get; set; }
        public DTOContact Contact { get; set; }
        public DTOAmt Debe { get; set; }
        public DTOAmt Haber { get; set; }
        public DTOAmt Pendent { get; set; }
        // Property SdoDeudor As DTOAmt
        // Property SdoCreditor As DTOAmt

        public enum Signes
        {
            Deutor,
            Creditor
        }

        public static DTOPgcSaldo Factory(DTOExercici oExercici, DTOPgcCta oCta, DTOContact oContact = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOPgcSaldo retval = new DTOPgcSaldo();
            {
                var withBlock = retval;
                withBlock.Exercici = oExercici;
                withBlock.Epg = oCta;
                withBlock.Contact = oContact;
            }
            return retval;
        }

        public bool IsDeutor()
        {
            bool retval = false;
            if (Debe != null)
            {
                if (Debe.IsGreaterThan(Haber))
                    retval = true;
            }
            return retval;
        }

        public bool IsCreditor()
        {
            bool retval = false;
            if (Haber != null)
            {
                if (Haber.IsGreaterThan(Debe))
                    retval = true;
            }
            return retval;
        }

        public bool IsNotZero()
        {
            bool retval = IsDeutor() | IsCreditor();
            return retval;
        }

        public Signes Signe()
        {
            var oZero = DTOAmt.Factory();
            DTOAmt oDebe = Debe ?? oZero;
            DTOAmt oHaber = Haber ?? oZero;
            Signes retval = oDebe.IsGreaterOrEqualThan(oHaber) ? Signes.Deutor : Signes.Creditor;
            return retval;
        }

        public DTOAmt SdoDeudor()
        {
            var oZero = DTOAmt.Factory();
            DTOAmt oDebe = Debe ?? oZero;
            DTOAmt oHaber = Haber ?? oZero;
            DTOAmt retval = oDebe.Clone().Substract(oHaber);
            return retval;
        }

        public DTOAmt SdoCreditor()
        {
            var oZero = DTOAmt.Factory();
            DTOAmt oDebe = Debe ?? oZero;
            DTOAmt oHaber = Haber ?? oZero;
            DTOAmt retval = oHaber.Clone().Substract(oDebe);
            return retval;
        }

        public static void UpdateSaldo(ref DTOAmt oSaldo, DTOCcb oCcb)
        {
            DTOPgcCta oCta = oCcb.Cta;
            if ((int)oCta.Act == (int)oCcb.Dh)
                oSaldo.Add(oCcb.Amt);
            else
                oSaldo.Substract(oCcb.Amt);
        }

        public static void AddSaldo(DTOPgcSaldo oBase, DTOPgcSaldo oSaldoToAdd)
        {
            if (oSaldoToAdd.Debe != null)
            {
                if (oBase.Debe == null)
                    oBase.Debe = oSaldoToAdd.Debe.Clone();
                else
                    oBase.Debe.Add(oSaldoToAdd.Debe);
            }
            if (oSaldoToAdd.Haber != null)
            {
                if (oBase.Haber == null)
                    oBase.Haber = oSaldoToAdd.Haber.Clone();
                else
                    oBase.Haber.Add(oSaldoToAdd.Haber);
            }
        }

        public static DTOAmt Saldo(DTOPgcSaldo oSaldo)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;

            DTOPgcCta oCta = (DTOPgcCta)oSaldo.Epg;
            if ((int)oCta.Act == (int)DTOPgcCta.Bal(oCta))
            {
                if (oCta.Act == DTOPgcCta.Acts.Deutora)
                {
                    if (oSaldo.SdoDeudor() == null)
                    {
                        if (oSaldo.SdoCreditor() != null)
                            retval = oSaldo.SdoCreditor().Inverse();
                    }
                    else
                        retval = oSaldo.SdoDeudor().Substract(oSaldo.SdoCreditor());
                }
                else if (oSaldo.SdoCreditor() == null)
                {
                    if (oSaldo.SdoDeudor() != null)
                        retval = oSaldo.SdoDeudor().Inverse();
                }
                else
                    retval = oSaldo.SdoCreditor().Substract(oSaldo.SdoDeudor());
            }
            else if (oCta.Act == DTOPgcCta.Acts.Deutora)
            {
                if (oSaldo.SdoDeudor() == null)
                {
                    if (oSaldo.SdoCreditor() != null)
                        retval = oSaldo.SdoCreditor().Inverse();
                }
                else
                    retval = oSaldo.SdoDeudor().Substract(oSaldo.SdoCreditor());
            }
            else if (oSaldo.SdoCreditor() == null)
            {
                if (oSaldo.SdoDeudor() != null)
                    retval = oSaldo.SdoDeudor().Inverse();
            }
            else
                retval = oSaldo.SdoCreditor().Substract(oSaldo.SdoDeudor());
            return retval;
        }

        public static List<DTOPgcSaldo> ResumTotsDigits(List<DTOPgcSaldo> oSaldos)
        {
            List<DTOPgcSaldo> retval = new List<DTOPgcSaldo>();
            DTOPgcEpgBase oLastCta = new DTOPgcEpgBase();
            DTOPgcSaldo oItem = null;
            foreach (DTOPgcSaldo oSaldo in oSaldos)
            {
                if (!oSaldo.Epg.Equals(oLastCta))
                {
                    oLastCta = oSaldo.Epg;
                    oItem = new DTOPgcSaldo();
                    {
                        var withBlock = oItem;
                        withBlock.Exercici = oSaldo.Exercici;
                        withBlock.Epg = oLastCta;
                    }
                    retval.Add(oItem);
                }


                DTOPgcSaldo.AddSaldo(oItem, oSaldo);
            }
            return retval;
        }
    }
}
