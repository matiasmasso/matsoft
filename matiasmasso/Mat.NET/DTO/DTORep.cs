using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTORep : DTOContact
    {
        public string NickName { get; set; }
        public DateTime FchAlta { get; set; }
        public DateTime FchBaja { get; set; }
        public bool DisableLiqs { get; set; }
        public decimal ComisionStandard { get; set; }
        public decimal ComisionReducida { get; set; }
        public IVACods IvaCod { get; set; }

        public string Description { get; set; }
        public DTOProveidor.IRPFCods IrpfCod { get; set; }
        public decimal IrpfCustom { get; set; }
        public DTOProveidor RaoSocialFacturacio { get; set; }
        [JsonIgnore]
        public Byte[] Img48 { get; set; }
        [JsonIgnore]
        public Byte[] Foto { get; set; }
        public DTOIban Iban { get; set; }

        public List<DTORepProduct> RepProducts { get; set; }



        public enum Wellknowns
        {
            NotSet,
            Josep,
            Enric,
            Rosillo
        }

        public enum IVACods
        {
            Exento,
            Standard
        }

        public DTORep() : base()
        {
        }

        public DTORep(Guid oGuid) : base(oGuid)
        {
        }

        public static DTORep Wellknown(DTORep.Wellknowns id)
        {
            DTORep retval = null;
            string sGuid = "";
            switch (id)
            {
                case DTORep.Wellknowns.Josep:
                    {
                        sGuid = "4A941105-6FDC-44C2-B2A4-267F050C41A1";
                        break;
                    }

                case DTORep.Wellknowns.Rosillo:
                    {
                        sGuid = "59A734EE-67D9-4B0D-86E6-94154CAAF733";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTORep(oGuid);
            }
            return retval;
        }

        public string NicknameOrNom()
        {
            string retval = "";
            if (NickName.isNotEmpty())
                retval = NickName;
            else if (base.FullNom.isNotEmpty())
                retval = base.FullNom;
            else if (base.Nom.isNotEmpty())
                retval = base.Nom;
            else
                retval = base.Guid.ToString();
            return retval;
        }

        public static decimal IVAtipus(DTORep oRep, DateTime DtFch = default(DateTime))
        {
            decimal retval = 0;
            switch (oRep.IvaCod)
            {
                case DTORep.IVACods.Exento:
                    {
                        break;
                    }

                case DTORep.IVACods.Standard:
                    {
                        retval = DTOTax.closestTipus(DTOTax.Codis.iva_Standard, DtFch);
                        break;
                    }
            }
            return retval;
        }

        public static DTORep FromContact(DTOContact oContact)
        {
            DTORep retval = null;
            if (oContact == null)
                retval = new DTORep();
            else
            {
                retval = new DTORep(oContact.Guid);
                {
                    var withBlock = retval;
                    withBlock.Emp = oContact.Emp;
                    withBlock.Nom = oContact.Nom;
                    withBlock.NomComercial = oContact.NomComercial;
                    withBlock.Nifs = oContact.Nifs;
                    withBlock.Address = oContact.Address;
                    withBlock.ContactClass = oContact.ContactClass;
                    withBlock.Lang = oContact.Lang;
                    withBlock.Rol = oContact.Rol;
                }
            }
            return retval;
        }

        public static decimal Irpf(DTORep oRep, DateTime DtFch = default(DateTime))
        {
            decimal retval = 0;
            DTOProveidor.IRPFCods oIrpfCod = oRep.IrpfCod;
            if (oRep.RaoSocialFacturacio != null)
                oIrpfCod = oRep.RaoSocialFacturacio.IRPF_Cod;
            switch (oIrpfCod)
            {
                case DTOProveidor.IRPFCods.exento:
                    {
                        break;
                    }

                case DTOProveidor.IRPFCods.reducido:
                    {
                        retval = DTOTax.closestTipus(DTOTax.Codis.irpf_Professionals_Reduit, DtFch);
                        break;
                    }

                case DTOProveidor.IRPFCods.standard:
                    {
                        retval = DTOTax.closestTipus(DTOTax.Codis.irpf_Professionals_Standard, DtFch);
                        break;
                    }

                case DTOProveidor.IRPFCods.custom:
                    {
                        retval = oRep.IrpfCustom;
                        break;
                    }
            }
            return retval;
        }

        public static DTOProveidor RaoSocialFacturacioOrSelf(DTORep oRep)
        {
            DTOProveidor retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oRep.RaoSocialFacturacio == null)
                retval = DTOProveidor.FromContact(oRep);
            else
                retval = oRep.RaoSocialFacturacio;
            return retval;
        }

        public new DTOGuidNom ToGuidNom()
        {
            string nom = this.NicknameOrNom();
            DTOGuidNom retval = DTOGuidNom.Factory(this.Guid, this.NicknameOrNom());
            return retval;
        }

    }
}
