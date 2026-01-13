using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOLeadAreas
    {
        public Country.Collection Countries { get; set; }

        public List<DTORol> Rols()
        {
            List<DTORol> retval = Countries.SelectMany(x => x.Zonas).
                SelectMany(y => y.Locations).
                SelectMany(z => z.Leads).
                GroupBy(a => a.Rol).
                Select(b => new DTORol((DTORol.Ids)b.First().Rol)).ToList();
            return retval;
        }
        public DTOContactClass.Collection ContactClasses()
        {
            DTOContactClass.Collection retval = new DTOContactClass.Collection();
            retval.AddRange(Countries.SelectMany(x => x.Zonas).
                SelectMany(y => y.Locations).
                SelectMany(z => z.Leads).
                GroupBy(a => a.ContactClass).
                Select(b => new DTOContactClass(b.First().ContactClass)));
            return retval;
        }

        public List<Lead> FilteredLeads(List<Location> locations)
        {
            List<Location> filteredLocations = Countries.SelectMany(x => x.Zonas).
                SelectMany(y => y.Locations).
                Where(z => locations.Any(a => a.Guid.Equals(z.Guid))).
                ToList();
            List<Lead> retval = filteredLocations.SelectMany(x => x.Leads).
                GroupBy(a => a.Email).
                Select(b => b.First()).
                OrderBy(c => c.Email.Substring(0, c.Email.IndexOf("@"))).
                OrderBy(c => c.Email.Substring(c.Email.IndexOf("@"))).
                ToList();
            return retval;
        }

        public List<Lead> FilteredLeads(DTOContactClass.Collection contactClasses, List<Location> locations)
        {
            List<Location> filteredLocations = Countries.SelectMany(x => x.Zonas).
                SelectMany(y => y.Locations).
                Where(z => locations.Any(a => a.Guid.Equals(z.Guid))).
                ToList();
            List<Lead> retval = filteredLocations.SelectMany(x => x.Leads).
                Where(y => contactClasses.Any(z => z.Guid.Equals(y.ContactClass))).
                GroupBy(a => a.Email).
                Select(b => b.First()).
                OrderBy(c => c.Email.Substring(0, c.Email.IndexOf("@"))).
                OrderBy(c => c.Email.Substring(c.Email.IndexOf("@"))).
                ToList();
            return retval;
        }




        public class Country
        {
            public Guid Guid { get; set; }
            public String Nom { get; set; }
            public Zona.Collection Zonas { get; set; }


            public Country()
            {
                this.Zonas = new Zona.Collection();
            }


            public List<Lead> Leads()
            {
                return this.Zonas.
                     SelectMany(b => b.Locations).
                     SelectMany(c => c.Leads).ToList();
            }

            public Country CloneWith(Zona.Collection zonas)
            {
                return new Country { Guid = this.Guid, Nom = this.Nom, Zonas = zonas };
            }


            public class Collection : List<Country>
            {
                public Collection ForRols(IEnumerable<DTORol> rols)
                {
                    Collection retval = new Collection();
                    foreach (Country country in this)
                    {
                        Zona.Collection zonas = country.Zonas.ForRols(rols);
                        if (zonas.Count > 0)
                        {
                            retval.Add(country.CloneWith(zonas));
                        }
                    }
                    return retval;
                }

                public Collection ForContactClasses(DTOContactClass.Collection contactClasses)
                {
                    Collection retval = new Collection();
                    foreach (Country country in this)
                    {
                        Zona.Collection zonas = country.Zonas.ForContactClasses(contactClasses);
                        if (zonas.Count > 0)
                        {
                            retval.Add(country.CloneWith(zonas));
                        }
                    }
                    return retval;
                }


                public List<DTOCheckedGuidNom> ToCheckedGuidNoms()
                {
                    return this.Select(c => new DTOCheckedGuidNom() { Guid = c.Guid, Nom = c.Nom, Tag = c }).ToList();
                }


            }
        }

        public class Zona
        {
            public Guid Guid { get; set; }
            public String Nom { get; set; }
            public Location.Collection Locations { get; set; }


            public Zona()
            {
                this.Locations = new Location.Collection();
            }

            public Lead.Collection Leads(List<DTORol> rols)
            {
                Lead.Collection retval = new Lead.Collection();
                retval.AddRange(this.Locations.ForRols(rols).
                    SelectMany(x => x.Leads).
                    GroupBy(y => y.Email).
                    Select(z => z.First())
                    );
                return retval;
            }

            public Zona CloneWith(Location.Collection locations)
            {
                return new Zona { Guid = this.Guid, Nom = this.Nom, Locations = locations };
            }

            public class Collection : List<Zona>
            {
                public Collection ForRols(IEnumerable<DTORol> rols)
                {
                    Collection retval = new Collection();
                    foreach (Zona zona in this)
                    {
                        Location.Collection locations = zona.Locations.ForRols(rols);
                        if (locations.Count > 0)
                        {
                            retval.Add(zona.CloneWith(locations));
                        }
                    }
                    retval.AddRange(this.Where(x => x.Locations.ForRols(rols).Count > 0));
                    return retval;
                }

                public Collection ForContactClasses(DTOContactClass.Collection contactClasses)
                {
                    Collection retval = new Collection();
                    foreach (Zona zona in this)
                    {
                        Location.Collection locations = zona.Locations.ForContactClasses(contactClasses);
                        if (locations.Count > 0)
                        {
                            retval.Add(zona.CloneWith(locations));
                        }
                    }
                    retval.AddRange(this.Where(x => x.Locations.ForContactClasses(contactClasses).Count > 0));
                    return retval;
                }

                public List<DTOCheckedGuidNom> ToCheckedGuidNoms()
                {
                    return this.Select(c => new DTOCheckedGuidNom() { Guid = c.Guid, Nom = c.Nom, Tag = c }).ToList();
                }

            }
        }

        public class Location
        {
            public Guid Guid { get; set; }
            public String Nom { get; set; }
            public Lead.Collection Leads { get; set; }
            private bool _checked { get; set; }

            public Location()
            {
                this.Leads = new Lead.Collection();
            }




            public Location CloneWith(Lead.Collection leads)
            {
                return new Location { Guid = this.Guid, Nom = this.Nom, Leads = leads };
            }

            public class Collection : List<Location>
            {
                public Collection ForRols(IEnumerable<DTORol> rols)
                {
                    Collection retval = new Collection();
                    foreach (Location location in this)
                    {
                        Lead.Collection leads = location.Leads.ForRols(rols);
                        if (leads.Count > 0)
                        {
                            retval.Add(location.CloneWith(leads));
                        }
                    }
                    return retval;
                }
                public Collection ForContactClasses(DTOContactClass.Collection contactClasses)
                {
                    Collection retval = new Collection();
                    foreach (Location location in this)
                    {
                        Lead.Collection leads = location.Leads.ForContactClasses(contactClasses);
                        if (leads.Count > 0)
                        {
                            retval.Add(location.CloneWith(leads));
                        }
                    }
                    return retval;
                }
            }
        }


        public class Lead
        {
            public Guid Guid { get; set; }
            public int Rol { get; set; }
            public Guid ContactClass { get; set; }
            public String Email { get; set; }

            public class Collection : List<Lead>
            {
                public Collection ForRols(IEnumerable<DTORol> rols)
                {
                    Collection retval = new Collection();
                    retval.AddRange(this.
                        Where(x => rols.Any(y => (int)y.id == x.Rol)).
                        GroupBy(a => a.Email).
                        Select(b => b.First()).
                        ToList());
                    return retval;
                }
                public Collection ForContactClasses(DTOContactClass.Collection contactClasses)
                {
                    Collection retval = new Collection();
                    retval.AddRange(this.
                        Where(x => contactClasses.Any(y => y.Guid.Equals(x.ContactClass))).
                        GroupBy(a => a.Email).
                        Select(b => b.First()).
                        ToList());
                    return retval;
                }
            }
        }

        public Lead.Collection Leads()
        {
            Lead.Collection retval = new Lead.Collection();
            retval.AddRange(this.Countries.
                 SelectMany(a => a.Zonas).
                 SelectMany(b => b.Locations).
                 SelectMany(c => c.Leads).
                 GroupBy(d => d.Email).
                 Select(e => e.First()));
            return retval;
        }


        public List<Country> RolsCountries(IEnumerable<DTORol> rols)
        {
            List<Lead> leads = this.Leads().Where(a => rols.Any(b => a.Rol == (int)b.id)).ToList();
            List<Country> retval = this.Countries.Where(a => a.Leads().Any(b => leads.Any(c => b.Guid.Equals(c.Guid)))).ToList();
            return retval;
        }
    }


    public class DTOLeadChecked : DTOUser
    {
        public bool Checked { get; set; }
        public DTOProductBrand Brand { get; set; }

        public DTOLeadChecked() : base()
        {

        }
        public DTOLeadChecked(Guid guid) : base(guid)
        {

        }
    }

}


