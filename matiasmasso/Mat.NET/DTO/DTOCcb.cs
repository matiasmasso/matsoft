using System;

namespace DTO
{
    public class DTOCcb : DTOBaseGuid
    {
        public DTOCca Cca { get; set; }
        public DTOContact Contact { get; set; }
        public DTOPgcCta Cta { get; set; }
        public DTOAmt Amt { get; set; }
        public DhEnum Dh { get; set; }
        public int Lin { get; set; }
        public DTOPnd Pnd { get; set; }

        public enum DhEnum
        {
            notSet,
            debe,
            haber
        }

        public DTOCcb() : base()
        {
        }

        public DTOCcb(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOCcb Factory(DTOCca oCca, DTOAmt oAmt, DTOPgcCta oCta, DTOContact oContact, DTOCcb.DhEnum oDh, DTOPnd oPnd = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOCcb retval = new DTOCcb();
            {
                var withBlock = retval;
                withBlock.Cca = oCca;
                withBlock.Amt = oAmt;
                withBlock.Cta = oCta;
                withBlock.Contact = oContact;
                withBlock.Dh = oDh;
                withBlock.Pnd = oPnd;
            }
            return retval;
        }

        public static DTOAmt Debit(DTOCcb oCcb)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oCcb.Dh == DTOCcb.DhEnum.debe)
                retval = oCcb.Amt;
            return retval;
        }

        public static DTOAmt Credit(DTOCcb oCcb)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oCcb.Dh == DTOCcb.DhEnum.haber)
                retval = oCcb.Amt;
            return retval;
        }

        public static void Arrastra(DTOCcb oCcb, ref DTOAmt oDeure, ref DTOAmt oHaver, ref DTOAmt oSaldo)
        {
            if (oCcb.Dh == DTOCcb.DhEnum.debe)
                oDeure.Add(oCcb.Amt);
            else
                oHaver.Add(oCcb.Amt);

            if ((int)(oCcb.Cta.Act) == (int)(oCcb.Dh))
                oSaldo.Add(oCcb.Amt);
            else
                oSaldo.Substract(oCcb.Amt);
        }
    }
}
