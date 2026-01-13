using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOProductDownload : DTOBaseGuid
    {
        public DTOBaseGuid Target { get; set; }
        public List<DTOBaseGuidCodNom> Targets { get; set; }
        public DTODocFile DocFile { get; set; }
        public Srcs Src { get; set; }
        public DTOLang Lang { get; set; }
        public DTOLang.Set LangSet { get; set; }
        public bool PublicarAlConsumidor { get; set; }
        public bool PublicarAlDistribuidor { get; set; }

        public bool Obsoleto { get; set; }

        public int FileCount { get; set; }

        // ojo afectan a urls
        public enum Srcs
        {
            notSet,
            instrucciones,
            catalogos,
            compatibilidad,
            despiece,
            imatge_Alta_Resolucio,
            certificat_Homologacio,
            publicacions,
            seguro,
            justificant,
            documentacio,
            proforma
        }

        public enum TargetCods
        {
            Product
        }

        public DTOProductDownload() : base()
        {
            LangSet = new DTO.DTOLang.Set("1111");
            Targets = new List<DTOBaseGuidCodNom>();
        }

        public DTOProductDownload(Guid oGuid) : base(oGuid)
        {
            LangSet = new DTO.DTOLang.Set("1111");
            Targets = new List<DTOBaseGuidCodNom>();
        }

        public static DTOProductDownload Factory(DTOBaseGuid oTarget, List<Exception> exs)
        {
            DTOProductDownload retval = new DTOProductDownload();
            retval.AddTarget(oTarget, exs);
            return retval;
        }

        public bool AddTarget(DTOBaseGuid value, List<Exception> exs)
        {
            bool retval = false;
            DTOBaseGuidCodNom oTarget = null/* TODO Change to default(_) if this is not a reference type */;
            if (value is DTOVehicle)
                oTarget = DTOBaseGuidCodNom.Factory(value.Guid, DTOBaseGuidCodNom.Cods.productBrand, string.Format("Vehicle matricula {0}", ((DTOVehicle)value).Matricula));
            else if (value is DTOLiniaTelefon)
                oTarget = DTOBaseGuidCodNom.Factory(value.Guid, DTOBaseGuidCodNom.Cods.liniaTelefon, string.Format("telefon {0}", ((DTOLiniaTelefon)value).num));
            else if (value is DTOProductBrand)
                oTarget = DTOBaseGuidCodNom.Factory(value.Guid, DTOBaseGuidCodNom.Cods.productBrand, ((DTOProductBrand)value).Nom.Esp);
            else if (value is DTOProductCategory)
                oTarget = DTOBaseGuidCodNom.Factory(value.Guid, DTOBaseGuidCodNom.Cods.productBrand, ((DTOProductCategory)value).Nom.Esp);
            else if (value is DTOProductSku)
                oTarget = DTOBaseGuidCodNom.Factory(value.Guid, DTOBaseGuidCodNom.Cods.productBrand, DTOProductSku.FullNom((DTOProductSku)value));
            else
                exs.Add(new Exception("target no permés"));
            if (exs.Count == 0)
            {
                Targets.Add(oTarget);
                retval = true;
            }
            return retval;
        }

        public static string Nom(DTOProductDownload oDownload)
        {
            string retval = "";
            if (oDownload.Target is DTOProduct)
                retval = DTOProduct.GetNom((DTOProduct)oDownload.Target);
            else if (oDownload.Target is DTOVehicle)
            {
                DTOVehicle oVehicle = (DTOVehicle)oDownload.Target;
                retval = oVehicle.MarcaModelYMatricula();
            }
            return retval;
        }

        public class ProductModel
        {
            public Srcs Src { get; set; }
            public List<File> Files { get; set; }


            public ProductModel(Srcs src)
            {
                this.Src = src;
                this.Files = new List<File>();
            }

            public DTOCatalog Catalog()
            {
                DTOCatalog retval = new DTOCatalog();
                List<DTOCatalog.Brand> brands = this.Files.GroupBy(x => x.BrandGuid).Select(y => new DTOCatalog.Brand { Guid = y.First().BrandGuid, Nom = y.First().BrandNom }).ToList();
                foreach (DTOCatalog.Brand brand in brands)
                {
                    retval.Add(brand);
                    Guid brandGuid = brand.Guid;
                    brand.Categories = this.Files.Where(x => x.BrandGuid.Equals(brandGuid)).GroupBy(y => y.CategoryGuid).Select(z => new DTOCatalog.Category { Guid = z.First().CategoryGuid, Nom = z.First().CategoryNom }).ToList();
                    foreach (DTOCatalog.Category category in brand.Categories)
                    {
                        Guid categoryGuid = category.Guid;
                        category.Skus = this.Files.Where(x => x.CategoryGuid.Equals(categoryGuid)).GroupBy(y => y.SkuGuid).Select(z => new DTOCatalog.Sku { Guid = z.First().SkuGuid, NomCurt = z.First().SkuNom }).ToList();
                    }
                };
                return retval;
            }

            public class File
            {
                public Guid Guid { get; set; }
                public Guid BrandGuid { get; set; }
                public string BrandNom { get; set; }
                public Guid CategoryGuid { get; set; }
                public string CategoryNom { get; set; }
                public Guid SkuGuid { get; set; }
                public string SkuNom { get; set; }
                public string Nom { get; set; }
                public string Features { get; set; }
                public int LogCount { get; set; }
                public string ThumbnailUrl { get; set; }
                public string DownloadUrl { get; set; }

                public File() : base()
                {

                }

            }

        }
    }
}
