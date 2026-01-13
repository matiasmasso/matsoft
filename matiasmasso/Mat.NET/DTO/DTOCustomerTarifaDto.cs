using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCustomerTarifaDto : DTOBaseGuid
    {
        public DTOBaseGuid CustomerOrChannel { get; set; }
        public string CustomerOrChannelNom { get; set; }
        public DateTime Fch { get; set; }
        public DTOProduct Product { get; set; }
        public decimal Dto { get; set; }
        public Srcs Src { get; set; }
        public string Obs { get; set; }

        public enum Srcs
        {
            notSet,
            canal,
            client
        }

        public new String ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("DTOCustomerTarifaDto: ");
            sb.AppendFormat("{0}%", this.Dto);
            sb.AppendFormat(" {dd/MM/yy}", this.Fch);
            string retval = sb.ToString();
            return retval;
        }

        public DTOCustomerTarifaDto() : base()
        {
        }


        public DTOCustomerTarifaDto(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOCustomerTarifaDto Factory(DTOBaseGuid oCustomerOrChannel, string Nom = "")
        {
            DTOCustomerTarifaDto retval = new DTOCustomerTarifaDto();
            {
                retval.CustomerOrChannel = oCustomerOrChannel;
                if (oCustomerOrChannel is DTOContact)
                    retval.Src = Srcs.client;
                else if (oCustomerOrChannel is DTODistributionChannel)
                    retval.Src = Srcs.canal;
                retval.CustomerOrChannelNom = Nom;
                retval.Fch = DTO.GlobalVariables.Now();
            }
            return retval;
        }

        public static string FullNom(DTOCustomerTarifaDto oCustomerDto, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOApp.Current.Lang;
            string sAllBrands = oLang.Tradueix("todas las marcas", "totes les marques", "all brands");
            string sConcept = (oCustomerDto == null) ? sAllBrands : oCustomerDto.Product.Nom.Esp;
            string retval = string.Format("{0:dd/MM/yy} {1}", oCustomerDto.Fch, sConcept);
            return retval;
        }

        public static decimal ProductDto(List<DTOCustomerTarifaDto> ActiveItems, DTOProduct oProduct)
        {
            decimal retval = 0;

            DTOCustomerTarifaDto oDto = null;
            if (oProduct is DTOProductSku oSku)
            {
                DTOProductCategory oCategory = (DTOProductCategory)oSku.Category;
                DTOProductBrand oBrand = oCategory.Brand;
                oDto = ActiveItems.Find(x => oSku.Equals(x.Product));
                if (oDto == null)
                {
                    oDto = ActiveItems.Find(x => oCategory.Equals(x.Product));
                    if (oDto == null)
                        oDto = ActiveItems.Find(x => oBrand.Equals(x.Product));
                }
            }
            else if (oProduct is DTOProductCategory oCategory)
            {
                DTOProductBrand oBrand = oCategory.Brand;
                oDto = ActiveItems.Find(x => oCategory.Equals(x.Product));
                if (oDto == null)
                    oDto = ActiveItems.Find(x => oBrand.Equals(x.Product));
            }
            else if (oProduct is DTOProductBrand oBrand)
            {
                oDto = ActiveItems.Find(x => oBrand.Equals(x.Product));
            }

            if (oDto == null)
                oDto = ActiveItems.Find(x => x.Product == null);
            if (oDto != null)
                retval = oDto.Dto;
            return retval;
        }
    }
}
