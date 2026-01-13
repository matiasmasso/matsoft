//using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Miravia
{
    public class PackRequest
    {
        [JsonProperty("pack_order_list")]
        public List<PackOrderClass> PackOrderList { get; set; } 

        [JsonProperty("delivery_type")]
        public string DeliveryType { get; set; } = "dropship";

        public PackRequest(List<Order.Item> items)
        {
            PackOrderList = new List<PackOrderClass>();
            var packOrder = new PackOrderClass();
            packOrder.OrderId = items.Select(x => x.OrderId).FirstOrDefault();
            packOrder.OrderItemList = items.Select(x => x.OrderItemId!).ToList();
            PackOrderList.Add(packOrder);
        }
    }

    public class PackOrderClass
    {
        [JsonProperty("order_item_list")]
        public List<string> OrderItemList { get; set; } = new();

        [JsonProperty("order_id")]
        public string? OrderId { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class PackResponse
    {
        [JsonProperty("result")]
        public Result Result { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        public AwbRequest AwbRequest()
        {
            var retval = new AwbRequest();
            retval.DocType = "PDF";
            retval.Packages = Result.Data.PackOrderList
                .SelectMany(x => x.OrderItemList)
                .Select(x => new AwbRequest.Package { PackageId = x.PackageId })
                .ToList();
            return retval;
        }
    }

    public class Result
    {
        [JsonProperty("error_msg")]
        public string? ErrorMsg { get; set; }

        [JsonProperty("data")]
        public ResponseData? Data { get; set; }

        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("error_code")]
        public string? ErrorCode { get; set; }
    }

    public class ResponseData
    {
        [JsonProperty("pack_order_list")]
        public List<PackOrderList> PackOrderList { get; set; }
    }

    public class OrderItemList
    {
        [JsonProperty("order_item_id")]
        public string OrderItemId { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("item_err_code")]
        public string ItemErrCode { get; set; }

        [JsonProperty("tracking_number")]
        public string TrackingNumber { get; set; }

        [JsonProperty("shipment_provider")]
        public string ShipmentProvider { get; set; }

        [JsonProperty("package_id")]
        public string PackageId { get; set; }

        [JsonProperty("retry")]
        public string Retry { get; set; }
    }

    public class PackOrderList
    {
        [JsonProperty("order_item_list")]
        public List<OrderItemList> OrderItemList { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }
    }

    public class AwbRequest
    {
        [JsonProperty("doc_type")]
        public string DocType { get; set; }

        [JsonProperty("packages")]
        public List<Package> Packages { get; set; } = new();

        public class Package
        {
            [JsonProperty("package_id")]
            public string PackageId { get; set; }
        }
    }

    public class AwbResponse
    {
        [JsonProperty("result")]
        public ResultClass Result { get; set; }
        [JsonProperty("code")]
        public string? Code { get; set; }
        [JsonProperty("request_id")]
        public string? RequestId { get; set; }

        public class ResultClass
        {
            [JsonProperty("error_msg")]
            public string? ErrorMsg { get; set; } // "package not found"
            [JsonProperty("data")]
            public DataClass? Data { get; set; }
            [JsonProperty("success")]
            public bool Success { get; set; }
            [JsonProperty("error_code")]
            public string? ErrorCode { get; set; } //error_code": "123"
        }

        public class DataClass
        {
            [JsonProperty("file")]
            public string? File { get; set; } // "file": "PGlmcmF",
            [JsonProperty("zpl_url")]
            public string? ZplUrl { get; set; } //"zpl_url": "http://www.test.com/xxx.zpl",
            [JsonProperty("pdf_url")]
            public string? PdfUrl { get; set; } //"pdf_url": "http://www.test.com/xxx.pdf",
            [JsonProperty("doc_type")]
            public string? DocType { get; set; }

        }
    }







}
