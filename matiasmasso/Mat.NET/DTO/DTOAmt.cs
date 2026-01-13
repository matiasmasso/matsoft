using System;

namespace DTO
{
    public class DTOAmt
    {
        public DTOCur Cur { get; set; }
        public decimal Eur { get; set; }
        public decimal Val { get; set; }

        public DTOAmt() : base()
        {
            //Cur = DTOCur.Eur();
        }

        public static DTOAmt Factory(decimal dcEur = 0)
        {
            dcEur = Math.Round(dcEur, 2);
            var retval = Factory(dcEur, DTOCur.Eur(), dcEur);
            return retval;
        }

        public static DTOAmt Factory(DTOCur ocur, decimal DcDivisa = 0)
        {
            if (ocur == null)
                ocur = DTOApp.Current.Cur;
            var retval = ocur.amtFromDivisa(DcDivisa);
            return retval;
        }

        public static DTOAmt Factory(decimal DcEur, string scur, decimal DcVal = 0)
        {
            DTOAmt retval = new DTOAmt();
            {
                var withBlock = retval;
                withBlock.Eur = DcEur;
                withBlock.Cur = DTOCur.Factory(scur);
                withBlock.Val = DcVal;
            }
            return retval;
        }


        public static DTOAmt Factory(decimal DcEur, DTOCur ocur, decimal DcVal)
        {
            if (ocur == null)
                ocur = DTOApp.Current.Cur;
            DTOAmt retval = new DTOAmt();
            {
                var withBlock = retval;
                withBlock.Eur = DcEur;
                withBlock.Val = DcVal;
                withBlock.Cur = ocur;
            }
            return retval;
        }

        public static DTOAmt Factory(params DTOAmt[] oAmts)
        {
            var retval = DTOAmt.Factory();
            foreach (var oAmt in oAmts)
                retval.Add(oAmt);
            return retval;
        }

        public Compact ToCompact()
        {
            var retval = DTOAmt.Compact.Factory(this.Eur, this.Cur.Tag, this.Val);
            return retval;
        }

        public static DTOAmt Empty(DTOCur ocur = null/* TODO Change to default(_) if this is not a reference type */)
        {
            var retval = DTOAmt.Factory(ocur);
            return retval;
        }

        public static DTOAmt FromQtyPriceDto(int iQty, DTOAmt oPrice, decimal DcDto = 0)
        {
            DTOAmt retval = null;
            if (oPrice != null)
            {
                retval = oPrice.Times(iQty);
                if (DcDto != 0)
                {
                    DTOAmt oDto = retval.Percent(DcDto);
                    retval = retval.Substract(oDto);
                }
            }
            return retval;
        }

        public static DTOAmt FromBaseImponible(DTOAmt oBase, decimal DcIva, decimal DcReq = 0, decimal DcIrpf = 0)
        {
            DTOAmt oIVAAmt = null;
            DTOAmt oReqAmt = null;
            DTOAmt oIRPFAmt = null;

            if (DcIva != 0)
            {
                oIVAAmt = DTOAmt.Factory(oBase.Cur, Math.Round(oBase.Eur * DcIva / 100, 2, MidpointRounding.AwayFromZero));
                if (DcReq != 0)
                    oReqAmt = DTOAmt.Factory(oBase.Cur, Math.Round(oBase.Eur * DcReq / 100, 2, MidpointRounding.AwayFromZero));
            }
            if (DcIrpf != 0)
                oIRPFAmt = DTOAmt.Factory(oBase.Cur, Math.Round(oBase.Eur * DcIrpf / 100, 2, MidpointRounding.AwayFromZero));

            DTOAmt retval = FromBaseImponible(oBase, oIVAAmt, oReqAmt, oIRPFAmt);
            return retval;
        }

        public static DTOAmt FromBaseImponible(DTOAmt oBase, DTOAmt oIva, DTOAmt oReq = null, DTOAmt oIrpf = null)
        {
            DTOAmt retval = null;
            if (oBase == null)
                retval = DTOAmt.Factory();
            else
            {
                retval = oBase.Clone();
                if (oIva != null)
                {
                    retval.Add(oIva);
                    if (oReq != null)
                        retval.Add(oReq);
                }
                if (oIrpf != null)
                    retval.Substract(oIrpf);
            }
            return retval;
        }

        public DTOAmt Times(decimal DcFactor)
        {
            if (Cur == null)
                Cur = DTOApp.Current.Cur;
            DTOAmt retval = new DTOAmt();
            retval.Eur = Math.Round(Eur * DcFactor, 2);

            retval.Val = Math.Round(Val * DcFactor, this.Cur.Decimals);
            retval.Cur = Cur;
            return retval;
        }

        public DTOAmt DividedBy(decimal DcFactor)
        {
            DTOAmt retval = new DTOAmt();
            retval.Eur = Eur / DcFactor;
            retval.Val = Val / DcFactor;
            retval.Cur = Cur;
            return retval;
        }

        public DTOAmt Percent(decimal DcTipus)
        {
            DTOAmt retval = new DTOAmt();
            retval.Eur = Math.Round(Eur * DcTipus / 100, Cur.Decimals, MidpointRounding.AwayFromZero);
            retval.Val = Math.Round(Val * DcTipus / 100, Cur.Decimals, MidpointRounding.AwayFromZero);
            retval.Cur = Cur;
            return retval;
        }

        public void AddPercent(decimal DcPercentatge)
        {
            if (DcPercentatge != 0)
            {
                decimal dcEur = Math.Round(Eur * DcPercentatge / 100, Cur.Decimals, MidpointRounding.AwayFromZero);
                decimal dcVal = Math.Round(Val * DcPercentatge / 100, Cur.Decimals, MidpointRounding.AwayFromZero);
                Eur += dcEur;
                Val += dcVal;
            }
        }

        public DTOAmt DeductPercent(decimal DcPercentatge)
        {
            AddPercent(-DcPercentatge);
            return this;
        }

        public DTOAmt Clone()
        {
            DTOAmt retval = new DTOAmt();
            {
                var withBlock = retval;
                withBlock.Eur = Eur;
                withBlock.Val = Val;
                withBlock.Cur = Cur;
            }
            return retval;
        }

        public DTOAmt Add(DTOAmt oAmtToAdd)
        {
            if (oAmtToAdd != null)
            {
                if (Eur == 0 & Val == 0)
                    Cur = oAmtToAdd.Cur;
                Eur += oAmtToAdd.Eur;
                Val += oAmtToAdd.Val;
            }
            return this.Clone();
        }

        public DTOAmt Substract(DTOAmt oSubstraend)
        {
            if (oSubstraend != null)
            {
                if (Eur == 0 & Val == 0)
                    Cur = oSubstraend.Cur;
                Eur -= oSubstraend.Eur;
                Val -= oSubstraend.Val;
            }
            return this.Clone();
        }

        public DTOAmt Inverse()
        {
            var retval = DTOAmt.Factory(-Eur, Cur, -Val);
            return retval;
        }

        public bool IsPositive()
        {
            bool retval = Eur > 0;
            return retval;
        }

        public bool IsNegative()
        {
            bool retval = Eur < 0;
            return retval;
        }

        public bool isZero()
        {
            bool retval = Eur == 0;
            return retval;
        }

        public bool IsNotZero()
        {
            bool retval = Eur != 0;
            return retval;
        }

        public bool IsGreaterThan(DTOAmt oComparedAmt)
        {
            bool retval = false;
            if (oComparedAmt != null)
            {
                decimal currentEur = Eur;
                decimal ComparedEur = oComparedAmt.Eur;
                retval = (currentEur > ComparedEur);
            }
            return retval;
        }

        public bool IsGreaterOrEqualThan(DTOAmt oComparedAmt)
        {
            bool retval = false;
            if (oComparedAmt != null)
            {
                decimal currentEur = Eur;
                decimal ComparedEur = oComparedAmt.Eur;
                retval = (currentEur >= ComparedEur);
            }
            return retval;
        }

        public string CurFormatted(bool BlankIfZero = true)
        {
            return DTOAmt.CurFormatted(this, BlankIfZero);
        }

        public static string CurFormatted(DTOAmt oAmt, bool BlankIfZero = true)
        {
            string retval = "";
            if (oAmt != null)
            {
                if (oAmt.Cur != null)
                {
                    if (oAmt.Val != 0 | !BlankIfZero)
                        retval = oAmt.Val.ToString(oAmt.Cur.formatString());
                }
            }
            return retval;
        }
        public static string CurFormatted(DTOAmt.Compact oAmt, bool BlankIfZero = true)
        {
            string retval = CurFormatted(oAmt.ToAmt(), BlankIfZero);
            return retval;
        }

        public static string CurFormatted(decimal dcEur, bool BlankIfZero = true)
        {
            var oAmt = DTOAmt.Factory(dcEur);
            string retval = CurFormatted(oAmt, BlankIfZero);
            return retval;
        }


        public string Formatted()
        {
            string retval = Val.ToString("#,##0.00;-#,##0.00;#");
            return retval;
        }

        public DTOAmt absolute()
        {
            DTOAmt retVal = null;
            if (IsNegative())
                retVal = Inverse();
            else
                retVal = this;
            return retVal;
        }

        public new bool Equals(object oCandidate)
        {
            bool retval = false;
            if (oCandidate is DTOAmt)
            {
                if (oCandidate != null)
                {
                    DTOAmt oCandidateAmt = (DTOAmt)oCandidate;
                    if (Cur.Tag == oCandidateAmt.Cur.Tag)
                        retval = (Eur == oCandidateAmt.Eur && Val == oCandidateAmt.Val);
                }
            }
            return retval;
        }

        public bool unEquals(object oCandidate)
        {
            bool retval = !Equals(oCandidate);
            return retval;
        }

        public static DTOAmt import(int iQty, DTOAmt oPrice, decimal DcDto, decimal DcDt2 = 0)
        {
            DTOAmt retval = null;
            if (oPrice != null)
            {
                retval = oPrice.Times(iQty);
                if (DcDto != 0)
                {
                    DTOAmt oDto = retval.Percent(DcDto);
                    retval = retval.Substract(oDto);
                }
                if (DcDt2 != 0)
                {
                    DTOAmt oDt2 = retval.Percent(DcDt2);
                    retval = retval.Substract(oDt2);
                }
            }
            return retval;
        }

        public static decimal EurOrDefault(DTOAmt oAmt)
        {
            decimal retval = 0;
            if (oAmt != null)
                retval = oAmt.Eur;
            return retval;
        }

        public DTOAmt Trimmed()
        {
            var ocur = DTOCur.Factory(Cur.Tag);
            var retval = DTOAmt.Factory(Eur, ocur, Val);
            return retval;
        }

        public override string ToString()
        {
            return String.Format("Amt: {0:N2} €", Eur);
        }

        public class Compact
        {
            public DTOCur.Compact Cur { get; set; }
            public decimal Eur { get; set; }
            public decimal Val { get; set; }

            public static Compact Factory(Decimal eur = 0, string cur = "", decimal val = 0)
            {
                Compact retval = new Compact();
                retval.Eur = eur;
                retval.Cur = DTOCur.Compact.Factory(cur == "" ? "EUR" : cur);
                retval.Val = retval.Cur.Tag == "EUR" ? eur : val;
                return retval;
            }

            public static Compact Factory(DTOAmt amt)
            {
                Compact retval = Compact.Factory(amt.Eur, amt.Cur.Tag, amt.Val);
                return retval;
            }

            public DTOAmt ToAmt()
            {
                DTOAmt retval = DTOAmt.Factory(Eur, Cur.Tag, Val);
                return retval;
            }

            public string CurFormatted(bool BlankIfZero = true)
            {
                return ToAmt().CurFormatted(BlankIfZero);
            }

            public override string ToString()
            {
                return String.Format("Amt.Compact: {0:N2} €", Eur);
            }

        }
    }
}
