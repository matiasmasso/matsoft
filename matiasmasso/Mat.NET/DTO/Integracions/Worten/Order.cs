using System;
using System.Collections.Generic;

namespace DTO.Integracions.Worten
{
    public class OrderList
    {
        public List<OrderClass> orders { get; set; }
    }
    public class OrderClass
    {
        public string order_id { get; set; }
        public DateTime created_date { get; set; }
        public CustomerClass customer { get; set; }
        public List<Line> order_lines { get; set; }
        public decimal total_commission { get; set; }
        public decimal total_price { get; set; }

        public class Line
        {
            public string offer_id { get; set; }
            public string offer_sku { get; set; }

            public Guid order_line_id { get; set; }
            public int order_line_index { get; set; }
            public string order_line_state { get; set; }
            public string order_line_state_reason_code { get; set; }
            public string order_line_state_reason_label { get; set; }
            public decimal price { get; set; }
            public string price_additional_info { get; set; }
            public decimal price_unit { get; set; }

            public Guid product_sku { get; set; }
            public string product_title { get; set; }
            public int quantity { get; set; }
        }

        public class CustomerClass
        {
            public Address billing_address { get; set; }
            public Address shipping_address { get; set; }
            public string civility { get; set; }
            public Guid customer_id { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string locale { get; set; }

            public string Fullname()
            {
                return string.Format("{0} {1}", firstname, lastname).Trim();
            }
        }

        public class Acceptance
        {
            public List<Line> order_lines { get; set; }

            public Acceptance()
            {
                order_lines = new List<Line>();
            }

            public void AcceptLine(OrderClass.Line item)
            {
                Line line = new Line();
                line.id = item.order_line_id.ToString();
                line.accepted = true;
                order_lines.Add(line);
            }

            public class Line
            {
                public bool accepted { get; set; }
                public string id { get; set; }
            }
        }

    }
}
