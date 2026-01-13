using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ProductSkuModel:ProductModel
    {
        public int Id { get; set; }
        public Guid Category { get; set; }
        public string? Ean { get; set; }
        public string? Ref { get; set; } 
        public LangTextModel? NomLlarg { get; set; }
        public bool Inherits { get; set; }
        public int InnerPack { get; set; }
        public bool ForzarInnerPack { get; set; }
        public bool HeredaDimensions { get; set; }

        public bool ImgExists { get; set; }
        public bool NoEcom { get; set; } // prevents it from being listed on ecommerce platforms or market places
        public Guid? MadeIn { get; set; }
        public bool IsBundle { get; set; }

        public const int IMG_WIDTH = 700;
        public const int IMG_HEIGHT = 800;
        public const int THUMBNAIL_WIDTH = 150;
        public const int THUMBNAIL_HEIGHT = THUMBNAIL_WIDTH * IMG_HEIGHT / IMG_WIDTH;


        public enum Wellknowns
        {
            mamaRoo5grey,
            Ports
        }

        public ProductSkuModel() : base(SourceCods.Sku) {
            NomLlarg = new LangTextModel(Guid, LangTextModel.Srcs.SkuNomLlarg);
        }
        public ProductSkuModel(Guid guid) : base(guid, SourceCods.Sku) {
            NomLlarg = new LangTextModel(Guid, LangTextModel.Srcs.SkuNomLlarg);
        }

        public static ProductSkuModel? Wellknown(Wellknowns id)
        {
            ProductSkuModel? retval = null;
            switch (id)
            {
                case Wellknowns.mamaRoo5grey: retval = new ProductSkuModel(new Guid("390b3fc2-cb7e-4242-a4b9-6ca21c3e86ec")); break;
                case Wellknowns.Ports: retval = new ProductSkuModel(new Guid("50B4DC15-A957-498A-BD95-C47BC75394F2")); break;
            }
            return retval;
        }

        public string RefYNomLlarg(LangDTO lang)
        {
            var sku = ((ProductSkuModel)this);
            var retval = $"{sku.Ref} {sku.NomLlarg?.Tradueix(lang)}";
            return retval.Trim();
        }
        public static string ThumbnailUrl(Guid guid) => Globals.RemoteApiUrl("productSku/thumbnail", string.Format("{0}.jpg", guid.ToString()));
        public new string ThumbnailUrl() => ThumbnailUrl(Guid);
        public static string ImageUrl(Guid guid) => Globals.RemoteApiUrl("productSku/image", string.Format("{0}.jpg", guid.ToString()));
        public string ImageUrl() => ImageUrl(Guid);

        public NavDTO ContextMenu()
        {
            NavDTO retval = new NavDTO();
            retval.AddItem("/sku/" + Guid.ToString(), "ficha", "fitxa", "properties");
            retval.AddItem("", "sellout");
            return retval;
        }

        public string BgColor(int stock, SkuPncModel pnc)
        {
            var retval = "";
            if (stock > 0)
            {
                retval = stock >= (pnc?.Clients ?? 0) ? "BgGreen" : "BgYellow";
            }
            else
            {
                retval = Obsoleto ? "BgGray" : "BgSalmon";
            }
            return retval;
        }

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Id.ToString() + " " + NomLlarg?.Esp + " " + Nom.Esp + " " + (Ean ?? "");
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }


        public override string ToString()
        {
            return String.Format("Sku: {0}",NomLlarg.Esp ?? "?");
        }

        public static string StructuredData(string nom, string description)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<script type=\"\"application/ld+json\"\">");
            sb.AppendLine($"\"@context\":\"https://schema.org\",");
            sb.AppendLine($"\"@type\":\"Product\",");
            sb.AppendLine($"\"name\":\"{nom}\",");
            sb.AppendLine($"\"description\":\"{description}\",");
            sb.AppendLine($"</script>");
            var retval = sb.ToString();
            return retval;
        }
    }

}
