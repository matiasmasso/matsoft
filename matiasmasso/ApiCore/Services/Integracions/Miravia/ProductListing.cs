using DTO;
using DTO.Integracions.Miravia;


namespace Api.Services.Integracions.Miravia
{
    public class ProductListing
    {
        public static ProductListingModel GetValue()
        {
            var lang = LangDTO.Esp();
            ProductListingModel retval = new();
            var magatzem = DTO.MgzModel.Default();
            var marketplace = DTO.MarketPlaceModel.Wellknown(DTO.MarketPlaceModel.Wellknowns.Miravia);
            using (var db = new Entities.MaxiContext())
            {
                retval.Items = db.VwCustomerDeptSkus
                    .Where(x => x.Customer == marketplace!.Guid
                    && !x.SkuNoEcom
                    && (x.MgzGuid == null || x.MgzGuid == magatzem!.Guid))
                    .Select(x => new ProductListingModel.Item
                    {
                        SkuGuid = x.SkuGuid,
                        CategoryGuid = x.CategoryGuid,
                        Category = x.Cod,
                        Category_name = x.Nom,
                        Product_Name = x.SkuNomLlargEsp,
                        Product_Images1 = ProductSkuModel.ImageUrl(x.SkuGuid),
                        Product_Images2 = string.Empty,
                        Brand = x.BrandNomEsp,
                        Long_Description = x.ExcerptEsp,
                        Ean_code = x.Ean13,
                        Package_weight = x.SkuKg,
                        Price = x.Retail,
                        Special_price = x.Retail,
                        Package_length = (int)x.SkuDimensionL, //mides en cm sense decimals
                        Package_Width = (int)x.SkuDimensionW,
                        Package_Height = (int)x.SkuDimensionH,
                        Stock = x.Stock ?? 0,
                        Delivery_by_seller = "NO",
                        SellerSku = x.SkuId.ToString(),
                        ParentSku = "",
                        Pickup_in_store = "NO",
                        Delivery_option_standard = "",
                        Delivery_option_express = ""

                    }).ToList();

                var langTexts = db.VwLangTexts
                    .Where(x => x.Src == (int)LangTextModel.Srcs.Filter || x.Src == (int)LangTextModel.Srcs.FilterItem)
                    .ToList();
                var filters = FiltersService.GetValues(db);

                var filterTargets = db.VwFilterTargets
                    .Select(x => new KeyValuePair<Guid, Guid>(x.ParentProduct, x.FilterItem))
                    .ToList();

                foreach (var row in retval.Items)
                {
                    var attributes = filterTargets
                        .Where(x => x.Key == row.SkuGuid || x.Key == row.CategoryGuid)
                        .Select(x => x.Value)
                        .Distinct()
                        .Select(x => new Attribute
                        {
                            FilterItem = x,
                            Value = langTexts.FirstOrDefault(y => y.Guid == x)?.Esp ?? ""
                        }).ToList();

                    foreach (var attribute in attributes)
                    {
                        var filter = filters.FirstOrDefault(x => x.Items.Any(y => attribute.FilterItem == y));
                        if (filter != null)
                        {
                            attribute.Ord = filters.IndexOf(filter);
                            attribute.Name = langTexts.FirstOrDefault(x => x.Guid == filter.Guid)?.Esp ?? "";
                        }
                    }

                    var sortedAttributes = attributes.OrderBy(x=>x.Ord).ToList();

                    if (sortedAttributes.Count > 0)
                    {
                        row.Attribute_Name_1 = attributes[0].Name;
                        row.Attribute_Value_1 = attributes[0].Value;
                    }
                    if (sortedAttributes.Count > 1)
                    {
                        row.Attribute_Name_2 = attributes[1].Name;
                        row.Attribute_Value_2 = attributes[1].Value;
                    }
                    if (sortedAttributes.Count > 2)
                    {
                        row.Attribute_Name_3 = attributes[2].Name;
                        row.Attribute_Value_3 = attributes[2].Value;
                    }
                    if (sortedAttributes.Count > 3)
                    {
                        row.Attribute_Name_4 = attributes[3].Name;
                        row.Attribute_Value_4 = attributes[3].Value;
                    }
                    if (sortedAttributes.Count > 4)
                    {
                        row.Attribute_Name_5 = attributes[4].Name;
                        row.Attribute_Value_5 = attributes[4].Value;
                    }
                    if (sortedAttributes.Count > 5)
                    {
                        row.Attribute_Name_6 = attributes[5].Name;
                        row.Attribute_Value_6 = attributes[5].Value;
                    }
                    if (sortedAttributes.Count > 6)
                    {
                        row.Attribute_Name_7 = attributes[6].Name;
                        row.Attribute_Value_7 = attributes[6].Value;
                    }
                    if (sortedAttributes.Count > 7)
                    {
                        row.Attribute_Name_8 = attributes[7].Name;
                        row.Attribute_Value_8 = attributes[7].Value;
                    }
                    if (sortedAttributes.Count > 8)
                    {
                        row.Attribute_Name_9 = attributes[8].Name;
                        row.Attribute_Value_9 = attributes[8].Value;
                    }
                    if (sortedAttributes.Count > 9)
                    {
                        row.Attribute_Name_10 = attributes[9].Name;
                        row.Attribute_Value_10 = attributes[9].Value;
                    }
                }
            }
            return retval;
        }

        public class Attribute
        {
            public Guid FilterItem { get; set; }
            public int Ord { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
