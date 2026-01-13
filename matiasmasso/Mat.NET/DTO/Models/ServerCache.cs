using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public class ServerCache
    {
        public DTOEmp Emp { get; set; }
        public List<LastUpdate> LastUpdates { get; set; }
        public List<Dictionary<string, Object>> Brands { get; set; }
        public List<Dictionary<string, Object>> Depts { get; set; }
        public List<Dictionary<string, Object>> Categories { get; set; }
        public List<DTODeptCategory> DeptCategories { get; set; }
        public List<Dictionary<string, Object>> Skus { get; set; }
        public List<Dictionary<string, Object>> Stocks { get; set; }
        public List<Dictionary<string, Object>> SkuBundles { get; set; }
        public Dictionary<Guid, string> RetailPrices { get; set; }
        public Dictionary<Guid, int> SkuPrevisions { get; set; }
        public List<Dictionary<string, Object>> ProductUrls { get; set; }
        public List<Dictionary<string, Object>> ProductPlugins { get; set; }
        public List<DTOProductSpare> ProductSpares { get; set; }
        public ImageCache Images { get; set; }

        public HashSet<Models.ServerCache.Tables> DirtyTables { get; set; }

        public enum Tables
        {
            Brands,
            Categories,
            Skus,
            Stocks,
            RetailPrices,
            SkuPrevisions,
            ProductUrls,
            Depts,
            SkuBundles,
            ProductPlugins,
            DeptCategories,
            ProductSpares
        }


        public ServerCache()
        {
            DirtyTables = new HashSet<Models.ServerCache.Tables>();
            LastUpdates = new List<LastUpdate>();
            Brands = new List<Dictionary<string, Object>>();
            Depts = new List<Dictionary<string, Object>>();
            Categories = new List<Dictionary<string, Object>>();
            DeptCategories = new List<DTODeptCategory>();
            ProductSpares = new List<DTOProductSpare>();
            Skus = new List<Dictionary<string, Object>>();
            SkuBundles = new List<Dictionary<string, Object>>();
            Stocks = new List<Dictionary<string, Object>>();
            RetailPrices = new Dictionary<Guid, string>();
            SkuPrevisions = new Dictionary<Guid, int>();
            ProductUrls = new List<Dictionary<string, Object>>();
            ProductPlugins = new List<Dictionary<string, Object>>();
            Images = new ImageCache();
        }

        public static ServerCache Factory(DTOEmp emp)
        {
            ServerCache retval = new ServerCache();
            retval.Emp = emp;
            retval.LastUpdates = Models.ServerCache.LastUpdatesFactory();
            return retval;
        }

        public static List<LastUpdate> LastUpdatesFactory()
        {
            List<LastUpdate> retval = new List<LastUpdate>();
            foreach (Tables table in Enum.GetValues(typeof(Tables)))
                retval.Add(new LastUpdate(table));
            return retval;
        }


    }
}
