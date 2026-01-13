using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOContactClass : DTOBaseGuid
    {
        public DTOLangText Nom { get; set; }
        public int Ord { get; set; }
        public bool SalePoint { get; set; }
        public bool Raffles { get; set; }
        public DTODistributionChannel DistributionChannel { get; set; }
        public List<DTOContact> Contacts { get; set; }

        public enum Wellknowns
        {
            notSet,
            botigaPuericultura,
            farmacia,
            paraFarmacia,
            majoristaFarmacies,
            guarderia,
            majoristaGuarderies,
            online,
            staff,
            rep,
            proveidor
        }

        public DTOContactClass() : base()
        {
            Contacts = new List<DTOContact>();
            Nom = new DTOLangText();
        }

        public DTOContactClass(Guid oGuid) : base(oGuid)
        {
            Contacts = new List<DTOContact>();
            Nom = new DTOLangText();
        }

        public static DTOContactClass Wellknown(DTOContactClass.Wellknowns value)
        {
            DTOContactClass retval = null;
            switch (value)
            {
                case DTOContactClass.Wellknowns.botigaPuericultura:
                    {
                        retval = new DTOContactClass(new Guid("2C19ABF8-F424-45DF-8690-09F32778A8DB"));
                        break;
                    }

                case DTOContactClass.Wellknowns.farmacia:
                    {
                        retval = new DTOContactClass(new Guid("61244B59-9E4D-4019-A358-D2932A3E370F"));
                        break;
                    }

                case DTOContactClass.Wellknowns.paraFarmacia:
                    {
                        retval = new DTOContactClass(new Guid("D0E8740A-3B6F-40D1-85B2-CB68C8497AE3"));
                        break;
                    }

                case DTOContactClass.Wellknowns.majoristaFarmacies:
                    {
                        retval = new DTOContactClass(new Guid("58597ABE-2D6C-4964-BADE-073F7A993E47"));
                        break;
                    }

                case DTOContactClass.Wellknowns.guarderia:
                    {
                        retval = new DTOContactClass(new Guid("231CEB14-7036-4BF8-B01F-D3F824A33C86"));
                        break;
                    }

                case DTOContactClass.Wellknowns.majoristaGuarderies:
                    {
                        retval = new DTOContactClass(new Guid("924D3A0E-B7F9-4F96-BA09-665630C76DEF"));
                        break;
                    }

                case DTOContactClass.Wellknowns.online:
                    {
                        retval = new DTOContactClass(new Guid("E4796515-F5EE-490A-AAAE-7D15714CDD08"));
                        break;
                    }

                case DTOContactClass.Wellknowns.staff:
                    {
                        retval = new DTOContactClass(new Guid("7E93ACDE-D6B1-4F4D-AD50-92CB5B151FB9"));
                        break;
                    }

                case DTOContactClass.Wellknowns.rep:
                    {
                        retval = new DTOContactClass(new Guid("BEA91554-9481-44FE-9C70-EA36844981A6"));
                        break;
                    }

                case DTOContactClass.Wellknowns.proveidor:
                    {
                        retval = new DTOContactClass(new Guid("D8D627FE-2ED8-4E85-823A-CFAF3A9BD2E7"));
                        break;
                    }

                case DTOContactClass.Wellknowns.notSet:
                    {
                        retval = null;
                        break;
                    }
            }
            return retval;
        }

        public class Collection : List<DTOContactClass>
        {
            public static Collection Factory(IEnumerable<DTOContactClass> contactclasses, List<Guid> filterGuids = null)
            {
                Collection retval = new Collection();
                if (filterGuids == null)
                    retval.AddRange(contactclasses);
                else
                {
                    List<DTOContactClass> filteredClasses = contactclasses.Where(x => filterGuids.Any(y => x.Guid.Equals(y))).ToList();
                    retval.AddRange(filteredClasses);
                }
                return retval;
            }
            public List<DTODistributionChannel> Channels()
            {
                List<DTODistributionChannel> retval = this.
                    Where(a => a.DistributionChannel != null).
                    GroupBy(b => b.DistributionChannel.Guid).
                    Select(c => c.First().DistributionChannel).
                    ToList();
                if (this.Any(x => x.DistributionChannel == null))
                {
                    DTODistributionChannel noChannel = DTODistributionChannel.Wellknown(DTODistributionChannel.Wellknowns.noChannel);
                    noChannel.ContactClasses.AddRange(this.Where(x => x.DistributionChannel == null));
                    retval.Add(noChannel);
                }
                return retval;
            }
        }
    }
}
