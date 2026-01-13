namespace DTO.Integracions.Worten
{
    public class Shop
    {
        public string shop_id { get; set; } //4857
        public string shop_name { get; set; } //Tippee PT-ES
        public string shop_state { get; set; } //OPEN
        public Contact_Information contact_informations { get; set; }
        public class Contact_Information
        {
            public string city { get; set; }
            public string civility { get; set; }
            public string country { get; set; }
            public string email { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string phone { get; set; }
            public string phone_secondary { get; set; }
            public string state { get; set; }
            public string street1 { get; set; }
            public string web_site { get; set; }
            public string zip_code { get; set; }
        }

    }
}
