using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOAtlas
    {
        public IEnumerable<Area> Areas { get; set; }

        public DTOAtlas()
        {
            Areas = new List<Area>();
        }

        public static string Caption(List<DTOAtlas.Area> oAreas)
        {
            string retval = "";
            switch (oAreas.Count)
            {
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        retval = oAreas.First().Nom;
                        break;
                    }

                default:
                    {
                        retval = "(diversos)";
                        break;
                    }
            }
            return retval;
        }

        public class Area
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public SourceCods SourceCod { get; set; }

            public enum SourceCods
            {
                notSet,
                country,
                zona,
                location,
                contact
            }

            public Area(Guid oGuid, string sNom, SourceCods oSourceCod) : base()
            {
                Guid = oGuid;
                Nom = sNom;
                SourceCod = oSourceCod;
            }
        }

        public class Country : Area
        {
            public List<Zona> Zonas { get; set; }

            public Country(Guid oGuid, string sNom) : base(oGuid, sNom, Area.SourceCods.country)
            {
                Zonas = new List<Zona>();
            }
        }

        public class Zona : Area
        {
            public List<Location> Locations { get; set; }

            public Zona(Guid oGuid, string sNom) : base(oGuid, sNom, Area.SourceCods.zona)
            {
                Locations = new List<Location>();
            }
        }

        public class Location : Area
        {
            public List<Contact> Contacts { get; set; } = new List<Contact>();

            public Location(Guid oGuid, string sNom) : base(oGuid, sNom, Area.SourceCods.location)
            {
                Contacts = new List<Contact>();
            }
        }

        public class Contact : Area
        {
            public Contact(Guid oGuid, string sNom) : base(oGuid, sNom, Area.SourceCods.contact)
            {
            }

            public static Contact Factory(DTOContact oContact)
            {
                var sNom = oContact.NomComercial;
                if (string.IsNullOrEmpty(sNom))
                    sNom = oContact.Nom;
                if (oContact is DTOCustomer)
                {
                    DTOCustomer oCustomer = (DTOCustomer)oContact;
                    if (oCustomer.Ref.isNotEmpty())
                        sNom = string.Format("{0} ({1})", sNom, oCustomer.Ref);
                }
                Contact retval = new Contact(oContact.Guid, sNom);
                return retval;
            }
        }
    }
}
