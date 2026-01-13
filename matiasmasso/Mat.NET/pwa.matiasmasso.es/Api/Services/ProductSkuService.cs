using DTO;

namespace Api.Services
{
    public class ProductSkuService
    {

        public static ProductSkuModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var model = db.Arts.Where(x => x.Guid == guid)
                .Select(x => new ProductSkuModel(guid)
                {
                    Id = x.Art1,
                    Category = x.Category,
                    Ean = x.Cbar,
                    Ref = x.Ref,
                    IsBundle = x.IsBundle,
                    Inherits = Convert.ToBoolean(x.Hereda)
                }).FirstOrDefault();

                var retval = Load(db, model);
                return retval;
            }
        }

        public static ProductSkuModel? Load(Entities.MaxiContext db, ProductSkuModel? model)
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
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "ESP")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "CAT")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "ENG")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "POR")?.SkuText
                        );
                    retval!.NomLlarg.Load(
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.SkuNomLlarg && x.Lang == "ESP")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.SkuNomLlarg && x.Lang == "CAT")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.SkuNomLlarg && x.Lang == "ENG")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.SkuNomLlarg && x.Lang == "POR")?.SkuText
                        );
                    retval!.Excerpt.Load(
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "ESP")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "CAT")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "ENG")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "POR")?.SkuText
                        );
                    retval.Content.Load(
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "ESP")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "CAT")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "ENG")?.SkuText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "POR")?.SkuText
                        );
                }
            }
            return retval;
        }

        public static Byte[]? Thumbnail(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Arts.Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Thumbnail;
            }
        }

        public static Byte[]? Image(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Arts.Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Image;
            }
        }

        public static bool Update(ProductSkuModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Art? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Art();
                    db.Arts.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Arts.Find(guid);

                if (entity == null) throw new Exception("Sku not found");

                entity.Category = value.Category;
                entity.Cbar = value.Ean;
                entity.Ref = value.Ref;
                entity.IsBundle = value.IsBundle;
                entity.Obsoleto = value.Obsoleto;

                LangTextService.Update(db, value.Nom);
                LangTextService.Update(db, value.NomLlarg);
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
                var entity = db.Arts.Find(guid);
                if (entity != null)
                {
                    db.Arts.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }


    }


    public class ProductSkusService
    {
        public static List<ProductSkuModel> All(Entities.MaxiContext db, int emp)
        {
            var retval = new List<ProductSkuModel>();
            var entities = db.VwSkus
                .Where(x => x.Emp == emp)
                .ToList();

            foreach (var x in entities)
            {
                var Sku = new ProductSkuModel(x.Guid);
                Sku.Id = x.Art;
                Sku.Category = (Guid)x.Category!;
                Sku.Ean = x.Cbar;
                Sku.Ref = x.Ref;
                Sku.IsBundle = x.IsBundle;
                Sku.Inherits = x.Hereda != 0;
                Sku.Nom.Load(x.NomCurtEsp, x.NomCurtCat, x.NomCurtEng, x.NomCurtPor);
                Sku.NomLlarg.Load(x.NomLlargEsp, x.NomLlargCat, x.NomLlargEng, x.NomLlargPor);
                Sku.ImgExists = x.ImgExists;
                Sku.Obsoleto = x.Obsoleto;
                retval.Add(Sku);
            }

            return retval;
        }

    }
}
