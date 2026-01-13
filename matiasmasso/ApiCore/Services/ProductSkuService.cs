using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ProductSkuService
    {

        public static ProductSkuModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var model = db.Arts
                        .AsNoTracking()
                    .Where(x => x.Guid == guid)
                .Select(x => new ProductSkuModel(guid)
                {
                    Id = x.Art1,
                    Category = x.Category,
                    Ean = x.Cbar,
                    Ref = x.Ref,
                    InnerPack = x.InnerPack,
                    ForzarInnerPack = x.ForzarInnerPack,
                    HeredaDimensions = x.HeredaDimensions,
                    NoEcom = x.NoEcom,
                    MadeIn = x.MadeIn,
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
                        .AsNoTracking()
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
                    retval!.NomLlarg?.Load(
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
                return db.Arts
                    .AsNoTracking()
                    .Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Thumbnail;
            }
        }

        public static Byte[]? Image(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Arts
                        .AsNoTracking()
                    .Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Image;
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
                entity.Ref = value.Ref ?? "";

                entity.InnerPack = (short)value.InnerPack;
                entity.ForzarInnerPack = value.ForzarInnerPack;
                entity.HeredaDimensions = value.HeredaDimensions;


                entity.NoEcom = value.NoEcom;
                entity.MadeIn = value.MadeIn;
                entity.IsBundle = value.IsBundle;
                entity.Obsoleto = value.Obsoleto;

                if (value.Nom.HasValue())
                    LangTextService.Update(db, value.Nom);
                if (value.NomLlarg?.HasValue() ?? false)
                    LangTextService.Update(db, value.NomLlarg);
                if (value.Excerpt.HasValue())
                    LangTextService.Update(db, value.Excerpt);
                if (value.Content.HasValue())
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
        public static List<ProductSkuModel> GetValues(EmpModel.EmpIds emp)
        {
            using (var db = new Entities.MaxiContext())
            {
                return All(db, emp);
            }
        }
        public static List<ProductSkuModel> All(Entities.MaxiContext db, EmpModel.EmpIds emp)
        {
            var retval = new List<ProductSkuModel>();

            retval = db.VwSkus
                        .AsNoTracking()
                .Where(x => x.Emp == (int)emp)
                .Select(x => new ProductSkuModel(x.Guid)
                {
                    Id = x.Art,
                    Category = (Guid)x.Category!,
                    Ean = x.Cbar,
                    Ref = x.Ref,
                    InnerPack = x.InnerPack,
                    ForzarInnerPack = x.ForzarInnerPack,
                    HeredaDimensions = x.HeredaDimensions,
                    NoEcom = x.NoEcom,
                    MadeIn = x.MadeIn,
                    IsBundle = x.IsBundle,
                    Inherits = x.Hereda != 0,
                    Nom = new LangTextModel(x.Guid, LangTextModel.Srcs.ProductNom, x.NomCurtEsp, x.NomCurtCat, x.NomCurtEng, x.NomCurtPor),
                    NomLlarg = new LangTextModel(x.Guid, LangTextModel.Srcs.SkuNomLlarg, x.NomLlargEsp, x.NomLlargCat, x.NomLlargEng, x.NomLlargPor),
                    HasImage = x.HasImage == 1,
                    Obsoleto = x.Obsoleto
                }).ToList();


            //var entities = db.VwSkus
            //    .Where(x => x.Emp == (int)emp)
            //    .ToList();

            //foreach (var x in entities)
            //{
            //    var Sku = new ProductSkuModel(x.Guid);
            //    Sku.Id = x.Art;
            //    Sku.Category = (Guid)x.Category!;
            //    Sku.Ean = x.Cbar;
            //    Sku.Ref = x.Ref;
            //    Sku.NoEcom = x.NoEcom;
            //    Sku.IsBundle = x.IsBundle;
            //    Sku.Inherits = x.Hereda != 0;
            //    Sku.Nom.Load(x.NomCurtEsp, x.NomCurtCat, x.NomCurtEng, x.NomCurtPor);
            //    Sku.NomLlarg?.Load(x.NomLlargEsp, x.NomLlargCat, x.NomLlargEng, x.NomLlargPor);
            //    Sku.ImgExists = x.ImgExists;
            //    Sku.Obsoleto = x.Obsoleto;
            //    retval.Add(Sku);
            //}

            return retval;
        }

    }
}
