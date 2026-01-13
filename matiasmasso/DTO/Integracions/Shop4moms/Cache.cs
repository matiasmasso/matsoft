using DTO.Integracions.Redsys;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Shop4moms
{
    public class Cache : DTO.CacheDTO
    {
        public List<GuidDecimal> Offers { get; set; } = new();
        public RouteModel.Collection Routes { get; set; } = new();

        public enum Contents
        {
            About,
            Legal,
            Privacity,
            Conditions
        }

        public Cache() : base(EmpModel.EmpIds.MatiasMasso) { }

        public ProductSkuModel? Sku(Guid? guid) => guid == null ? null : Skus.FirstOrDefault(x => x.Guid == guid);

        public decimal? Price(ProductSkuModel sku)
        {
            var rrpp = base.RetailPrices.FirstOrDefault(x => x.Guid == sku.Guid);
            var offer = Offers.FirstOrDefault(x => x.Guid == sku.Guid);
            var retval = offer == null ? rrpp?.Value : offer.Value;
            return retval;
        }

        public string SkuFullNom(ProductSkuModel? sku, LangDTO lang)
        {
            string retval = string.Empty;
            if (sku != null)
            {
                var category = Categories.First(x => x.Guid == sku.Category);
                retval = string.Format("{0} {1}", category.Nom.Tradueix(lang), sku.Nom.Tradueix(lang));
            }
            return retval;
        }

        public string CategoryNom(ProductSkuModel? sku, LangDTO lang)
        {
            string retval = "";
            if (sku != null)
            {
                var category = Categories.First(x => x.Guid == sku.Category);
                retval = category.Nom.Tradueix(lang);
            }
            return retval;
        }

        public string ImageUrl(ProductCategoryModel category) => Globals.ApiUrl("productCategory/image", category.Guid.ToString() + ".jpg");
        public string ImageUrl(ProductSkuModel sku) => Globals.ApiUrl("productSku/image", sku.Guid.ToString() + ".jpg");

        public decimal? RrppOrOffer(ProductSkuModel sku)
        {
            var rrpp = RetailPrices.FirstOrDefault(x => x.Guid == sku.Guid);
            var offer = Offers.FirstOrDefault(x => x.Guid == sku.Guid);
            var retval = offer == null ? rrpp?.Value : offer.Value;
            return retval;
        }
    }


}
