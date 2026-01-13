using System;

namespace DTO.Models
{
    public class SatRecallModel
    {
        public Guid Guid { get; set; }
        public IncidenciaModel Incidencia { get; set; }
        public string PickupFrom { get; set; }

        public DateTime FchCustomer { get; set; }
        public DateTime FchManufacturer { get; set; }

        public string PickupRef { get; set; }
        public string CreditNum { get; set; }
        public DateTime CreditFch { get; set; }
        public string Obs { get; set; }

        public class IncidenciaModel
        {
            public Guid Guid { get; set; }
            public int Num { get; set; }
            public string Asin { get; set; }
            public CustomerModel Customer { get; set; }
            public ProductModel Product { get; set; }

            public string SerialNumber { get; set; }
        }

        public class CustomerModel
        {
            public Guid Guid { get; set; }
            public string FullNom { get; set; }
        }

        public class ProductModel
        {
            public Guid Guid { get; set; }
            public LangText Nom { get; set; }

            public int SourceCod { get; set; }
        }

        public class LangText
        {
            public string Esp { get; set; }
        }
    }


}

