using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Integracions.Worten
{
    public class ShopOffer
    {
        public List<Offer> offers { get; set; }
    }

    public class Offer
    {
        public bool active { get; set; }
        //public List<Price> all_prices { get; set; }
        //public bool allow_quote_requests { get; set; }
        //public Price applicable_pricing { get; set; }
        public string category_code { get; set; }
        public string category_label { get; set; }
        public List<string> channels { get; set; }
        public string currency_iso_code { get; set; }
        //public string description { get; set; }
        //public Discount discount { get; set; }
        //public Fulfillment fulfillment { get; set; }
        //public List<string> inactivity_reasons { get; set; }
        //public int leadtime_to_ship { get; set; }
        //public CodedLabel logistic_class { get; set; }
        //public decimal min_shipping_price { get; set; }
        //public decimal min_shipping_price_additional { get; set; }
        //public string min_shipping_type { get; set; }
        //public string min_shipping_zone { get; set; }
        //public List<string> offer_additional_fields { get; set; }
        public string offer_id { get; set; }
        public decimal price { get; set; }
        //public string price_additional_info { get; set; }
        public List<Product_Reference> product_references { get; set; }
        public Guid product_sku { get; set; }
        public string product_title { get; set; }
        public int quantity { get; set; }
        //public DateTime shipping_deadline { get; set; }
        public string shop_sku { get; set; }
        public int state_code { get; set; }
        public decimal total_price { get; set; }
        public string update_delete { get; set; }

        public DTOProductSku Sku { get; set; }

        public string Ean()
        {
            string retval = "";
            Product_Reference value = product_references.FirstOrDefault(x => x.reference_type == "EAN");
            if (value != null)
                retval = value.reference;
            return retval;
        }

        public Product_Reference AddProductReference(string type, string value)
        {
            Product_Reference retval = new Product_Reference();
            retval.reference_type = type;
            retval.reference = value;
            product_references.Add(retval);
            return retval;
        }
    }

    public class Price
    {
        public string channel_code { get; set; }
        //public DateTime discount_end_date { get; set; } //format incorrecte
        //public DateTime discount_start_date { get; set; } //format incorrecte
        public decimal price { get; set; }
        public decimal unit_discount_price { get; set; }
        public decimal unit_origin_price { get; set; }
        public List<Volume_Price> volume_prices { get; set; }

    }
    public class Volume_Price
    {
        public decimal price { get; set; }
        public int quantity_threshold { get; set; }
        public decimal unit_discount_price { get; set; }
        public decimal unit_origin_price { get; set; }

    }

    public class Fulfillment
    {
        public Center center { get; set; }
    }

    public class Center
    {
        public string code { get; set; }
    }

    public class CodedLabel
    {
        public string code { get; set; }
        public string label { get; set; }
    }

    public class Product_Reference
    {
        public string reference { get; set; }
        public string reference_type { get; set; }
    }

    public class Discount
    {
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public decimal price { get; set; }
        public List<DiscountRange> ranges { get; set; }
    }

    public class DiscountRange
    {
        public decimal price { get; set; }
        public int quantity_threshold { get; set; }
    }

    public class ImportResult
    {
        public string import_id { get; set; }

    }
}
