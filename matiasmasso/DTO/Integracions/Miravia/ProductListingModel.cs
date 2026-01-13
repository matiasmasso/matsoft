using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO.Integracions.Miravia
{
    public class ProductListingModel
    {
        public List<Item> Items { get; set; } = new();

        public async Task<string> ToCsv()
        {
            string retval = "";
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms, Encoding.UTF8))
                {
                    var rows = new List<string>()
                    {
                    "Category,Category Name,Product Name,Product Images1,Product Images2,Product Images3,Product Images4,Product Images5,Product Images6,Product Images7,Product Images8,Brand,Long Description,Hazmat,Variation Name1,Option for Variation1,Image per Variation,Variation Name2,Option for Variation2,ean code,package weight,price,special price,package length,package width,package height,Stock,delivery by seller,Attribute Name 1, Attribute value 1,Attribute Name 2, Attribute value 2,Attribute Name 3, Attribute value 3,Attribute Name 4, Attribute value 4,Attribute Name 5, Attribute value 5,Attribute Name 6, Attribute value 6,Attribute Name 7, Attribute value 7,Attribute Name 8, Attribute value 8,Attribute Name 9, Attribute value 9,Attribute Name 10, Attribute value 10,SellerSku,ParentSku            ,Pickup in Store,Delivery option standard,Delivery option express,",
                    "Mandatory,Optional,Mandatory,Mandatory,Optional,Optional,Optional,Optional,Optional,Optional,Optional,Mandatory,Optional,Optional,Conditional Mandatory,Conditional Mandatory,Optional,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Mandatory,Mandatory,optional,Mandatory,Mandatory,Mandatory,Mandatory,Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Conditional Mandatory,Mandatory,Mandatory,Optional,Optional,Optional,",
                    "Indicate the appropriate category ID for each product. An accurate category ID would boost search results.,Category Name,Product name should include product brand and model. Avoid irrelevant keywords as it may cause the listing to be banned.,The URL of your main product image. The image cannot be duplicated with other listings in your shop,The URL of your main product image. The image cannot be duplicated with other listings in your shop,The URL of your main product image. The image cannot be duplicated with other listings in your shop,The URL of your main product image. The image cannot be duplicated with other listings in your shop,The URL of your main product image. The image cannot be duplicated with other listings in your shop,The URL of your main product image. The image cannot be duplicated with other listings in your shop,The URL of your main product image. The image cannot be duplicated with other listings in your shop,The URL of your main product image. The image cannot be duplicated with other listings in your shop,The brand of your product,A good product description enhances the quality of your listing and increases chances of sales.,Please fill in accurately. Inaccurate DG may result in additional shipping fee or failed delivery. DG contains battery/magnet/liquid/flammable materials.default value is None DG If not fill.,Add variants to a product that has more than one option. You can select from the provided variant types or create your own.,Add variants to a product that has more than one option. You can select from the provided variant types or create your own.,Add variants to a product that has more than one option. You can select from the provided variant types or create your own.,Add variants to a product that has more than one option. You can select from the provided variant types or create your own.,Add variants to a product that has more than one option. You can select from the provided variant types or create your own.,EAN code can't duplicated in 1 store. It means each EAN code can only be associated with 1 product in this store. And it only can be posted if the old EAN is deleted.,Please ensure you have entered the right package weight (kg) and dimensions (cm) to avoid incorrect shipping fee charges,Input your product price in your local currency.,Please ensure you have entered the right package weight (kg) and dimensions (cm) to avoid incorrect shipping fee charges,Please ensure you have entered the right package weight (kg) and dimensions (cm) to avoid incorrect shipping fee charges,Please ensure you have entered the right package weight (kg) and dimensions (cm) to avoid incorrect shipping fee charges,Please ensure you have entered the right package weight (kg) and dimensions (cm) to avoid incorrect shipping fee charges,Input your product stock,if select Yes here represented this item delivery by Seller. if select No here represented this item delivery by Arise.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,Attribute element contains name key and value pairs describing the product attributes. Limit of 10.,SKU is a unique identifier for each product variation. SKU value cannot be duplicated in a store.,The parent sku is the original or main form of the product and child products are the subsequent products of the parent product that have slight variations. It is a non-buyable product used to relate child products. Item level product information will be read from parent sku and not from child sku,Pickup in Store' only support for sellers who get the whitelist premission from Miravia. If you want to use 'Pickup in Store' please ask your person in charge to comfirm further informations. Otherwise may result in failure of your create/update product process.,When you select Delivery by Miraiva as your delivery option. Delivery option standard will be input 'Yes'  as default value. Parameter is invaild when you select Delivery by seller as your delivery option.,When you select Delivery by Miraiva as your delivery option. Delivery option standard will be input 'No'  by default. Parameter is invaild when you select Delivery by seller as your delivery option.,",
                    "*please choose from the drop-down box  or input the correct category id.,,*Please input 5 to 255 characters for product name.,*This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,*Choose from the ASC or input directly,Please input 20 to 3000 characters for product description. Leave it blank during Bulk Edit will be considered as no change.,Choose from the drop-down box,,,,,,ean code,*Please input 1 to 300,*Only positive numbers are accepted.,,*Please input 1 to 300,*Please input 1 to 300,*Please input 1 to 300,*Only positive numbers are accepted.,Please select Yes or No.,,,,,,,,,,,,,,,,,,,,,Please input less than 200 characters. Leave it blank during Bulk Edit will be considered as no change.,Please input less than 200 characters,Please input Yes or No. Empty value will be regarded as No or skip.,Please input Yes or No. Empty value will be regarded as Yes or skip.,Please input Yes or No. Empty value will be regarded as No or skip.,d at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,This is the main image of your product page. Multiple images can be uploaded at once. Maximum 8 images. Size between 330x330 and 5000x5000 px. Max file size: 5 MB. Obscene image is strictly prohibited.,*Choose from the ASC or input directly,Please input 20 to 3000 characters for product description. Leave it blank during Bulk Edit will be considered as no change.,Choose from the drop-down box,,,,,,*ean code,*Please input 1 to 300,*Only positive numbers are accepted.,,*Please input 1 to 300,*Please input 1 to 300,*Please input 1 to 300,*Only positive numbers are accepted.,Please select Yes or No.,,,,,,,,,,,,,,,,,,,,,Please input less than 200 characters. Leave it blank during Bulk Edit will be considered as no change.,Please input less than 200 characters,input 'delete' or leave empty,,",
                    };

                    foreach (var item in Items)
                    {
                        var row = item.CsvRow();
                        rows.Add(row);
                    }

                    retval = await Helper.ToCsv(rows);
                }
            }
            return retval;
        }

        public class Item
        {
            public Guid? SkuGuid { get; set; }
            public Guid? CategoryGuid { get; set; }
            public string? Category { get; set; }
            public string? Category_name { get; set; }
            public string? Product_Name { get; set; }
            public string? Product_Images1 { get; set; }
            public string? Product_Images2 { get; set; }
            public string? Product_Images3 { get; set; }
            public string? Product_Images4 { get; set; }
            public string? Product_Images5 { get; set; }
            public string? Product_Images6 { get; set; }
            public string? Product_Images7 { get; set; }
            public string? Product_Images8 { get; set; }
            public string? Brand { get; set; }
            public string? Long_Description { get; set; }

            public string? Hazmat { get; set; }
            public string? Variation_Name1 { get; set; }
            public string? Option_for_Variation1 { get; set; }
            public string? Image_per_Variation { get; set; }
            public string? Variation_Name2 { get; set; }
            public string? Option_for_Variation2 { get; set; }

            public string? Ean_code { get; set; }
            public decimal? Package_weight { get; set; }
            public decimal? Price { get; set; }
            public decimal? Special_price { get; set; }
            public int Package_length { get; set; }
            public int Package_Width { get; set; }
            public int Package_Height { get; set; }
            public int Stock { get; set; }
            public string? Delivery_by_seller { get; set; }

            public string? Attribute_Name_1 { get; set; }
            public string? Attribute_Value_1 { get; set; }
            public string? Attribute_Name_2 { get; set; }
            public string? Attribute_Value_2 { get; set; }
            public string? Attribute_Name_3 { get; set; }
            public string? Attribute_Value_3 { get; set; }
            public string? Attribute_Name_4 { get; set; }
            public string? Attribute_Value_4 { get; set; }
            public string? Attribute_Name_5 { get; set; }
            public string? Attribute_Value_5 { get; set; }
            public string? Attribute_Name_6 { get; set; }
            public string? Attribute_Value_6 { get; set; }
            public string? Attribute_Name_7 { get; set; }
            public string? Attribute_Value_7 { get; set; }
            public string? Attribute_Name_8 { get; set; }
            public string? Attribute_Value_8 { get; set; }
            public string? Attribute_Name_9 { get; set; }
            public string? Attribute_Value_9 { get; set; }
            public string? Attribute_Name_10 { get; set; }
            public string? Attribute_Value_10 { get; set; }

            public string? SellerSku { get; set; }
            public string? ParentSku { get; set; }
            public string? Pickup_in_store { get; set; }
            public string? Delivery_option_standard { get; set; }
            public string? Delivery_option_express { get; set; }


            public string CsvRow()
            {
                var fieldValues = new List<string>();

                fieldValues.Add(Helper.Text(Category));
                fieldValues.Add(Helper.Text(Category_name));
                fieldValues.Add(Helper.Text(Product_Name));
                fieldValues.Add(Helper.Text(Product_Images1));
                fieldValues.Add(Helper.Text(Product_Images2));
                fieldValues.Add(Helper.Text(Product_Images3));
                fieldValues.Add(Helper.Text(Product_Images4));
                fieldValues.Add(Helper.Text(Product_Images5));
                fieldValues.Add(Helper.Text(Product_Images6));
                fieldValues.Add(Helper.Text(Product_Images7));
                fieldValues.Add(Helper.Text(Product_Images8));
                fieldValues.Add(Helper.Text(Brand));
                fieldValues.Add(Helper.Text(Long_Description));

                fieldValues.Add(Helper.Text(Hazmat));
                fieldValues.Add(Helper.Text(Variation_Name1));
                fieldValues.Add(Helper.Text(Option_for_Variation1));
                fieldValues.Add(Helper.Text(Image_per_Variation));
                fieldValues.Add(Helper.Text(Variation_Name2));
                fieldValues.Add(Helper.Text(Option_for_Variation2));

                fieldValues.Add(Helper.Text(Ean_code));
                fieldValues.Add(Helper.SingleDecimal(Package_weight));
                fieldValues.Add(Helper.TwoDecimals(Price));
                fieldValues.Add(Helper.TwoDecimals(Special_price));
                fieldValues.Add(Helper.SingleDecimal(Package_length / 10));
                fieldValues.Add(Helper.SingleDecimal(Package_Width / 10));
                fieldValues.Add(Helper.SingleDecimal(Package_Height / 10));
                fieldValues.Add(Helper.NoDecimals(Stock));
                fieldValues.Add(Helper.Text(Delivery_by_seller));

                fieldValues.Add(Helper.Text(Attribute_Name_1));
                fieldValues.Add(Helper.Text(Attribute_Value_1));
                fieldValues.Add(Helper.Text(Attribute_Name_2));
                fieldValues.Add(Helper.Text(Attribute_Value_2));
                fieldValues.Add(Helper.Text(Attribute_Name_3));
                fieldValues.Add(Helper.Text(Attribute_Value_3));
                fieldValues.Add(Helper.Text(Attribute_Name_4));
                fieldValues.Add(Helper.Text(Attribute_Value_4));
                fieldValues.Add(Helper.Text(Attribute_Name_5));
                fieldValues.Add(Helper.Text(Attribute_Value_5));
                fieldValues.Add(Helper.Text(Attribute_Name_6));
                fieldValues.Add(Helper.Text(Attribute_Value_6));
                fieldValues.Add(Helper.Text(Attribute_Name_7));
                fieldValues.Add(Helper.Text(Attribute_Value_7));
                fieldValues.Add(Helper.Text(Attribute_Name_8));
                fieldValues.Add(Helper.Text(Attribute_Value_8));
                fieldValues.Add(Helper.Text(Attribute_Name_9));
                fieldValues.Add(Helper.Text(Attribute_Value_9));
                fieldValues.Add(Helper.Text(Attribute_Name_10));
                fieldValues.Add(Helper.Text(Attribute_Value_10));

                fieldValues.Add(Helper.Text(SellerSku));
                fieldValues.Add(Helper.Text(ParentSku));
                fieldValues.Add(Helper.Text(Pickup_in_store));
                fieldValues.Add(Helper.Text(Delivery_option_standard));
                fieldValues.Add(Helper.Text(Delivery_option_express));

                var retval = string.Join(Helper.CsvFieldSeparator, fieldValues);
                return retval;

            }

        }
    }
}
