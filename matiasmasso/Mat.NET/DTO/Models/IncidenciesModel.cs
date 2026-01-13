using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Models
{
    public class IncidenciesModel
    {
        public HashSet<DTOGuidNom.Compact> Customers { get; set; }
        public HashSet<DTOGuidNom.Compact> CodisApertura { get; set; }
        public HashSet<DTOGuidNom.Compact> CodisTancament { get; set; }
        public CatalogModel Catalog { get; set; }
        public HashSet<Item> Items { get; set; }

        public IncidenciesModel()
        {
            Customers = new HashSet<DTOGuidNom.Compact>();
            CodisApertura = new HashSet<DTOGuidNom.Compact>();
            CodisTancament = new HashSet<DTOGuidNom.Compact>();
            Catalog = new CatalogModel();
            Items = new HashSet<Item>();
        }

        public string CustomerNom(Item item)
        {
            DTOGuidNom.Compact customer = Customers.FirstOrDefault(x => x.Guid.Equals(item.CustomerGuid));
            string retval = (customer == null) ? "" : customer.Nom;
            return retval;

        }
        public class Item
        {
            public Guid Guid { get; set; }
            public int Num { get; set; }
            public string Asin { get; set; }
            public DateTime Fch { get; set; }
            public DateTime FchClose { get; set; }
            public DTOIncidencia.Srcs Src { get; set; }
            public Guid CustomerGuid { get; set; }
            public Guid ProductGuid { get; set; }
            public Guid CodApertura { get; set; }
            public Guid CodTancament { get; set; }
            public string SerialNumber { get; set; }
            public string ManufactureDate { get; set; }
            public bool HasTicket { get; set; }
            public bool HasImg { get; set; }
            public bool HasVideo { get; set; }
            public string Obs { get; set; }
            public string UserNom { get; set; }
            public DateTime FchLastEdited { get; set; }


            public bool Matches(string searchterm)
            {
                if (string.IsNullOrEmpty(searchterm))
                    return true;
                else
                    return (Asin.ContainsIgnoreCase(searchterm) || Num.ToString().Contains(searchterm));
            }
        }

        public class Request
        {
            public Guid UserGuid { get; set; }
            public Guid CustomerGuid { get; set; }
            public Guid ProductGuid { get; set; }
            public int Year { get; set; }
            public bool OnlyOpen { get; set; }
        }
    }
}
