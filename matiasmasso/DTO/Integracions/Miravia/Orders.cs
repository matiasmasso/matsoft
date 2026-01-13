using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Miravia
{
    // OrdersResponse myDeserializedClass = JsonConvert.DeserializeObject<OrdersResponse>(myJsonResponse);
    public class OrdersResponse
    {
        [JsonPropertyName("data")]
        [JsonProperty("data")]
        public Data? Data { get; set; }

        [JsonPropertyName("code")]
        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonPropertyName("request_id")]
        [JsonProperty("request_id")]
        public string? RequestId { get; set; }
    }


    public class OrderItemsResponse
    {
        [JsonPropertyName("data")]
        [JsonProperty("data")]
        public List<Order.Item> Data { get; set; }

        [JsonPropertyName("code")]
        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonPropertyName("request_id")]
        [JsonProperty("request_id")]
        public string? RequestId { get; set; }
    }

    //public class PickUpStoreInfo
    //{
    //}

    public class AddressBilling
    {
        [JsonPropertyName("country")]
        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonPropertyName("address3")]
        [JsonProperty("address3")]
        public string? Address3 { get; set; }

        [JsonPropertyName("phone")]
        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("address2")]
        [JsonProperty("address2")]
        public string? Address2 { get; set; }

        [JsonPropertyName("city")]
        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonPropertyName("address1")]
        [JsonProperty("address1")]
        public string? Address1 { get; set; }

        [JsonPropertyName("post_code")]
        [JsonProperty("post_code")]
        public string? PostCode { get; set; }

        [JsonPropertyName("phone2")]
        [JsonProperty("phone2")]
        public string? Phone2 { get; set; }

        [JsonPropertyName("last_name")]
        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("address5")]
        [JsonProperty("address5")]
        public string? Address5 { get; set; }

        [JsonPropertyName("address4")]
        [JsonProperty("address4")]
        public string? Address4 { get; set; }

        [JsonPropertyName("first_name")]
        [JsonProperty("first_name")]
        public string? FirstName { get; set; }
    }

    public class AddressShipping
    {
        [JsonPropertyName("country")]
        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonPropertyName("address3")]
        [JsonProperty("address3")]
        public string? Address3 { get; set; }

        [JsonPropertyName("phone")]
        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("address2")]
        [JsonProperty("address2")]
        public string? Address2 { get; set; }

        [JsonPropertyName("city")]
        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonPropertyName("address1")]
        [JsonProperty("address1")]
        public string? Address1 { get; set; }

        [JsonPropertyName("post_code")]
        [JsonProperty("post_code")]
        public string? PostCode { get; set; }

        [JsonPropertyName("phone2")]
        [JsonProperty("phone2")]
        public string? Phone2 { get; set; }

        [JsonPropertyName("last_name")]
        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("address5")]
        [JsonProperty("address5")]
        public string? Address5 { get; set; }

        [JsonPropertyName("address4")]
        [JsonProperty("address4")]
        public string? Address4 { get; set; }

        [JsonPropertyName("first_name")]
        [JsonProperty("first_name")]
        public string? FirstName { get; set; }

        public string NomAndLocation() => string.Format("{0} ({1})", FullNom(), City);

        public string FullNom() => (FirstName + ' ' + LastName).Trim();
    }

    public class Data
    {
        [JsonPropertyName("count")]
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonPropertyName("orders")]
        [JsonProperty("orders")]
        public List<Order> Orders { get; set; }

        [JsonPropertyName("countTotal")]
        [JsonProperty("countTotal")]
        public int CountTotal { get; set; }
    }

    public class Order : BaseGuid, IModel
    {
        [JsonPropertyName("voucher")]
        [JsonProperty("voucher")]
        public string? Voucher { get; set; }

        [JsonPropertyName("warehouse_code")]
        [JsonProperty("warehouse_code")]
        public string? WarehouseCode { get; set; }

        [JsonPropertyName("order_number")]
        [JsonProperty("order_number")]
        public string? OrderNumber { get; set; }

        [JsonPropertyName("voucher_seller")]
        [JsonProperty("voucher_seller")]
        public string? VoucherSeller { get; set; }

        [JsonPropertyName("created_at")]
        [JsonProperty("created_at")]
        public string? CreatedAt { get; set; }

        [JsonPropertyName("voucher_code")]
        [JsonProperty("voucher_code")]
        public string? VoucherCode { get; set; }

        [JsonPropertyName("gift_option")]
        [JsonProperty("gift_option")]
        public bool GiftOption { get; set; }

        [JsonPropertyName("customer_last_name")]
        [JsonProperty("customer_last_name")]
        public string? CustomerLastName { get; set; }

        [JsonPropertyName("promised_shipping_times")]
        [JsonProperty("promised_shipping_times")]
        public string? PromisedShippingTimes { get; set; }

        [JsonPropertyName("updated_at")]
        [JsonProperty("updated_at")]
        public string? UpdatedAt { get; set; }

        [JsonPropertyName("price")]
        [JsonProperty("price")]
        public string? Price { get; set; }

        [JsonPropertyName("national_registration_number")]
        [JsonProperty("national_registration_number")]
        public string? NationalRegistrationNumber { get; set; }

        [JsonPropertyName("shipping_fee_original")]
        [JsonProperty("shipping_fee_original")]
        public string? ShippingFeeOriginal { get; set; }

        [JsonPropertyName("payment_method")]
        [JsonProperty("payment_method")]
        public string? PaymentMethod { get; set; }

        [JsonPropertyName("customer_first_name")]
        [JsonProperty("customer_first_name")]
        public string? CustomerFirstName { get; set; }

        [JsonPropertyName("shipping_fee_discount_seller")]
        [JsonProperty("shipping_fee_discount_seller")]
        public string? ShippingFeeDiscountSeller { get; set; }

        [JsonPropertyName("shipping_fee")]
        [JsonProperty("shipping_fee")]
        public string? ShippingFee { get; set; }

        [JsonPropertyName("items_count")]
        [JsonProperty("items_count")]
        public int ItemsCount { get; set; }

        [JsonPropertyName("delivery_info")]
        [JsonProperty("delivery_info")]
        public string? DeliveryInfo { get; set; }

        [JsonPropertyName("statuses")]
        [JsonProperty("statuses")]
        public List<string>? Statuses { get; set; }

        [JsonPropertyName("address_billing")]
        [JsonProperty("address_billing")]
        public AddressBilling? AddressBilling { get; set; }

        [JsonPropertyName("extra_attributes")]
        [JsonProperty("extra_attributes")]
        public string? ExtraAttributes { get; set; }

        [JsonPropertyName("order_id")]
        [JsonProperty("order_id")]
        public string? OrderId { get; set; }

        [JsonPropertyName("remarks")]
        [JsonProperty("remarks")]
        public string? Remarks { get; set; }

        [JsonPropertyName("gift_message")]
        [JsonProperty("gift_message")]
        public string? GiftMessage { get; set; }

        [JsonPropertyName("address_shipping")]
        [JsonProperty("address_shipping")]
        public AddressShipping? AddressShipping { get; set; }

        public Guid ZipGuid { get; set; }
        public Guid? Operator { get; set; } //operadora que registra la comanda

        public ConsumerTicketModel? Ticket { get; set; }
        public List<Order.Item> Items { get; set; } = new();

        public List<DocfileModel> ShipmentLabels { get; set; } = new();


        public Order() : base() { }
        public Order(Guid guid) : base(guid) { }

        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);

                var searchTargets = new List<string?> {
                    AddressShipping?.FirstName,
                    AddressShipping?.LastName,
                    AddressShipping?.Address3,
                    AddressShipping?.Address4,
                    AddressShipping?.Address5,
                    AddressShipping?.City
                };

                var searchTarget = string.Join(" ", searchTargets.Where(x => !string.IsNullOrEmpty(x)));
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public string PropertyPageUrl()
        {
            throw new NotImplementedException();
        }

        public class Item
        {
            //[JsonProperty("pick_up_store_info")]
            //public PickUpStoreInfo? PickUpStoreInfo { get; set; }

            [JsonPropertyName("tax_amount")]
            [JsonProperty("tax_amount")]
            public double? TaxAmount { get; set; }

            [JsonPropertyName("reason")]
            [JsonProperty("reason")]
            public string? Reason { get; set; }

            [JsonPropertyName("sla_time_stamp")]
            [JsonProperty("sla_time_stamp")]
            public DateTime? SlaTimeStamp { get; set; }

            [JsonPropertyName("voucher_seller")]
            [JsonProperty("voucher_seller")]
            public double? VoucherSeller { get; set; }

            [JsonPropertyName("purchase_order_id")]
            [JsonProperty("purchase_order_id")]
            public string? PurchaseOrderId { get; set; }

            [JsonPropertyName("voucher_code_seller")]
            [JsonProperty("voucher_code_seller")]
            public string? VoucherCodeSeller { get; set; }

            [JsonPropertyName("voucher_code")]
            [JsonProperty("voucher_code")]
            public string? VoucherCode { get; set; }

            [JsonPropertyName("package_id")]
            [JsonProperty("package_id")]
            public string? PackageId { get; set; }

            [JsonPropertyName("buyer_id")]
            [JsonProperty("buyer_id")]
            public string? BuyerId { get; set; }

            [JsonPropertyName("variation")]
            [JsonProperty("variation")]
            public string? Variation { get; set; }

            [JsonPropertyName("product_id")]
            [JsonProperty("product_id")]
            public string? ProductId { get; set; }

            [JsonPropertyName("purchase_order_number")]
            [JsonProperty("purchase_order_number")]
            public string? PurchaseOrderNumber { get; set; }

            [JsonPropertyName("sku")]
            [JsonProperty("sku")]
            public string? Sku { get; set; }

            [JsonPropertyName("order_type")]
            [JsonProperty("order_type")]
            public string? OrderType { get; set; }

            [JsonPropertyName("invoice_number")]
            [JsonProperty("invoice_number")]
            public string? InvoiceNumber { get; set; }

            [JsonPropertyName("cancel_return_initiator")]
            [JsonProperty("cancel_return_initiator")]
            public string? CancelReturnInitiator { get; set; }

            [JsonPropertyName("shop_sku")]
            [JsonProperty("shop_sku")]
            public string? ShopSku { get; set; }

            [JsonPropertyName("is_reroute")]
            [JsonProperty("is_reroute")]
            public int? IsReroute { get; set; }

            [JsonPropertyName("stage_pay_status")]
            [JsonProperty("stage_pay_status")]
            public string? StagePayStatus { get; set; }

            [JsonPropertyName("sku_id")]
            [JsonProperty("sku_id")]
            public string? SkuId { get; set; }

            [JsonPropertyName("tracking_code_pre")]
            [JsonProperty("tracking_code_pre")]
            public string? TrackingCodePre { get; set; }

            [JsonPropertyName("order_item_id")]
            [JsonProperty("order_item_id")]
            public string? OrderItemId { get; set; }

            [JsonPropertyName("shop_id")]
            [JsonProperty("shop_id")]
            public string? ShopId { get; set; }

            [JsonPropertyName("order_flag")]
            [JsonProperty("order_flag")]
            public string? OrderFlag { get; set; }

            [JsonPropertyName("is_fbl")]
            [JsonProperty("is_fbl")]
            public int? IsFbl { get; set; }

            [JsonPropertyName("name")]
            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonPropertyName("delivery_option_sof")]
            [JsonProperty("delivery_option_sof")]
            public int? DeliveryOptionSof { get; set; }

            [JsonPropertyName("order_id")]
            [JsonProperty("order_id")]
            public string? OrderId { get; set; }

            [JsonPropertyName("status")]
            [JsonProperty("status")]
            public string? Status { get; set; }

            [JsonPropertyName("product_main_image")]
            [JsonProperty("product_main_image")]
            public string? ProductMainImage { get; set; }

            [JsonPropertyName("paid_price")]
            [JsonProperty("paid_price")]
            public double? PaidPrice { get; set; }

            [JsonPropertyName("product_detail_url")]
            [JsonProperty("product_detail_url")]
            public string? ProductDetailUrl { get; set; }

            [JsonPropertyName("warehouse_code")]
            [JsonProperty("warehouse_code")]
            public string? WarehouseCode { get; set; }

            [JsonPropertyName("promised_shipping_time")]
            [JsonProperty("promised_shipping_time")]
            public string? PromisedShippingTime { get; set; }

            [JsonPropertyName("shipping_type")]
            [JsonProperty("shipping_type")]
            public string? ShippingType { get; set; }

            [JsonPropertyName("created_at")]
            [JsonProperty("created_at")]
            public string? CreatedAt { get; set; }

            [JsonPropertyName("voucher_seller_lpi")]
            [JsonProperty("voucher_seller_lpi")]
            public int? VoucherSellerLpi { get; set; }

            [JsonPropertyName("wallet_credits")]
            [JsonProperty("wallet_credits")]
            public string? WalletCredits { get; set; }

            [JsonPropertyName("updated_at")]
            [JsonProperty("updated_at")]
            public string? UpdatedAt { get; set; }

            [JsonPropertyName("currency")]
            [JsonProperty("currency")]
            public string? Currency { get; set; }

            [JsonPropertyName("shipping_provider_type")]
            [JsonProperty("shipping_provider_type")]
            public string? ShippingProviderType { get; set; }

            [JsonPropertyName("shipping_fee_original")]
            [JsonProperty("shipping_fee_original")]
            public double? ShippingFeeOriginal { get; set; }

            [JsonPropertyName("item_price")]
            [JsonProperty("item_price")]
            public double? ItemPrice { get; set; }

            [JsonPropertyName("is_digital")]
            [JsonProperty("is_digital")]
            public int? IsDigital { get; set; }

            [JsonPropertyName("shipping_service_cost")]
            [JsonProperty("shipping_service_cost")]
            public int? ShippingServiceCost { get; set; }

            [JsonPropertyName("tracking_code")]
            [JsonProperty("tracking_code")]
            public string? TrackingCode { get; set; }

            [JsonPropertyName("shipping_fee_discount_seller")]
            [JsonProperty("shipping_fee_discount_seller")]
            public string? ShippingFeeDiscountSeller { get; set; }

            [JsonPropertyName("shipping_amount")]
            [JsonProperty("shipping_amount")]
            public double? ShippingAmount { get; set; }

            [JsonPropertyName("reason_detail")]
            [JsonProperty("reason_detail")]
            public string? ReasonDetail { get; set; }

            [JsonPropertyName("return_status")]
            [JsonProperty("return_status")]
            public string? ReturnStatus { get; set; }

            [JsonPropertyName("shipment_provider")]
            [JsonProperty("shipment_provider")]
            public string? ShipmentProvider { get; set; }

            [JsonPropertyName("voucher_amount")]
            [JsonProperty("voucher_amount")]
            public double? VoucherAmount { get; set; }

            [JsonPropertyName("digital_delivery_info")]
            [JsonProperty("digital_delivery_info")]
            public string? DigitalDeliveryInfo { get; set; }

            [JsonPropertyName("extra_attributes")]
            [JsonProperty("extra_attributes")]
            public string? ExtraAttributes { get; set; }

            public Guid SkuGuid { get; set; }
            public Guid PncGuid { get; set; } // to keep track of order line on delivery item
        }

    }

}


