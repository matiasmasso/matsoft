using Api.Entities;
using DTO;

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
            var model = Api.Models.AppState.DefaultCache().Category(guid);
            var retval = Load(db, model);
            return retval;
        }

        public static ProductCategoryModel? Load(Entities.MaxiContext db, ProductCategoryModel? model)
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
        public static List<ProductCategoryModel> All(Entities.MaxiContext db, int emp)
        {
            var retval = new List<ProductCategoryModel>();
            var entities = db.VwCategories
                .Where(x => x.Emp == emp)
                .OrderBy(x => x.Codi)
                .ThenBy(x => x.Ord)
                .ToList();

            var depts = db.VwDeptCategories
                .ToList();

            foreach (var x in entities)
            {
                var Category = new ProductCategoryModel(x.Guid);
                Category.Brand = x.Brand;
                Category.Dept = depts.FirstOrDefault(d => d.Category == x.Guid)?.Dept;
                Category.Codi = x.Codi;
                Category.Nom.Load(x.Esp, x.Cat, x.Eng, x.Por);
                Category.HasImage = x.ImgExists != 0;
                Category.Obsoleto = x.Obsoleto;
                retval.Add(Category);
            }

            return retval;
        }

        public static List<ProductCategoryModel> FromBrand(Guid brandGuid)
        {
            var retval = new List<ProductCategoryModel>();
            using (var db = new Entities.MaxiContext())
            {
                retval = db.VwCategories
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
