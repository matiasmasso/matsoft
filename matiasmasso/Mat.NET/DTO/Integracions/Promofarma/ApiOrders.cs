using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Integracions.Promofarma
{

    public class OrderList : Response
    {
        public decimal total { get; set; }
        public int results { get; set; }
        public int page { get; set; }
        public List<Order> orders { get; set; }


        public static string EndPoint(DateTime from, DateTime to) => string.Format("orders/");


        public class Request
        {
            public DateTime From { get; set; }
            public DateTime To { get; set; }
        }
    }

    public class Order : Response
    {
        public Data data { get; set; }
        public Metadata metadata { get; set; }
        public static string endPoint(string order_id) => string.Format("orders/{0}", order_id);

        public static DTOConsumerTicket ConsumerTicketFactory(DTOUser user)
        {
            DTOConsumerTicket retval = new DTOConsumerTicket();
            retval.Fch = DTO.GlobalVariables.Now();
            retval.MarketPlace = DTOMarketPlace.Wellknown(DTOMarketPlace.Wellknowns.Promofarma);
            retval.UsrLog = DTOUsrLog.Factory(user);
            retval.PurchaseOrder = DTOPurchaseOrder.Factory(retval);
            return retval;
        }
    }


    public class OrderLines : Response
    {
        public List<OrderLine> data { get; set; }

        public string EndPoint(int orderId) => string.Format("orders/{0}/lines");
    }


    public class OrderLine
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
        public string status { get; set; } //ie: "NEW"
        public decimal commission { get; set; } //ie: 20,
        public int quantity { get; set; } //ie: 1
        public List<string> ean { get; set; } //ie: [],
        public List<string> national_code { get; set; } //ie: 162967,
        public decimal price { get; set; } //ie: 4.56,
        public string currency { get; set; } //ie: "EUR"
        public decimal tax { get; set; } //ie: 21
        public bool next_day { get; set; } //ie: true,
    }

    public class Data
    {
        public string order_id { get; set; }
        public DateTime order_date { get; set; }
        public int number_of_order_lines { get; set; }
        public decimal total_amount { get; set; }
        public string currency { get; set; }
        public string courier { get; set; }
        public string customer_name { get; set; }
        public string customer_country { get; set; }
    }

    public class PickupRequest : Response
    {
        public Metadata Metadata { get; set; }
        public static string EndPoint(string order_id) => string.Format("shipment/{0}/request-pickup", order_id);
    }
    public class ShippingLabel : Response
    {
        public string Data { get; set; } //base-64 encoded pdf:
        public static string EndPoint(string order_id) => string.Format("shipment/{0}/label", order_id);
    }
    public class ShippingWaybill : Response
    {
        public string Data { get; set; } //base-64 encoded pdf:
        public static string EndPoint(string order_id) => string.Format("shipment/{0}/waybill", order_id);
    }


    public class Response
    {
        public string Result { get; set; } //ie: OK, KO
        public string Code { get; set; } //ie: invalid_argument_exception
        public string Message { get; set; } //ie: Invalid argument: page not valid <not_integer_page>

        public ErrorCode Error { get; set; }

        public class ErrorCode
        {
            public int Code { get; set; } //ie: 401
            public string Message { get; set; } //ie: Access unauthorized
        }

    }

    public class Metadata
    {
        public List<Link> Links { get; set; }
    }

    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }
    }

}
