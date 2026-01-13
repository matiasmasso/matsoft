using Api.Shared;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ProductDeptService
    {

        public static ProductDeptModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var model = AppState.DefaultCache().Dept(guid);
                var retval = Load(db, model);
                return retval;
            }
        }


        public static ProductDeptModel? Load(Entities.MaxiContext db, ProductDeptModel? model)
        {
            var retval = model;
            if(model != null)
            {
                var entities = db.VwProductLangTexts
                        .AsNoTracking()
                .Where(x => x.Guid == model.Guid)
                .ToList();
                if (entities.Count() > 0)
                {
                    retval!.Nom.Load(
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "ESP")?.DeptText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "CAT")?.DeptText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "ENG")?.DeptText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductNom && x.Lang == "POR")?.DeptText
                        );
                    retval!.Excerpt.Load(
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "ESP")?.DeptText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "CAT")?.DeptText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "ENG")?.DeptText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductExcerpt && x.Lang == "POR")?.DeptText
                        );
                    retval.Content.Load(
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "ESP")?.DeptText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "CAT")?.DeptText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "ENG")?.DeptText,
                        entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ProductText && x.Lang == "POR")?.DeptText
                        );
                }
            }
            return retval;
        }


        public static Byte[]? Thumbnail(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Depts.Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Banner;
            }
        }

        public static bool Update(ProductDeptModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Dept? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Dept();
                    db.Depts.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.Depts.Find(guid);

                if (entity == null) throw new Exception("Dept not found");

                entity.Brand = value.Brand;
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
                var entity = db.Depts.Find(guid);
                if (entity != null)
                {
                    db.Depts.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }

    }

    public class ProductDeptsService
    {

        public static List<ProductDeptModel> GetValues(EmpModel.EmpIds emp)
        {
            using (var db = new Entities.MaxiContext())
            {
                return All(db, emp);
            }
        }
        public static List<ProductDeptModel> All(Entities.MaxiContext db, EmpModel.EmpIds emp)
        {
            var retval = new List<ProductDeptModel>();
            var entities = db.VwDepts
                        .AsNoTracking()
                .Where(x => x.Emp == (int)emp)
                .ToList();

            foreach (var x in entities)
            {
                var Dept = new ProductDeptModel(x.Guid);
                Dept.Brand = x.Brand;
                Dept.Nom.Load(x.Esp, x.Cat, x.Eng, x.Por);
                Dept.Obsoleto = x.Obsoleto;
                retval.Add(Dept);
            }

            return retval;
        }
    }
}
