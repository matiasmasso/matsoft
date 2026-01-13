using Api.Entities;
using Api.Shared;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ProductCategoryService
    {

        public static ProductCategoryModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Find(db, guid);
            }
        }
        public static ProductCategoryModel? Find(Entities.MaxiContext db, Guid guid)
        {

            var retval = db.Stps
                        .AsNoTracking()
                .Where(x => x.Guid == guid)
                .Join(db.VwProductTexts, stp => stp.Guid, langText => langText.Guid, (stp, langText) => new ProductCategoryModel(guid)
                {
                    Brand = stp.Brand,
                    Codi = stp.Codi,
                    Nom = new LangTextModel(stp.Guid, LangTextModel.Srcs.ProductNom, langText.NomEsp, langText.NomCat, langText.NomEng, langText.NomPor),
                    Excerpt = new LangTextModel(stp.Guid, LangTextModel.Srcs.ProductExcerpt, langText.ExcerptEsp, langText.ExcerptCat, langText.ExcerptEng, langText.ExcerptPor),
                    Content = new LangTextModel(stp.Guid, LangTextModel.Srcs.ProductText, langText.ContentEsp, langText.ContentCat, langText.ContentEng, langText.ContentPor),
                    InnerPack = stp.InnerPack,
                    OuterPack = stp.OuterPack,
                    ForzarInnerPack = stp.ForzarInnerPack,
                    MadeIn = stp.MadeIn,
                    HasImage = stp.Image != null,
                    Obsoleto = stp.Obsoleto
                }).FirstOrDefault();

            return retval;
        }

        public static ProductCategoryModel? Load(Entities.MaxiContext db, ProductCategoryModel? model)
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
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "ESP")?.CategoryText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "CAT")?.CategoryText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "ENG")?.CategoryText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "POR")?.CategoryText
                        );
                    retval!.Excerpt.Load(
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "ESP")?.CategoryText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "CAT")?.CategoryText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "ENG")?.CategoryText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "POR")?.CategoryText
                        );
                    retval.Content.Load(
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "ESP")?.CategoryText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "CAT")?.CategoryText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "ENG")?.CategoryText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "POR")?.CategoryText
                        );
                }
            }
            return retval;
        }

        public static Byte[]? Image(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Stps.Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Image;
            }
        }

        public static bool Update(ProductCategoryModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Stp? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Stp();
                    db.Stps.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Stps.Find(guid);

                if (entity == null) throw new System.Exception("Category not found");

                entity.Brand = (Guid)value.Brand!;
                entity.Codi = value.Codi;

                entity.InnerPack = value.InnerPack;
                entity.OuterPack = value.OuterPack;
                entity.ForzarInnerPack = value.ForzarInnerPack;
                entity.MadeIn = value.MadeIn;

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
                var entity = db.Stps.Find(guid);
                if (entity != null)
                {
                    db.Stps.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }

    }


    public class ProductCategoriesService
    {
        public static List<ProductCategoryModel> All(EmpModel.EmpIds emp)
        {
            using (var db = new Entities.MaxiContext())
            {
                return All(db, emp);
            }
        }
        public static List<ProductCategoryModel> All(Entities.MaxiContext db, EmpModel.EmpIds emp)
        {
            var retval = new List<ProductCategoryModel>();

            var depts = db.VwDeptCategories.ToList();


            retval = db.Stps
                .AsNoTracking()
                .Include(x => x.BrandNavigation)
                .Where(x => x.BrandNavigation.Emp == (int)emp)
                .Join(db.VwProductTexts, stp => stp.Guid, langText => langText.Guid, (stp, langText) => new ProductCategoryModel(stp.Guid)
                {
                    Brand = stp.Brand,
                    Codi = stp.Codi,
                    Nom = new LangTextModel(stp.Guid, LangTextModel.Srcs.ProductNom, langText.NomEsp, langText.NomCat, langText.NomEng, langText.NomPor),
                    Excerpt = new LangTextModel(stp.Guid, LangTextModel.Srcs.ProductExcerpt, langText.ExcerptEsp, langText.ExcerptCat, langText.ExcerptEng, langText.ExcerptPor),
                    Content = new LangTextModel(stp.Guid, LangTextModel.Srcs.ProductText, langText.ContentEsp, langText.ContentCat, langText.ContentEng, langText.ContentPor),
                    InnerPack = stp.InnerPack,
                    OuterPack = stp.OuterPack,
                    ForzarInnerPack = stp.ForzarInnerPack,
                    MadeIn = stp.MadeIn,
                    HasImage = stp.Image != null,
                    Obsoleto = stp.Obsoleto || stp.BrandNavigation.Obsoleto
                })
                      .OrderBy(x => x.Obsoleto)
                      //.ThenBy(x => x.Brand)
                      //.ThenBy(x => x.Nom.Esp)
                      .ToList();

            foreach (var category in retval)
            {
                category.Dept = depts.FirstOrDefault(d => d.Category == category.Guid)?.Dept;

            }

            return retval;
        }

        public static List<ProductCategoryModel> FromBrand(Guid brandGuid)
        {
            var retval = new List<ProductCategoryModel>();
            using (var db = new Entities.MaxiContext())
            {
                retval = db.VwCategories
                        .AsNoTracking()
                    .Where(x => x.Brand == brandGuid && x.Obsoleto == false)
                    .OrderBy(x => x.Codi)
                    .ThenBy(x => x.Ord)
                    .Select(x => new ProductCategoryModel(x.Guid)
                    {
                        Brand = brandGuid,
                        Codi = x.Codi,
                        Nom = new LangTextModel(x.Esp, x.Cat, x.Eng, x.Por),
                        HasImage = x.ImgExists != 0
                    })
                    .ToList();
            }

            return retval;
        }

    }

}
