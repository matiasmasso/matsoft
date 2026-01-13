using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCca : DTOBaseGuid
    {
        public DTOExercici Exercici { get; set; }
        public int Id { get; set; }
        public string Concept { get; set; }
        public DateTime Fch { get; set; }
        public CcdEnum Ccd { get; set; }
        public int Cdn { get; set; }
        public DTOBookFra BookFra { get; set; }
        public DTOProjecte Projecte { get; set; }

        public DTOUsrLog UsrLog { get; set; }

        public DTODocFile DocFile { get; set; }
        public DTOBaseGuid @Ref { get; set; }
        public List<DTOCcb> Items { get; set; }

        public List<DTOPnd> Pnds { get; set; }
        public DTOAmt Eur { get; set; }


        public enum CcdEnum
        {
            NotSet = 0,
            AperturaExercisi = 1,
            MigracioPlaComptable = 2,
            Unknown = 3,
            AlbaraBotiga = 5,
            FacturaNostre = 10,
            ReclamacioEfecte = 11,
            Venciment = 12,
            Reemborsament = 14,
            CobramentACompte = 15,
            XecNostre = 16,
            RemesaEfectes = 17,
            VisaCobros = 18,
            RemesaXecs = 19,
            Impagat = 20,
            Cobro = 21,
            IngresXecs = 22,
            XecRebut = 23,
            DespesesRemesa = 25,
            FacturaProveidor = 30,
            Transit = 31,
            FacturaInsercionsPublicitaries = 31,
            TransferNorma34 = 34,
            Manual = 49,
            Pagament = 50,
            DipositIrrevocableCompra = 56,
            Nomina = 60,
            SegSocTc1 = 61,
            RepComisions = 62,
            IAE = 63,
            IBI = 64,
            InteresosNostreFavor = 70,
            IRPF = 80,
            IVA = 81,
            InventariMensual = 87,
            InventariMensualDesvaloritzacio = 88,
            InmovilitzatAlta = 89,
            Amortitzacions = 90,
            InmovilitzatBaixa = 91,
            TancamentComptes = 96,
            ImpostSocietats = 97,
            TancamentExplotacio = 98,
            TancamentBalanç = 99
        }

        public DTOCca() : base()
        {
            Items = new List<DTOCcb>();
            UsrLog = new DTOUsrLog();
        }

        public DTOCca(Guid oGuid) : base(oGuid)
        {
            Items = new List<DTOCcb>();
            UsrLog = new DTOUsrLog();
        }

        public static DTOCca Factory(DateTime DtFch, DTOUser oUser, DTOCca.CcdEnum oCcd, int iCdn = 0)
        {
            DTOCca retval = new DTOCca();
            {
                var withBlock = retval;
                withBlock.Exercici = new DTOExercici(oUser.Emp, DtFch.Year);
                withBlock.Fch = DtFch;
                withBlock.Ccd = oCcd;
                withBlock.Cdn = iCdn;
                withBlock.Items = new List<DTOCcb>();
                withBlock.UsrLog = DTOUsrLog.Factory(oUser);
            }
            return retval;
        }

        public string formattedId()
        {
            string retval = string.Format("{0:0000}{1:0000}", Fch.Year, Id);
            return retval;
        }

        public static string FullNom(DTOCca oCca)
        {
            string retval = "";
            if (oCca != null)
            {
                if (oCca.Fch == default(DateTime))
                    retval = oCca.Concept;
                else
                    retval = string.Format("{0:dd/MM/yy} - {1}", oCca.Fch, oCca.Concept);
            }
            return retval;
        }

        public DTOCcb AddDebit(DTOAmt oAmt, DTOPgcCta oCta, DTOContact oContact = null/* TODO Change to default(_) if this is not a reference type */, DTOPnd oPnd = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOCcb retval = addCcb(oAmt, oCta, oContact, DTOCcb.DhEnum.debe, oPnd);
            return retval;
        }

        public DTOCcb AddCredit(DTOAmt oAmt, DTOPgcCta oCta, DTOContact oContact = null/* TODO Change to default(_) if this is not a reference type */, DTOPnd oPnd = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOCcb retval = addCcb(oAmt, oCta, oContact, DTOCcb.DhEnum.haber, oPnd);
            return retval;
        }


        public DTOCcb AddSaldo(DTOPgcCta oCta, DTOContact oContact = null/* TODO Change to default(_) if this is not a reference type */, DTOPnd oPnd = null/* TODO Change to default(_) if this is not a reference type */)
        {
            var oDebit = DTOAmt.Factory();
            var oCredit = DTOAmt.Factory();
            var oSaldo = DTOAmt.Factory();

            foreach (DTOCcb item in Items)
            {
                switch (item.Dh)
                {
                    case DTOCcb.DhEnum.debe:
                        {
                            oDebit.Add(item.Amt);
                            break;
                        }

                    case DTOCcb.DhEnum.haber:
                        {
                            oCredit.Add(item.Amt);
                            break;
                        }
                }
            }

            DTOCcb.DhEnum oSigne = DTOCcb.DhEnum.debe;
            if (oDebit.Eur > oCredit.Eur)
            {
                oSaldo = oDebit.Substract(oCredit);
                oSigne = DTOCcb.DhEnum.haber;
            }
            else
                oSaldo = oCredit.Substract(oDebit);

            DTOCcb retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oSaldo.IsNotZero())
            {
                retval = DTOCcb.Factory(this, oSaldo, oCta, oContact, oSigne, oPnd);
                Items.Add(retval);
            }
            return retval;
        }


        public DTOCcb addCcb(DTOAmt oAmt, DTOPgcCta oCta, DTOContact oContact, DTOCcb.DhEnum oDh, DTOPnd oPnd = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOCcb retval = DTOCcb.Factory(this, oAmt, oCta, oContact, oDh, oPnd);
            Items.Add(retval);
            return retval;
        }

        public static DTOCca clon(DTOCca oCca, DTOUser oUser)
        {
            DTOCca retval = Factory(oCca.Fch, oUser, oCca.Ccd, oCca.Cdn);
            {
                var withBlock = retval;
                withBlock.Concept = oCca.Concept;
                withBlock.Items = new List<DTOCcb>();
                foreach (DTOCcb oCcb in oCca.Items)
                    withBlock.addCcb(oCcb.Amt, oCcb.Cta, oCcb.Contact, oCcb.Dh);
            }
            return retval;
        }
    }
}
