using System;
using System.Collections.Generic;

namespace DTO.Models.Wtbol
{
    public class Model
    {
        public Site Site { get; set; }
        public Models.CatalogModel Catalog { get; set; }

        public Model()
        {
            Site = new Site();
            Site.LandingPages = new List<LandingPage>();
            Site.Stocks = new List<Stock>();

            Catalog = new Models.CatalogModel();
        }
    }
    public class Site
    {
        public Guid Guid { get; set; }
        public string MerchantId { get; set; }
        public Base.GuidNom Customer { get; set; }
        public string Website { get; set; }
        public string ContactNom { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTel { get; set; }
        public DateTime LastStkFch { get; set; }
        public Guid UsrGuid { get; set; }

        public List<LandingPage> LandingPages { get; set; }
        public List<Stock> Stocks { get; set; }

        public Site()
        {
        }

        public LandingPage AddLandingPage(Guid productGuid, string url)
        {
            LandingPage retval = new LandingPage();
            retval.ProductGuid = productGuid;
            retval.Url = url;
            LandingPages.Add(retval);
            return retval;
        }
        public Stock AddStock(Guid productGuid, int qty)
        {
            Stock retval = new Stock();
            retval.ProductGuid = productGuid;
            retval.Qty = qty;
            Stocks.Add(retval);
            return retval;
        }
    }

    public class LandingPage
    {
        public Guid ProductGuid { get; set; }
        public string Url { get; set; }
    }
    public class Stock
    {
        public Guid ProductGuid { get; set; }
        public int Qty { get; set; }
    }
}
