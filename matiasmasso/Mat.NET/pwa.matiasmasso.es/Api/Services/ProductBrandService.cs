using DTO;
using System.Collections.Generic;

namespace Api.Services
{
    public class ProductBrandService
    {
        public static ProductBrandModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var model = db.Tpas.Where(x=>x.Guid == guid)
                    .Select(x=>new ProductBrandModel(guid)
                    {
                        Emp = x.Emp,
                        Ord= x.Ord,
                        Proveidor=x.Proveidor,
                        Cnap = x.CnapGuid,
                        MadeIn = x.MadeIn,
                        CodiMercancia=x.CodiMercancia,
                        CodDist=x.CodDist
                    }).FirstOrDefault();
                var retval = Load(db, model);
                return retval;
            }
        }

        public static ProductBrandModel? Load(Entities.MaxiContext db, ProductBrandModel? model)
        {
            var retval = model;
            if (model != null)
            {
                var entities = db.VwProductLangTexts
                .Where(x => x.Guid == model.Guid)
                .ToList();
                if (entities.Count() > 0)
                {
                    retval!.Nom.Load(
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "ESP")?.BrandText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "CAT")?.BrandText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "ENG")?.BrandText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "POR")?.BrandText
                        );
                    retval!.Excerpt.Load(
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "ESP")?.BrandText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "CAT")?.BrandText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "ENG")?.BrandText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "POR")?.BrandText
                        );
                    retval.Content.Load(
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "ESP")?.BrandText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "CAT")?.BrandText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "ENG")?.BrandText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "POR")?.BrandText
                        );
                }
            }
            return retval;
        }


        public static bool Update(ProductBrandModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Tpa? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Tpa();
                    db.Tpas.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Tpas.Find(guid);

                if (entity == null) throw new Exception("Brand not found");

                entity.Emp = value.Emp;
                entity.Ord = value.Ord;
                entity.Proveidor = value.Proveidor;
                entity.CnapGuid = value.Cnap;
                entity.MadeIn = value.MadeIn;
                entity.CodiMercancia = value.CodiMercancia;
                entity.CodDist= value.CodDist;
                entity.Obsoleto = value.Obsoleto;

                LangTextService.Update(db, value.Nom);
                LangTextService.Update(db, value.Excerpt);
                LangTextService.Update(db, value.Content);

                db.SaveChanges();
                return true;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Tpas.Find(guid);
                if (entity != null)
                {
                    db.Tpas.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }

    }

    public class ProductBrandsService
    {
        public static List<ProductBrandModel> All(Entities.MaxiContext db, int emp)
        {
            var retval = new List<ProductBrandModel>();
            var entities = db.VwBrands
                .Where(x => x.Emp == emp)
                .OrderBy(x=>x.Ord)
                .ToList();

            foreach (var x in entities)
            {
                var brand = new ProductBrandModel(x.Guid);
                brand.Nom.Load(x.Esp, x.Cat, x.Eng, x.Por);
                brand.Obsoleto = x.Obsoleto;
                retval.Add(brand);
            }

            return retval;
        }
    }
}
