using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCompactCataleg
    {
        public List<DTOCompactBrand> brands { get; set; }
        public string BrandSpriteHash { get; set; }
        public List<DTOCompactSku> skus { get; set; }

        public static List<DTOCompactBrand> CompactBrands(List<DTOProductBrand> oBrands)
        {
            List<DTOCompactBrand> retval = new List<DTOCompactBrand>();
            foreach (DTOProductBrand oBrand in oBrands)
            {
                var oCompactBrand = DTOCompactBrand.Factory(oBrand);
                retval.Add(oCompactBrand);

                foreach (DTOProductCategory oCategory in oBrand.Categories)
                {
                    var oCompactCategory = DTOCompactCategory.Factory(oCategory);
                    oCompactBrand.Categories.Add(oCompactCategory);

                    foreach (DTOProductSku oSku in oCategory.Skus)
                    {
                        var oCompactSku = DTOCompactSku.Factory(oSku);
                        oCompactCategory.Skus.Add(oCompactSku);
                    }
                }
            }
            return retval;
        }
    }

    public class DTOCompactBrand
    {
        public Guid Guid { get; set; }
        public string Nom { get; set; }
        public List<DTOCompactCategory> Categories { get; set; }

        public static DTOCompactBrand Factory(DTOProductBrand oBrand)
        {
            return DTOCompactBrand.Factory(oBrand.Guid, oBrand.Nom.Esp);
        }

        public static DTOCompactBrand Factory(Guid oGuid, string sNom = "")
        {
            DTOCompactBrand retval = new DTOCompactBrand();
            {
                var withBlock = retval;
                withBlock.Guid = oGuid;
                withBlock.Nom = sNom;
                withBlock.Categories = new List<DTOCompactCategory>();
            }
            return retval;
        }

        public class Collection : List<DTOCompactBrand>
        {
            public List<DTOProductBrand> ProductBrands()
            {
                List<DTOProductBrand> retval = new List<DTOProductBrand>();
                foreach (DTOCompactBrand compactBrand in this)
                {
                    DTOProductBrand brand = new DTOProductBrand(compactBrand.Guid);
                    brand.Nom.Esp = compactBrand.Nom;
                    retval.Add(brand);
                    foreach (DTOCompactCategory compactCategory in compactBrand.Categories)
                    {
                        DTOProductCategory category = new DTOProductCategory(compactCategory.Guid);
                        category.Nom.Esp = compactCategory.Nom;
                        brand.Categories.Add(category);
                        foreach (DTOCompactSku compactSku in compactCategory.Skus)
                        {
                            DTOProductSku sku = new DTOProductSku(compactSku.Guid);
                            sku.Nom.Esp = compactSku.Nom;
                            category.Skus.Add(sku);
                        }
                    }
                }
                return retval;
            }
        }

    }

    public class DTOCompactCategory
    {
        public Guid Guid { get; set; }
        public DTOCompactBrand Brand { get; set; }
        public string Nom { get; set; }
        public List<DTOCompactSku> Skus { get; set; }

        public static DTOCompactCategory Factory(DTOProductCategory oCategory)
        {
            return DTOCompactCategory.Factory(oCategory.Guid, oCategory.Nom.Esp);
        }

        public static DTOCompactCategory Factory(Guid oGuid, string sNom = "")
        {
            DTOCompactCategory retval = new DTOCompactCategory();
            {
                var withBlock = retval;
                withBlock.Guid = oGuid;
                withBlock.Nom = sNom;
                withBlock.Skus = new List<DTOCompactSku>();
            }
            return retval;
        }
    }

    public class DTOCompactSku
    {
        public Guid Guid { get; set; }
        public DTOCompactCategory Category { get; set; }
        public string Nom { get; set; }
        public decimal Retail { get; set; }
        public bool Obsoleto { get; set; }
        public string ThumbnailUrl { get; set; }

        public static DTOCompactSku Factory(DTOProductSku oSku)
        {
            return DTOCompactSku.Factory(oSku.Guid, oSku.Nom.Esp);
        }

        public static DTOCompactSku Factory(Guid oGuid, string sNom = "")
        {
            DTOCompactSku retval = new DTOCompactSku();
            {
                var withBlock = retval;
                withBlock.Guid = oGuid;
                withBlock.Nom = sNom;
            }
            return retval;
        }
    }
}