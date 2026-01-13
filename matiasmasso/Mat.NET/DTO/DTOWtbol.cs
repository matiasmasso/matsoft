using DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web.Mvc.Razor;

namespace DTO
{

}
public class DTOWtbolSite : DTOBaseGuid
{
    public string Nom { get; set; }
    public string Web { get; set; }
    public DTOCustomer Customer { get; set; }
    public string MerchantId { get; set; }
    public string ContactNom { get; set; }
    public string ContactEmail { get; set; }
    public string ContactTel { get; set; }
    public DateTime FchLastStocks { get; set; }
    public bool Active { get; set; }
    public string Obs { get; set; }
    [JsonIgnore]
    public Byte[] Logo { get; set; }
    public DTOUsrLog UsrLog { get; set; }

    public List<int> LandingPageStatusCount { get; set; }
    public List<DTOWtbolBasket> Baskets { get; set; }

    public List<DTOWtbolLandingPage> LandingPages { get; set; }
    public List<DTOWtbolStock> Stocks { get; set; }

    public const string HATCH_FEED_URL_TEMPLATE = "https://www.matiasmasso.es/britax/stocks/{0}";

    public enum Wellknowns
    {
        none,
        glider,
        algatec
    }

    public DTOWtbolSite() : base()
    {
        this.LandingPageStatusCount = new int[] { 0, 0, 0 }.ToList();
        this.Baskets = new List<DTOWtbolBasket>();
    }

    public DTOWtbolSite(Guid oGuid) : base(oGuid)
    {
        this.LandingPageStatusCount = new int[] { 0, 0, 0 }.ToList();
        this.Baskets = new List<DTOWtbolBasket>();
    }

    public static DTOWtbolSite Wellknown(DTOWtbolSite.Wellknowns value)
    {
        DTOWtbolSite retval = null;
        switch (value)
        {
            case DTOWtbolSite.Wellknowns.glider:
                {
                    retval = new DTOWtbolSite(new Guid("91C7DF8B-1410-413F-8E1F-AFC2A271FBCA"));
                    break;
                }

            case DTOWtbolSite.Wellknowns.algatec:
                {
                    retval = new DTOWtbolSite(new Guid("0596C238-3962-4334-A7C7-F0E926BF9C34"));
                    break;
                }
        }
        return retval;
    }

    public static DTOWtbolSite Factory(DTOCustomer oCustomer = null/* TODO Change to default(_) if this is not a reference type */)
    {
        DTOWtbolSite retval = new DTOWtbolSite();
        {
            var withBlock = retval;
            withBlock.Customer = oCustomer;
        }
        return retval;
    }

    public string Url()
    {
        string retval = "";
        if (!string.IsNullOrEmpty(this.Web))
            retval = this.Web.StartsWith("http") ? this.Web : string.Format("https://{0}", this.Web);
        return retval;
    }

    public string HatchFeedUrl()
    {
        string retval = "";
        if (this.MerchantId != "")
            retval = string.Format(HATCH_FEED_URL_TEMPLATE, this.MerchantId);
        return retval;
    }

    public void RestoreObjects()
    {
        if (LandingPages != null)
        {
            foreach (var oLandingPage in LandingPages)
            {
                oLandingPage.RestoreObjects();
                oLandingPage.Site = this;
            }
        }
    }

    public bool HasLandingPage(DTOProduct product, string url)
    {
        bool retval = this.LandingPages.Any(x => ((DTOProduct)x.Product).Guid.Equals(product.Guid) && x.Uri.ToString() == url);
        return retval;
    }

    //public string SerializedWithNoLandingPages()
    //{
    //    var serializer = new 
    //        JavaScriptSerializer();
    //    DTOWtbolSite trimmed = this;


    //    trimmed.LandingPages = null;
    //    string retval = serializer.Serialize(trimmed);
    //    return retval;
    //}

    public static string StockText(DTOWtbolSite oWtbolSite)
    {
        var oSkuLandingPages = oWtbolSite.LandingPages.Where(x => x.Product is DTOProductSku);
        int iProducts = oSkuLandingPages.Where(y => ((DTOProductSku)y.Product).Stock > 0).Count();
        decimal DcEur = oSkuLandingPages.Where(y => ((DTOProductSku)y.Product).Rrpp != null).Sum(x => ((DTOProductSku)x.Product).Stock * ((DTOProductSku)x.Product).Rrpp.Eur);
        string retval = string.Format("{0} productes per {1}€", iProducts, DcEur);
        return retval;
    }

    public int LandingPagesCount()
    {
        int retval = 0;
        if (this.LandingPages != null)
        {
            retval = this.LandingPages.Count;
        }
        return retval;
    }

    public DateTime? LastLandingPagesUpload()
    {
        DateTime? retval = null;
        //.LandingPages.Max(Function(x) x.FchCreated)'
        if (this.LandingPages.Count > 0)
        {
            retval = this.LandingPages.Max(x => x.FchCreated);
        }
        return retval;
    }

    public static MatHelper.Excel.Sheet ExcelLandingPages(DTOBasicCatalog oCatalog, DTOWtbolSite oSite, DTOLang lang, List<Exception> exs)
    {
        MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Landing pages", "MATIAS MASSO SA - Wtbol");
        {
            var withBlock = retval;
            withBlock.AddColumn("Guid");
            withBlock.AddColumn(lang.Tradueix("Nivel", "Nivell", "Level"));
            withBlock.AddColumn(lang.Tradueix("Marca", "Marca", "Brand"));
            withBlock.AddColumn(lang.Tradueix("Categoria", "Categoria", "Category"));
            withBlock.AddColumn(lang.Tradueix("Producto", "Producte", "Product"));
            withBlock.AddColumn("Landing page");
        }

        MatHelper.Excel.Row oRow = retval.AddRow();
        oRow.AddCell(oSite.Guid.ToString());
        oRow.AddCell(lang.Tradueix("Comercio"));
        oRow.AddCell(oSite.Nom);

        foreach (DTOBasicCatalog.Brand brand in oCatalog)
        {
            oRow = retval.AddRow();
            oRow.AddCell(brand.Guid.ToString());
            oRow.AddCell(lang.Tradueix("Marca", "Marca", "Brand"));
            oRow.AddCell(brand.Nom, MmoUrl.Factory(true, "product", brand.Guid.ToString()));
            oRow.AddCell();
            oRow.AddCell();
            oRow.AddCell();

            DTOWtbolLandingPage oLandingPage = oSite.LandingPages.FirstOrDefault(x => ((DTOProduct)x.Product).Guid.Equals(brand.Guid));
            if (oLandingPage != null)
                oRow.AddCell(oLandingPage.Uri.AbsoluteUri);

            foreach (DTOBasicCatalog.Category category in brand.Categories)
            {
                oLandingPage = oSite.LandingPages.FirstOrDefault(x => ((DTOProduct)x.Product).Guid.Equals(category.Guid));
                oRow = retval.AddRow();
                oRow.AddCell(category.Guid.ToString());
                oRow.AddCell(lang.Tradueix("Categoria", "Categoria", "Category"));
                oRow.AddCell(brand.Nom);
                oRow.AddCell(category.Nom, MmoUrl.Factory(true, "product", category.Guid.ToString()));
                oRow.AddCell();

                if (oLandingPage != null)
                    oRow.AddCell(oLandingPage.Uri.AbsoluteUri);

                foreach (DTOBasicCatalog.Sku sku in category.Skus)
                {
                    oRow = retval.AddRow();
                    oRow.AddCell(sku.Guid.ToString());
                    oRow.AddCell(lang.Tradueix("Producto", "Producte", "Product"));
                    oRow.AddCell(brand.Nom);
                    oRow.AddCell(category.Nom);
                    oRow.AddCell(sku.Nom, MmoUrl.Factory(true, "product", sku.Guid.ToString()));

                    oLandingPage = oSite.LandingPages.FirstOrDefault(x => ((DTOProduct)x.Product).Guid.Equals(sku.Guid));
                    if (oLandingPage != null)
                        oRow.AddCell(oLandingPage.Uri.AbsoluteUri);

                }

            }

        }
        return retval;
    }
}

public class DTOWtbolLandingPage : DTOBaseGuid
{
    public DTOWtbolSite Site { get; set; }
    public Object Product { get; set; }
    public int Stock { get; set; }
    public int MgzStock { get; set; }
    public DTOAmt RRPP { get; set; }
    public Uri Uri { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime FchCreated { get; set; }
    public DateTime FchStatus { get; set; }
    public DTOUser UsrCreated { get; set; }
    public DTOUser UsrStatus { get; set; }


    public enum StatusEnum
    {
        Pending,
        Approved,
        Denied
    }

    public DTOWtbolLandingPage() : base()
    {
    }

    public DTOWtbolLandingPage(Guid oGuid) : base(oGuid)
    {
    }

    public static DTOWtbolLandingPage Factory(DTOWtbolSite oSite, DTOUser oUser)
    {
        DTOWtbolLandingPage retval = new DTOWtbolLandingPage();
        {
            var withBlock = retval;
            withBlock.Site = oSite;
            withBlock.FchCreated = DTO.GlobalVariables.Now();
        }
        return retval;
    }

    public static DTOWtbolLandingPage Factory(DTOWtbolLandingPage.LandingPageModel model)
    {
        DTOWtbolLandingPage retval = new DTOWtbolLandingPage(model.Guid);
        retval.Site = new DTOWtbolSite(model.SiteGuid);
        retval.Product = model.Product;
        retval.Status = model.Status;
        retval.FchCreated = DateTime.Parse(model.FchCreated);
        retval.UsrCreated = model.UsrCreated;
        retval.FchStatus = DateTime.Parse(model.FchStatus);
        retval.UsrStatus = model.UsrStatus;
        if (!string.IsNullOrEmpty(model.Url))
        {
            if (!model.Url.StartsWith("http"))
                model.Url = "https://" + model.Url;
            retval.Uri = new Uri(model.Url);
        }
        return retval;
    }

    public string Segment()
    {
        string retval = "";
        if (Uri != null)
            retval = Uri.LocalPath;
        return retval;
    }


    public void RestoreObjects()
    {
        Product = DTOProduct.fromJObject((JObject)this.Product);
    }

    public LandingPageModel model()
    {
        LandingPageModel retval = new LandingPageModel();
        retval.Guid = this.Guid;
        retval.SiteGuid = this.Site.Guid;
        retval.CustomerGuid = this.Site.Customer.Guid;
        retval.Url = (this.Uri == null) ? this.Site.Web : this.Uri.ToString();
        retval.Product = ((DTOProduct)this.Product);
        retval.Status = this.Status;
        retval.FchCreated = this.FchCreated.ToString("s");
        retval.UsrCreated = this.UsrCreated;
        retval.FchStatus = this.FchStatus.ToString("s");
        retval.UsrStatus = this.UsrStatus;
        return retval;
    }
    public class LandingPageModel
    {
        public Guid Guid { get; set; }
        public Guid SiteGuid { get; set; }
        public Guid CustomerGuid { get; set; }
        public DTOProduct Product { get; set; }
        public string Url { get; set; }
        public string FchCreated { get; set; }
        public string FchStatus { get; set; }
        public DTOUser UsrCreated { get; set; }
        public DTOUser UsrStatus { get; set; }
        public StatusEnum Status { get; set; }
        public LandingPageModel() : base() { }


    }


}

public class DTOWtbolStock
{
    public DTOWtbolSite Site { get; set; }
    public DTOProductSku Sku { get; set; }
    public int Stock { get; set; }

    public DTOAmt Price { get; set; }
    public Uri Uri { get; set; }
    public DateTime FchCreated { get; set; }
}

public class DTOWtbolInputStocks
{
    public DTOWtbolSite Site { get; set; }
    public List<Item> Items { get; set; }

    public class Item
    {
        public string Sku { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}

public class DTOWtbolInputLandingPages
{
    public DTOWtbolSite Site { get; set; }
    public List<Item> Items { get; set; }

    public DTOWtbolInputLandingPages(DTOWtbolSite oSite)
    {
        Site = oSite;
        Items = new List<Item>();
    }
    public void AddItem(string ean, string url)
    {
        if (Items.Any(x => x.Sku == ean))
        {
        }
        else
        {
            var item = new Item();
            item.Sku = ean;
            item.Url = url;
            Items.Add(item);
        }
    }


    public class Item
    {
        public string Sku { get; set; }
        public string Url { get; set; }
    }
}

public class DTOWtbolCtr : DTOBaseGuid
{
    public DTOWtbolSite Site { get; set; }
    public object Product { get; set; }
    public DateTime Fch { get; set; }
    public string Ip { get; set; }

    public DTOWtbolCtr() : base()
    {
    }

    public DTOWtbolCtr(Guid oGuid) : base(oGuid)
    {
    }

    public void RestoreObjects()
    {
        Product = DTOProduct.fromJObject((JObject)Product);
    }
}

public class DTOWtbolBasket : DTOBaseGuid
{
    public DTOWtbolSite Site { get; set; }
    public DateTime Fch { get; set; }
    public List<Item> Items { get; set; }

    public DTOWtbolBasket() : base()
    {
    }

    public DTOWtbolBasket(Guid oGuid) : base(oGuid)
    {
    }

    public static DTOWtbolBasket Factory(DTOWtbolSite oSite)
    {
        DTOWtbolBasket retval = new DTOWtbolBasket();
        {
            var withBlock = retval;
            withBlock.Site = oSite;
            withBlock.Fch = DTO.GlobalVariables.Now();
            withBlock.Items = new List<DTOWtbolBasket.Item>();
        }
        return retval;
    }

    public DTOAmt Amt()
    {
        decimal eur = Items.Sum(x => x.Amt().Eur);
        return DTOAmt.Factory(eur);
    }
    public Item AddItem(DTOProductSku oSku, int qty, decimal Price)
    {
        Item retval = new Item();
        {
            var withBlock = retval;
            withBlock.Sku = oSku;
            withBlock.Qty = qty;
            withBlock.Price = Price;
        }
        Items.Add(retval);
        return retval;
    }

    public class Item
    {
        public int Qty { get; set; }
        public DTOProductSku Sku { get; set; }
        public decimal Price { get; set; }
        public DTOAmt Amt()
        {
            decimal retval = Qty * Price;
            return DTOAmt.Factory(retval);
        }
    }
}
