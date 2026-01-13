using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTORanking
    {
        public DTOUser User { get; set; }
        public List<DTORep> Reps { get; set; }
        public DTORep Rep { get; set; }
        public DTOContact Proveidor { get; set; }
        //public DateTime FchTo { get; set; } = DTO.GlobalVariables.Today();
        //public DateTime FchFrom { get; set; } = new DateTime(DTO.GlobalVariables.Today().Year, 1, 1);
        public int Year { get; set; }
        public int MonthFrom { get; set; }
        public int MonthTo { get; set; }
        public int Days { get; set; } = 90;
        public DTOProduct Product { get; set; }
        public DTOArea Area { get; set; }
        public ConceptTypes ConceptType { get; set; }
        public Formats Format { get; set; }
        public List<DTORankingItem> Items { get; set; }

        public List<DTOContact> Proveidors { get; set; }
        public DTOProductCatalog Catalog { get; set; }
        public DTODistributionChannel Channel { get; set; }

        public bool GroupCcx { get; set; } // Group stores from same owner

        public enum Formats
        {
            Amounts,
            Units
        }

        public enum ConceptTypes
        {
            Geo,
            Product
        }

        

        public static List<DTOCountry> Atlas(DTORanking oRanking)
        {
            List<DTOCountry> retval = new List<DTOCountry>();
            var oCountries = oRanking.Items.GroupBy(x => x.Location.Zona.Country.Guid).Select(y => y.First()).Select(z => z.Location.Zona.Country).ToList();
            var oZonas = oRanking.Items.GroupBy(x => x.Location.Zona.Guid).Select(y => y.First()).Select(z => z.Location.Zona).ToList();
            var oLocations = oRanking.Items.GroupBy(x => x.Location.Guid).Select(y => y.First()).Select(z => z.Location).ToList();

            foreach (var oCountry in oCountries)
            {
                retval.Add(oCountry);
                foreach (var oZona in oZonas.Where(x => x.Country.Guid.Equals(oCountry.Guid)))
                {
                    oCountry.Zonas.Add(oZona);
                    foreach (var oLocation in oLocations.Where(x => x.Zona.Guid.Equals(oZona.Guid)))
                        oZona.Locations.Add(oLocation);
                }
            }
            return retval;
        }

        public static DTORanking CustomerRanking(DTOUser oUser, DTOProduct oProduct = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTORanking retval = new DTORanking();
            {
                var withBlock = retval;
                withBlock.User = oUser;
                withBlock.ConceptType = DTORanking.ConceptTypes.Geo;
                withBlock.Product = oProduct;
            }
            return retval;
        }

        public static MatHelper.Excel.Sheet ExcelSheet(DTORanking oRanking)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet();
            foreach (DTORankingItem item in oRanking.Items)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                {
                    var withBlock = item;
                    oRow.AddCell(item.Location.Zona.Country.ISO);
                    oRow.AddCell(item.Location.Zona.Nom);
                    oRow.AddCell(item.Location.Nom);
                    oRow.AddCell(item.Customer.Nom);
                    switch (oRanking.Format)
                    {
                        case DTORanking.Formats.Amounts:
                            {
                                oRow.AddCell(item.Amt.Eur);
                                break;
                            }

                        case DTORanking.Formats.Units:
                            {
                                oRow.AddCell(item.Units);
                                break;
                            }
                    }
                }
            }
            return retval;
        }
    }

    public class DTORankingItem
    {
        public int Order { get; set; }
        public DTOCustomer Customer { get; set; }
        public DTOLocation Location { get; set; }
        public DTOProduct Product { get; set; }
        public int Units { get; set; }
        public DTOAmt Amt { get; set; }
    }
}
