using Api.Entities;
using DTO;
namespace Api.Services
{
    public class ProductService
    {
        public static ProductModel? Load(MaxiContext db, ProductModel? model)
        {
            ProductModel? retval = null;
            if(model != null)
            {
                if (model.Src == ProductModel.SourceCods.Brand)
                    retval = ProductBrandService.Load(db, (ProductBrandModel)model);
                else if (model.Src == ProductModel.SourceCods.Dept)
                    retval = ProductDeptService.Load(db, (ProductDeptModel)model);
                else if (model.Src == ProductModel.SourceCods.Category)
                    retval = ProductCategoryService.Load(db, (ProductCategoryModel)model);
                else if (model.Src == ProductModel.SourceCods.Sku) 
                    retval = ProductSkuService.Load(db, (ProductSkuModel)model);
            }
            return retval;
        }
        public static Byte[]? Thumbnail(Guid guid)
        {
            Byte[]? retval;
            using (var db = new Entities.MaxiContext())
            {
                retval = db.Arts.Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Thumbnail;
                if (retval == null)
                    retval = db.Stps.Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Image;
                if (retval == null)
                    retval = db.Depts.Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Banner;
            }
            return retval;
        }

    }
    public class ProductsService
    {
        public static List<CanonicalUrlDTO> CanonicalUrls()
        {
            using(var db = new Entities.MaxiContext())
            {
                return CanonicalUrls(db);
            }
        }

        public static List<CanonicalUrlDTO> CanonicalUrls(Entities.MaxiContext db)
        {
            List<CanonicalUrlDTO> retval = new();
            foreach (var x in db.VwProductUrlCanonicals)
            {
                var item = CanonicalUrlDTO.Factory(x.Guid
                    , x.UrlBrandEsp
                    , x.UrlBrandCat
                    , x.UrlBrandEng
                    , x.UrlBrandPor
                    , x.UrlDeptEsp
                    , x.UrlDeptCat
                    , x.UrlDeptEng
                    , x.UrlDeptPor
                    , x.UrlCategoryEsp
                    , x.UrlCategoryCat
                    , x.UrlCategoryEng
                    , x.UrlCategoryPor
                    , x.UrlSkuEsp
                    , x.UrlSkuCat
                    , x.UrlSkuEng
                    , x.UrlSkuPor
                    , x.IncludeDeptOnUrl
                    );
                retval.Add(item);
            }
            return retval;
        }

    }
}
