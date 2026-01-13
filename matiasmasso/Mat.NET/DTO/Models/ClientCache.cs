using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Models
{
    public class ClientCache
    {
        public DTOEmp Emp { get; set; }
        public List<LastUpdate> LastUpdates { get; set; }
        public List<DTOProductBrand> Brands { get; set; }
        public List<DTODept> Depts { get; set; }
        public List<DTOProductCategory> Categories { get; set; }
        public List<DTODeptCategory> DeptCategories { get; set; }
        public List<DTOProductSpare> ProductSpares { get; set; }
        public List<DTOProductSku> Skus { get; set; }
        public List<DTOProductSku> SkuBundles { get; set; }
        public List<Models.SkuStock> Stocks { get; set; }
        public Dictionary<Guid, string> RetailPrices { get; set; }
        public Dictionary<Guid, int> SkuPrevisions { get; set; }
        public List<Dictionary<string, Object>> ProductUrls { get; set; }
        public List<DTOProductPlugin> ProductPlugins { get; set; }

        public bool IsLoading { get; set; }

        public event EventHandler AfterUpdate;

        public void FinishedLoading()
        {
            IsLoading = false;
            if (AfterUpdate != null)
                AfterUpdate(this, new EventArgs());
        }

        public List<DTOProductBrand> GetBrands(bool includeObsolets = false)
        {
            List<DTOProductBrand> retval = Brands;
            if (!includeObsolets)
                retval.RemoveAll(x => x.obsoleto);
            return retval;
        }


        public DTOProduct FindProduct(Guid guid)
        {
            DTOProduct retval = null; 
            retval = Brands.FirstOrDefault(x => x.Guid.Equals(guid));
            if (retval == null)
                retval = Categories.FirstOrDefault(x => x.Guid.Equals(guid));
            if (retval == null)
                retval = Skus.FirstOrDefault(x => x.Guid.Equals(guid));
            return retval;
        }

        public DTOProductSku FindSku(Guid guid)
        {
            return Skus.FirstOrDefault(x => x.Guid.Equals(guid));
        }

        public DTOProductSku FindSku(DTOEan ean)
        {
            return Skus.FirstOrDefault(x => x.Ean13 != null && x.Ean13.Value == ean.Value);
        }

        public DTOProductSku FindSku(int id)
        {
            return Skus.FirstOrDefault(x => x.Id == id);
        }

        public DTOProductSku FindSkuByRefPrv(string refPrv)
        {
            return Skus.FirstOrDefault(x => x.RefProveidor == refPrv);
        }

        public List<DTOProductSku> SkuSearch(string searchTerm)
        {
            return Skus.Where(x => x.Matches(searchTerm)).OrderBy(a => a.Nom.Esp).OrderBy(b => b.Category.Nom.Esp).OrderBy(c => c.Category.Brand.Nom.Esp).OrderBy(d => d.obsoleto).ToList();
        }

        public Models.SkuStock SkuStock(Guid guid)
        {
            SkuStock retval = Stocks.FirstOrDefault(x => x.Guid.Equals(guid));
            if (retval == null)
                retval = new SkuStock();
            return retval;
        }
        public Decimal RetailPrice(Guid guid)
        {
            Decimal retval = 0;
            string tmp = "";
            if (RetailPrices.TryGetValue(guid, out tmp))
                retval = Decimal.Parse(tmp, System.Globalization.CultureInfo.InvariantCulture);
            return retval;
        }

        public int SkuPrevisio(Guid guid)
        {
            int retval = 0;
            int tmp = 0;
            if (SkuPrevisions.TryGetValue(guid, out tmp))
                retval = tmp;
            return retval;
        }

        //creates an empty cache with exisiting LastUpdates list
        //to be posted to the Api
        //so it may include updated tables in the response if any
        public ServerCache Request()
        {
            ServerCache retval = Models.ServerCache.Factory(Emp);
            retval.LastUpdates = LastUpdates;
            return retval;
        }

        public void UpdateTable(LastUpdate serverTable)
        {
            LastUpdate clientTable = LastUpdates.FirstOrDefault(x => x.Table == serverTable.Table);
            if (clientTable != null)
                clientTable.Fch = serverTable.Fch;
        }


        public static List<LastUpdate> LastUpdatesFactory()
        {
            List<LastUpdate> retval = new List<LastUpdate>();
            foreach (ServerCache.Tables table in Enum.GetValues(typeof(ServerCache.Tables)))
                retval.Add(new LastUpdate(table));
            return retval;
        }

        public DateTime LastCatalogUpdate()
        {
            DateTime retval = DateTime.MinValue;
            foreach (LastUpdate item in LastUpdates.OrderByDescending(x => x.Fch).ToList())
            {
                switch (item.Table)
                {
                    case ServerCache.Tables.Stocks:
                    case ServerCache.Tables.Skus:
                    case ServerCache.Tables.Categories:
                    case ServerCache.Tables.Brands:
                        retval = item.Fch;
                        break;
                }
                if (retval > DateTime.MinValue)
                    break;
            }
            return retval;
        }

        public List<string> CanonicalUrls(DTOProductBrand oBrand)
        {
            List<string> retval = new List<string>();
            //ProductUrls.
            return retval;
        }

        public DTOProduct ProductOrSelf(Guid guid)
        {
            DTOProduct retval = null;
            DTOProductBrand oBrand = Brands.FirstOrDefault(x => x.Guid.Equals(guid));
            if (oBrand == null)
            {
                DTOProductCategory oCategory = Categories.FirstOrDefault(x => x.Guid.Equals(guid));
                if (oCategory == null)
                {
                    DTOProductSku oSku = Skus.FirstOrDefault(x => x.Guid.Equals(guid));
                    if (oSku == null)
                        retval = new DTOProduct(guid);
                    else
                        retval = oSku;
                }
                else
                    retval = oCategory;
            }
            else
                retval = oBrand;

            return retval;
        }

    }
}
