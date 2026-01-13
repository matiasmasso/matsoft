using System;
using System.Collections.Generic;

namespace DTO.Models.Min
{
    public class SkuBundle : Minifiable
    {
        public enum Cods
        {
            Guid,
            Skus
        }

        public static Dictionary<string, Object> Factory(DTOProductSku bundle)
        {
            List<Dictionary<string, Object>> items = new List<Dictionary<string, Object>>();
            foreach (DTOSkuBundle bundleSku in bundle.BundleSkus)
                items.Add(Item.Factory(bundleSku));

            SkuBundle retval = new SkuBundle();
            retval.Add(Cods.Guid, bundle.Guid.ToString());
            retval.Add(Cods.Skus, items);
            return retval;
        }

        public void Add(Cods cod, Object value)
        {
            base.Add(((int)cod).ToString(), value);
        }

        public static DTOProductSku Expand(Object jObject)
        {
            DTOProductSku retval = new DTOProductSku();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            Dictionary<string, Object> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);

            foreach (KeyValuePair<string, Object> x in baseObj)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.Guid)
                    retval.Guid = new Guid(x.Value.ToString());
                else if (cod == Cods.Skus)
                    retval.BundleSkus = Item.Expand((Object)x.Value);
            }
            return retval;
        }

        public class Item : Minifiable
        {
            public enum Cods
            {
                Sku,
                Qty,
                Rrpp
            }

            public static Dictionary<string, Object> Factory(DTOSkuBundle item)
            {
                Item retval = new Item();
                retval.Add(Cods.Sku, item.Sku.Guid.ToString());
                retval.Add(Cods.Qty, item.Qty.ToString());
                retval.Add(Cods.Rrpp, item.Rrpp.ToString("F", System.Globalization.CultureInfo.InvariantCulture));
                return retval;
            }

            public void Add(Cods cod, Object value)
            {
                base.Add(((int)cod).ToString(), value);
            }

            public static List<DTOSkuBundle> Expand(Object jObject)
            {
                List<DTOSkuBundle> retval = new List<DTOSkuBundle>();

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
                List<Dictionary<string, Object>> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, Object>>>(json);

                foreach (Dictionary<string, Object> item in baseObj)
                {
                    DTOSkuBundle skuBundle = new DTOSkuBundle();
                    foreach (KeyValuePair<string, Object> x in item)
                    {
                        Cods cod = (Cods)x.Key.toInteger();
                        if (cod == Cods.Sku)
                            skuBundle.Sku = new DTOProductSku(new Guid(x.Value.ToString()));
                        else if (cod == Cods.Qty)
                            skuBundle.Qty = ParseInt(x);
                        else if (cod == Cods.Rrpp)
                            skuBundle.Rrpp = ParseDecimal(x);
                    }
                    retval.Add(skuBundle);
                }
                return retval;
            }
        }
    }
}
