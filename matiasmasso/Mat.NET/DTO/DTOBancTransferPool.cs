using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOBancTransferPool : DTOBaseGuid
    {
        public DTOUser User { get; set; }
        public DTOBanc BancEmissor { get; set; }
        public DateTime Fch { get; set; }
        public string Ref { get; set; }
        public DTOAmt Expenses { get; set; }
        public List<DTOBancTransferBeneficiari> Beneficiaris { get; set; }
        public DTOCca Cca { get; set; }
        public List<DTOPnd> Pnds { get; set; }

        public DTOBancTransferPool() : base()
        {
        }

        public DTOBancTransferPool(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOBancTransferPool Factory(DTOUser oUser, DateTime DtFch = default(DateTime), DTOBanc oBancEmissor = null/* TODO Change to default(_) if this is not a reference type */, DTOAmt oExpenses = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOBancTransferPool retval = new DTOBancTransferPool();
            {
                var withBlock = retval;
                withBlock.User = oUser;
                withBlock.Fch = DtFch == default(DateTime) ? DTO.GlobalVariables.Today() : DtFch;
                withBlock.BancEmissor = oBancEmissor;
                withBlock.Expenses = oExpenses;
                withBlock.Beneficiaris = new List<DTOBancTransferBeneficiari>();
            }
            return retval;
        }

        public DTOAmt Total()
        {
            var retval = DTOAmt.Empty();
            foreach (var oBeneficiari in Beneficiaris)
                retval.Add(oBeneficiari.Amt);
            return retval;
        }

        public string MsgId()
        {
            string retval = string.Format("{0}.{1:yyyyMMddhhmmss}", Cca.formattedId(), DTO.GlobalVariables.Now());
            return retval;
        }

        public string DefaultFilename(string sBeneficiariNom = "")
        {
            string sufix = "";
            if (!string.IsNullOrEmpty(sBeneficiariNom))
                sufix = sBeneficiariNom;
            else if (Beneficiaris.Count == 1)
                sufix = Beneficiaris.First().Contact.Nom;
            else
            {
                string firstConcepte = Beneficiaris.First().Concepte;
                bool sameConcepte = !Beneficiaris.Exists(x => x.Concepte != firstConcepte);
                if (sameConcepte)
                    sufix = firstConcepte;
            }
            string retval = string.Format("SepaCreditTransfer {0} {1}.xml", Cca.formattedId(), sufix);
            return retval;
        }

        public static void AddBeneficiari(DTOBancTransferPool oBancTransferPool, DTOPgcCta oCta, DTOContact oContact, DTOBankBranch oBankBranch, string sAccount, DTOAmt oAmt, string sConcepte)
        {
            // Quan no hi ha Partida pendent a saldar DTOPnd
            DTOBancTransferBeneficiari retval = new DTOBancTransferBeneficiari();
            {
                var withBlock = retval;
                withBlock.Parent = oBancTransferPool;
                withBlock.Cta = oCta;
                withBlock.Contact = oContact;
                withBlock.BankBranch = oBankBranch;
                withBlock.Account = sAccount;
                withBlock.Amt = oAmt;
                withBlock.Concepte = sConcepte;
            }
            oBancTransferPool.Beneficiaris.Add(retval);
        }

        public static void AddBeneficiari(DTOBancTransferPool oBancTransferPool, DTOPgcCta oCta, DTOContact oContact, DTOIban oIban, DTOAmt oAmt, string sConcepte)
        {
            DTOBankBranch oBankBranch = null/* TODO Change to default(_) if this is not a reference type */;
            string sAccount = "";
            if (oIban != null)
            {
                oBankBranch = oIban.BankBranch;
                sAccount = oIban.Digits;
            }
            DTOBancTransferBeneficiari retval = new DTOBancTransferBeneficiari();
            {
                var withBlock = retval;
                withBlock.Parent = oBancTransferPool;
                withBlock.Cta = oCta;
                withBlock.Contact = oContact;
                withBlock.BankBranch = oBankBranch;
                withBlock.Account = sAccount;
                withBlock.Amt = oAmt;
                withBlock.Concepte = sConcepte;
            }
            oBancTransferPool.Beneficiaris.Add(retval);
        }

        public static void AddPnd(ref DTOBancTransferPool oBancTransferPool, DTOPnd oPnd, DTOBankBranch oBankBranch, string sAccount, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOLang.ESP();

            DTOBancTransferBeneficiari oBeneficiari = oBancTransferPool.Beneficiaris.Find(x => x.Contact.Equals(oPnd.Contact) & x.Account == sAccount);

            if (oBeneficiari == null)
            {
                oBeneficiari = new DTOBancTransferBeneficiari();
                {
                    var withBlock = oBeneficiari;
                    withBlock.Parent = oBancTransferPool;
                    withBlock.Cta = oPnd.Cta;
                    withBlock.Contact = oPnd.Contact;
                    withBlock.BankBranch = oBankBranch;
                    withBlock.Account = sAccount;
                    withBlock.Amt = DTOAmt.Empty();
                }
                oBancTransferPool.Beneficiaris.Add(oBeneficiari);
            }

            {
                var withBlock = oBeneficiari;
                withBlock.Pnds.Add(oPnd);
                withBlock.Concepte = DTOPnd.Concepte(withBlock.Pnds);
                switch (oPnd.Cod)
                {
                    case DTOPnd.Codis.Creditor:
                        {
                            withBlock.Amt.Add(oPnd.Amt);
                            break;
                        }

                    case DTOPnd.Codis.Deutor:
                        {
                            withBlock.Amt.Substract(oPnd.Amt);
                            break;
                        }
                }
            }
        }
    }

    public class DTOBancTransferBeneficiari : DTOBaseGuid
    {
        public DTOBancTransferPool Parent { get; set; }
        public DTOPgcCta Cta { get; set; }
        public DTOContact Contact { get; set; }
        public DTOAmt Amt { get; set; }
        public DTOBankBranch BankBranch { get; set; }
        public string Account { get; set; }
        public string Concepte { get; set; }
        public List<DTOPnd> Pnds { get; set; }

        public bool IsOurBankAccount { get; set; }

        public DTOBancTransferBeneficiari() : base()
        {
            Pnds = new List<DTOPnd>();
        }

        public DTOBancTransferBeneficiari(Guid oGuid) : base(oGuid)
        {
            Pnds = new List<DTOPnd>();
        }

        public static List<DTOCcb> Ccbs(DTOBancTransferBeneficiari value)
        {
            // per Remmitance advice
            DTOCca oCca = value.Parent.Cca;
            List<DTOCcb> retval = oCca.Items.FindAll(x => value.Contact.Equals(x.Contact));
            return retval;
        }
    }
}
