using DocumentFormat.OpenXml.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ProductCategoryModel:ProductModel
    {
        public Guid? Brand { get; set; }
        public Guid? Dept { get; set; }
        public int Codi { get; set; }
        public int Ord { get; set; }

        public int InnerPack { get; set; }
        public int OuterPack { get; set; }
        public bool ForzarInnerPack { get; set; }

        public Guid? MadeIn { get; set; }


        public List<ProductSkuModel> Skus { get; set; } = new();

        public const int IMG_WIDTH = 410;
        public const int IMG_HEIGHT = 410;
        public const int THUMBNAIL_WIDTH = 150;
        public const int THUMBNAIL_HEIGHT = THUMBNAIL_WIDTH * IMG_HEIGHT / IMG_WIDTH;

        public enum Codis
        {
            standard,
            accessories,
            spareparts,
            POS,
            others
        }

        public enum Wellknowns
        {
            rockaRoo,
            dualfix_iSize
        }
        public ProductCategoryModel() : base(SourceCods.Category) { }
        public ProductCategoryModel(Guid guid) : base(guid,SourceCods.Category) { }

        public static Guid? Default() => Wellknown(Wellknowns.dualfix_iSize)?.Guid;
        public static ProductCategoryModel? Wellknown(Wellknowns id)
        {
            ProductCategoryModel? retval = null;
            switch (id)
            {
                case Wellknowns.dualfix_iSize: retval = new ProductCategoryModel(new Guid("7318FF90-5847-4D73-9B5B-4DAFC168810B")); break;
                case Wellknowns.rockaRoo: retval = new ProductCategoryModel(new Guid("FDCAD204-4EF1-49AE-90A9-537AC04FBD19")); break;
            }
            return retval;
        }


        public static string ImageUrl(Guid guid) => Globals.RemoteApiUrl("productCategory/image", string.Format("{0}.jpg", guid.ToString()));
        public string ImageUrl() => Globals.RemoteApiUrl("productCategory/image", string.Format("{0}.jpg", Guid.ToString()));

        public NavDTO ContextMenu()
        {
            NavDTO retval = new NavDTO();
            retval.AddItem("/category/" + Guid.ToString(), "ficha", "fitxa", "properties");
            retval.AddItem("", "sellout");
            return retval;
        }

        public string CamelCase4moms(LangDTO lang)
        {
            //first letter to lower case to match the look&feel of the brand
            var nom = Nom.Tradueix(lang);
            var retval = Char.ToLowerInvariant(nom[0]) + nom.Substring(1);
            if (Codi == (int)ProductCategoryModel.Codis.accessories)
                retval = lang.Tradueix("accesorios","accessoris","accessories","acessórios");
            return retval;
        }

        public override string ToString()
        {
            return Nom.Esp ?? "Category?";
        }
    }
}
