namespace DTO.Integracions.Worten
{
    public class Address
    {
        public string city { get; set; }
        public string civility { get; set; }
        public string company { get; set; }
        public string country { get; set; }
        public string country_iso_code { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string phone_secondary { get; set; }
        public string state { get; set; }
        public string street_1 { get; set; }
        public string street_2 { get; set; }
        public string zip_code { get; set; }

        public string Fullname()
        {
            return string.Format("{0} {1}", firstname, lastname).Trim();
        }

        public string FullnameAndLocation()
        {
            string location = city;
            if (country != "ES")
                location = string.Format("{0},{1}", city, country);
            string retval = string.Format("{0} ({1})", Fullname(), location);
            return retval;
        }

    }
}
