using System;

namespace DTO
{
    public class DTOBankBranch : DTOBaseGuid
    {
        public DTOBank Bank { get; set; }
        public string Id { get; set; }
        public DTOLocation Location { get; set; }
        public string Address { get; set; }
        public string Swift { get; set; }
        public string Tel { get; set; }
        public bool Obsoleto { get; set; }

        public DTOBankBranch() : base()
        {
        }

        public DTOBankBranch(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOBankBranch Factory(DTOBank oBank)
        {
            DTOBankBranch retval = new DTOBankBranch();
            {
                var withBlock = retval;
                withBlock.Bank = oBank;
            }
            return retval;
        }

        public static string FullNomAndAddress(DTOBankBranch oBranch)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oBranch != null)
            {
                sb.Append(DTOBank.NomComercialORaoSocial(oBranch.Bank));
                if (oBranch.Location != null)
                {
                    sb.Append(" - ");
                    sb.Append(oBranch.Location.Nom);
                    if (oBranch.Address.isNotEmpty())
                    {
                        sb.Append(" - ");
                        sb.Append(oBranch.Address);
                    }
                }
            }
            string retval = sb.ToString();
            return retval;
        }

        public static string FullNomAndAddressHtml(DTOBankBranch oBranch)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oBranch != null)
            {
                sb.Append(DTOBank.NomComercialORaoSocial(oBranch.Bank));
                if (oBranch.Location != null)
                {
                    if (oBranch.Address.isNotEmpty())
                    {
                        sb.Append("<br/>");
                        sb.Append(oBranch.Address);
                    }
                    sb.Append("<br/>");
                    sb.Append(oBranch.Location.Nom);
                }
            }
            string retval = sb.ToString();
            return retval;
        }
    }
}
