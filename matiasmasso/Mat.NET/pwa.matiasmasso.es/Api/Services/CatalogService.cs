using DTO;
using Microsoft.EntityFrameworkCore;
namespace Api.Services
{
    public class CatalogService
    {
        public static CatalogModel Model(CatalogModel request)
        {
            //Compares client last refresh with api last refresh and with database last time it was updated
            //and reads from database only if needed

            using (var db = new Entities.MaxiContext())
            {
                var retval = request;

                //check if database data is more recent than client cache
                var lastUpdate = SQLService.LastTablesUpdate(db, "Country", "Zona", "Location", "Zip", "Provincia");
                if (request.Fch == null || lastUpdate > request.Fch)
                {
                    //check if database data is more recent than Api cache
                    var emp = (int)EmpDTO.Ids.MatiasMasso;
                    var apiCache = Api.Models.AppState.Cache(emp);
                    if (apiCache.Catalog.Fch == null || lastUpdate > apiCache.Catalog.Fch)
                    {
                        apiCache.Catalog.Fch = DateTime.Now;
                        apiCache.Catalog.Brands = BrandsService.All(db, emp);
                        apiCache.Catalog.Depts = DeptsService.All(db,emp);
                        apiCache.Catalog.Categories = CategoriesService.All(db,emp);
                        apiCache.Catalog.Skus = SkusService.All(db, emp);
                        apiCache.Catalog.RetailPrices = PriceListCustomerService.RetailPrices(db, emp);
                    }
                    retval = apiCache.Catalog;
                }
                return retval;
            }

        }
        public static List<CatalogDTO.Brand> Brands(int empId)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from tpa in db.Tpas
                              join nom in db.VwProductTexts on tpa.Guid equals nom.Guid
                              where tpa.Emp == empId
                              select new CatalogDTO.Brand()
                              {
                                  Guid = tpa.Guid,
                                  Obsoleto = tpa.Obsoleto,
                                  Nom = new LangTextDTO()
                                  {
                                      Esp = nom.NomEsp,
                                      Cat = nom.NomCat,
                                      Eng = nom.NomEng,
                                      Por = nom.NomPor
                                  }
                              }).ToList();
                return retval;
            }
        }
        public static List<CatalogDTO.Dept> Depts(int empId)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from dept in db.Depts
                              join nom in db.VwProductTexts on dept.Guid equals nom.Guid
                              join tpa in db.Tpas on dept.Brand equals tpa.Guid
                              where tpa.Emp == empId
                              select new CatalogDTO.Dept()
                              {
                                  Guid = dept.Guid,
                                  Brand = dept.Brand,
                                  Nom = new LangTextDTO()
                                  {
                                      Esp = nom.NomEsp,
                                      Cat = nom.NomCat,
                                      Eng = nom.NomEng,
                                      Por = nom.NomPor
                                  }
                              }).ToList();
                return retval;
            }
        }
        public static List<CatalogDTO.Category> Categories(int empId)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from stp in db.Stps
                              join tpa in db.Tpas on stp.Brand equals tpa.Guid
                              where tpa.Emp == empId
                              join nom in db.VwProductTexts on stp.Guid equals nom.Guid 
                              join dept in db.VwDeptCategories on stp.Guid equals dept.Category into query
                              from m in query.DefaultIfEmpty()
                              select new CatalogDTO.Category()
                              {
                                  Guid = stp.Guid,
                                  Brand = stp.Brand,
                                  Dept = m.Dept,
                                  NoStk = stp.NoStk,
                                  Obsoleto = stp.Obsoleto,
                                  Nom = new LangTextDTO()
                                  {
                                      Esp = nom.NomEsp,
                                      Cat = nom.NomCat,
                                      Eng = nom.NomEng,
                                      Por = nom.NomPor
                                  }
                              }).ToList();
                return retval;
            }
        }
        public static List<CatalogDTO.Sku> Skus(int empId)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = (from entity in db.VwSkuNoms
                              where entity.Emp == empId
                              select new CatalogDTO.Sku()
                              {
                                  Guid = entity.SkuGuid,
                                  Category = entity.CategoryGuid,
                                  IsBundle = entity.IsBundle ?? false,
                                  NoStk = entity.SkuNoStk,
                                  Obsoleto = entity.Obsoleto != 0,
                                  Nom = new LangTextDTO()
                                  {
                                      Esp = entity.SkuNomEsp,
                                      Cat = entity.SkuNomCat,
                                      Eng = entity.SkuNomEng,
                                      Por = entity.SkuNomPor
                                  },
                                  NomLlarg = new LangTextDTO()
                                  {
                                      Esp = entity.SkuNomLlargEsp,
                                      Cat = entity.SkuNomLlargCat,
                                      Eng = entity.SkuNomLlargEng,
                                      Por = entity.SkuNomLlargPor
                                  }
                              }).ToList();
                return retval;
            }
        }
    }
}

