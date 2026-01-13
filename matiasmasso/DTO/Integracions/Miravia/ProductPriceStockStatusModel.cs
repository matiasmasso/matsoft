using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Miravia
{
    public class ProductPriceStockStatusModel
    {
        public List<Item> Items { get; set; } = new();
        public async Task<string> ToCsv()
        {
            var rows = new List<string>()
            {
                "SellerSku,Price,SpecialPrice,Status,Total Stock Quantity,Multiwarehouse Code 1,Stock Quantity 1,Multiwarehouse Code 2,Stock Quantity 2,Multiwarehouse Code 3,Stock Quantity 3,Multiwarehouse Code 4,Stock Quantity 4,Multiwarehouse Code 5,Stock Quantity 5",
                //"SellerSku,Price,SpecialPrice,Status,Default Stock Quantity,Multiwarehouse Code 1,Stock Quantity 1,Multiwarehouse Code 2,Stock Quantity 2,Multiwarehouse Code 3,Stock Quantity 3,Multiwarehouse Code 4,Stock Quantity 4,Multiwarehouse Code 5,Stock Quantity 5",
                "Mandatory,optional,optional,optional,optional,optional,optional,optional,optional,optional,optional,optional,optional,optional,optional",
                "SKU is a unique identifier for each product variation. SKU value cannot be duplicated in a store.,This is the price that the customer has to pay for the product. This price includes the taxes. ,Sales Price for a Product for Promotion. ,Input Status value to change Sku status,Input your product stock quantity. The stock quantity should be equal or greater than order quantity.,Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse',Input your product stock quantity. The stock quantity should be equal or greater than order quantity.,Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse',Input your product stock quantity. The stock quantity should be equal or greater than order quantity.,Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse',Input your product stock quantity. The stock quantity should be equal or greater than order quantity.,Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse',Input your product stock quantity. The stock quantity should be equal or greater than order quantity.,Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse',Input your product stock quantity. The stock quantity should be equal or greater than order quantity.",
                "Please input less than 200 characters.,Only numbers£¨¡Ý0£© and empty value are accepted. Empty value will skip this parameter.,Special Price has to be lower than 'Price' or else it's not a sale offer. Only numbers£¨¡Ý0£© and empty value are accepted. Empty value will skip this parameter.,Only 'delete' and empty value are accepted.Empty value will skip this parameter.,Only numbers£¨¡Ý0£© and empty value are accepted. Empty value will skip this parameter.,Only positive numbers are accepted. Empty value will skip this parameter.,Only numbers£¨¡Ý0£© and empty value are accepted. Empty value will skip this parameter.,Only positive numbers are accepted. Empty value will skip this parameter.,Only numbers£¨¡Ý0£© and empty value are accepted. Empty value will skip this parameter.,Only positive numbers are accepted. Empty value will skip this parameter.,Only numbers£¨¡Ý0£© and empty value are accepted. Empty value will skip this parameter.,Only positive numbers are accepted. Empty value will skip this parameter.,Only numbers£¨¡Ý0£© and empty value are accepted. Empty value will skip this parameter.,Only positive numbers are accepted. Empty value will skip this parameter.,Only numbers£¨¡Ý0£© and empty value are accepted. Empty value will skip this parameter."
            };

            foreach (var item in Items)
            {
                var row = item.CsvRow();
                rows.Add(row);
            }

            var retval = await Helper.ToCsv(rows);
            return retval;

        }



        public class Item
        {
            // SKU is a unique identifier for each product variation.
            // SKU value cannot be duplicated in a store.
            public string? SellerSku { get; set; }
            //This is the price that the customer has to pay for the product.
            //This price includes the taxes.
            public decimal? Price { get; set; }
            //Sales Price for a Product for Promotion.
            public decimal? SpecialPrice { get; set; }
            //Input Status value to change Sku status
            public string? Status { get; set; }
            //Input your product stock quantity.
            public int? DefaultStock { get; set; }
            //The stock quantity should be equal or greater than order quantity.
            public int? Quantity { get; set; }
            /// <summary>
            /// Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse'
            /// </summary>
            public string? MultiwarehouseCode1 { get; set; } // = "STACI LLICA";
            /// <summary>
            /// Input your product stock quantity. The stock quantity should be equal or greater than order quantity.
            /// </summary>
            public int? StockQuantity1 { get; set; }

            /// <summary>
            /// Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse'
            /// </summary>
            public string? MultiwarehouseCode2 { get; set; }

            /// <summary>
            /// Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse'
            /// </summary>
            public int? StockQuantity2 { get; set; }

            /// <summary>
            /// Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse'
            /// </summary>
            public string? MultiwarehouseCode3 { get; set; }

            /// <summary>
            /// Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse'
            /// </summary>
            public int? StockQuantity3 { get; set; }

            /// <summary>
            /// Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse'
            /// </summary>
            public string? MultiwarehouseCode4 { get; set; }

            /// <summary>
            /// Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse'
            /// </summary>
            public int? StockQuantity4 { get; set; }

            /// <summary>
            /// Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse'
            /// </summary>
            public string? MultiwarehouseCode5 { get; set; }

            /// <summary>
            /// Input your multiwarehouse code. You can get Multiwarehouse code from 'Miravia Seller Center-My Account-Setting-Warehouse'
            /// </summary>
            public int? StockQuantity5 { get; set; }

            public string CsvRow()
            {
                var fieldValues = new List<string>();
                fieldValues.Add(Helper.Text(SellerSku));
                fieldValues.Add(Helper.TwoDecimals(Price));
                fieldValues.Add(Helper.TwoDecimals(SpecialPrice));
                fieldValues.Add(Helper.Text(Status));
                fieldValues.Add(Helper.NoDecimals(DefaultStock));
                //fieldValues.Add(Helper.NoDecimals(Quantity));
                //fieldValues.Add(Helper.Text(MultiwarehouseCode1));
                //fieldValues.Add(Helper.NoDecimals(StockQuantity1));
                //fieldValues.Add(Helper.Text(MultiwarehouseCode2));
                //fieldValues.Add(Helper.NoDecimals(StockQuantity2));
                //fieldValues.Add(Helper.Text(MultiwarehouseCode3));
                //fieldValues.Add(Helper.NoDecimals(StockQuantity3));
                //fieldValues.Add(Helper.Text(MultiwarehouseCode4));
                //fieldValues.Add(Helper.NoDecimals(StockQuantity4));
                //fieldValues.Add(Helper.Text(MultiwarehouseCode5));
                //fieldValues.Add(Helper.NoDecimals(StockQuantity5));

                var retval = string.Join(Helper.CsvFieldSeparator, fieldValues);
                return retval.ToString();
            }
        }
    }
}
