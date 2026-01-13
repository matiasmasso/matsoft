using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO.CatalogDTO;

namespace DTO
{
    public class ProBasketDTO
    {
        public List<DestinationClass> Destinations { get; set; } = new();

        public DestinationClass? Destination { get; set; }

        public List<CustomerPortfolioModel> CustomerPortfolio { get; set; } = new();// CliTpa
        public List<ProductChannelModel> ChannelProducts { get; set; } = new();// ProductChannel





        public class DestinationClass
        {
            public Guid Guid { get; set; }
            public string? Nom { get; set; }
            public string? Adr { get; set; }
            public string? Location { get; set; }
        }

        public List<ProductBrandModel> Brands(CacheDTO cache)
        {
            var skus = cache.Skus.Where(x => CustomerPortfolio.Any(y => x.Guid == y.Product)).ToList();
            var categories = cache.Categories.Where(x => CustomerPortfolio.Any(y => x.Guid == y.Product)).ToList();
            var skuCategories = cache.Categories.Where(x => skus.Any(y => y.Category == x.Guid)).ToList();
            categories.AddRange(skuCategories);
            var brands = cache.Brands.Where(x => CustomerPortfolio.Any(y => x.Guid == y.Product)).ToList();
            var categoryBrands = cache.Brands.Where(x => categories.Any(y => x.Guid == y.Brand)).ToList();
            var retval = new List<ProductBrandModel>();
            retval.AddRange(brands);
            retval.AddRange(categoryBrands);
            return retval;
        }
    }
}
