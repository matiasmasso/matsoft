using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOProductDistributor : DTOBaseGuid
    {
        public string Nom { get; set; }
        public DTOContactClass ContactClass { get; set; }
        public string Adr { get; set; }
        public string ZipCod { get; set; }
        public DTOArea Location { get; set; }
        public DTOArea Zona { get; set; }
        public DTOArea Country { get; set; }
        public DTOContactTel Tel { get; set; }

        public string Url { get; set; }
        public bool Promo { get; set; }
        public decimal Sales { get; set; } // dins el periode actiu (60 dies)
        public decimal SalesHistoric { get; set; } // dels darrers dos anys
        public decimal SalesCcx { get; set; } // prorratejat entre els diferents punts de venda que suministra una central de compres
        public bool Raffles { get; set; }
        public DateTime LastFch { get; set; }
        public double Latitud  {get; set;}
        public double Longitud {get; set;}

        public List<string> Items { get; set; }

        public DTOProductDistributor() : base()
        {
        }

        public DTOProductDistributor(Guid oGuid) : base(oGuid)
        {
        }

        public DTOContact Contact()
        {
            return new DTOContact(base.Guid);
        }

        public static List<DTOArea> Countries(List<DTOProductDistributor> src)
        {
            var oCountries = src.Select(x => x.Country).ToList();

            List<DTOArea> retval = new List<DTOArea>();
            foreach (var oCountry in oCountries)
            {
                Guid oGuid = oCountry.Guid;
                if (!retval.Any(x => x.Guid == oGuid))
                    retval.Add(oCountry);
            }
            return retval;
        }

        public static List<DTOArea> Zonas(List<DTOProductDistributor> src, DTOGuidNom oCountry)
        {
            var retval = src.Where(x => x.Country.Equals(oCountry)).Select(x => x.Zona).Distinct().ToList();
            return retval;
        }

        public static List<DTOArea> Zonas(List<DTOProductDistributor> src)
        {
            var retval = src.Select(x => x.Zona).Distinct().ToList();
            return retval;
        }

        public static List<DTOArea> Locations(List<DTOProductDistributor> src, DTOGuidNom oZona)
        {
            List<DTOArea> retval = src.Where(x => x.Zona.Equals(oZona)).Select(x => x.Location).Distinct().ToList();
            return retval;
        }

        public static List<DTOArea> Locations(List<DTOProductDistributor> src)
        {
            List<DTOArea> retval = src.Select(x => x.Location).Distinct().ToList();
            return retval;
        }


        public static List<DTOProductDistributor> PremiumOrSpareDistributors(List<DTOProductDistributor> oDistributors, DTOArea oLocation)
        {
            var oAllDistributors = oDistributors.Where(x => x.Location.Equals(oLocation)).ToList();
            var oPremiumDistributors = oAllDistributors.Where(x => x.Sales > 0).ToList(); // those who purchased within reasonable date
            var oSpareDistributors = oAllDistributors.Where(x => x.Sales == 0).ToList(); // those who have not purchased since long ago. Just display them if no Premium ones are available

            List<DTOProductDistributor> retval = new List<DTOProductDistributor>();
            if (oPremiumDistributors.Count() == 0)
            {
                var oHistory = oSpareDistributors.Where(x => x.SalesHistoric > 0).ToList();
                var oNoHistory = oSpareDistributors.Where(x => x.SalesHistoric == 0).ToList();
                if (oHistory.Count() == 0)
                    retval = oNoHistory;
                else
                    retval = oHistory;
            }
            else
                retval = oPremiumDistributors;
            return retval;
        }

        public static List<DTOProductDistributor> LocationDistributors(List<DTOProductDistributor> oDistributors, DTOArea oLocation)
        {
            var oAllDistributors = oDistributors.Where(x => x.Location.Equals(oLocation));
            var oPremiumDistributors = oAllDistributors.Where(x => x.Sales > 0).ToList(); // those who purchased within reasonable date
            var oSpareDistributors = oAllDistributors.Where(x => x.Sales == 0).ToList(); // those who have not purchased since long ago. Just display them if no Premium ones are available

            List<DTOProductDistributor> retval = new List<DTOProductDistributor>();
            if (oPremiumDistributors.Count == 0)
            {
                var oHistory = oSpareDistributors.Where(x => x.SalesHistoric > 0).ToList();
                var oNoHistory = oSpareDistributors.Where(x => x.SalesHistoric == 0).ToList();
                if (oHistory.Count == 0)
                    retval = oNoHistory;
                else
                    retval = oHistory;
            }
            else
                retval = oPremiumDistributors;
            return retval;
        }

        public static List<DTOProductDistributor> All(List<DTOProductDistributor> src, DTOGuidNom oLocation)
        {
            List<DTOProductDistributor> retval = src.Where(x => x.Location.Equals(oLocation)).ToList();
            return retval;
        }

        public static DTOBaseGuid BestZona(List<DTOProductDistributor> oDistributors)
        {
            DTOBaseGuid retval = null/* TODO Change to default(_) if this is not a reference type */;
            var query = oDistributors.GroupBy(g => new { g.Zona }).Select(group => new { Zona = group.Key.Zona, groupCount = group.Count() }).OrderBy(z => z.groupCount);

            if (query.Count() > 0)
                retval = query.Last().Zona;
            return retval;
        }

        public static DTOBaseGuid BestLocation(List<DTOProductDistributor> oDistributors)
        {
            DTOBaseGuid retval = null/* TODO Change to default(_) if this is not a reference type */;
            var query = oDistributors.GroupBy(g => new { g.Location }).Select(group => new { Location = group.Key.Location, groupCount = group.Count() }).OrderBy(z => z.groupCount);

            if (query.Count() > 0)
                retval = query.Last().Location;
            return retval;
        }

        public static MatHelper.Excel.Sheet Excel(List<DTOProductDistributor> items)
        {
            string sTitle = string.Format("M+O {0} {1} {2:00}", DTO.GlobalVariables.Today().Year, DTO.GlobalVariables.Today().Month, DTO.GlobalVariables.Today().Day);
            string sFilename = string.Format("M+O Store Locator {0:yyyyMMdd}.xlsx", DTO.GlobalVariables.Today());
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet(sTitle, sFilename);
            {
                var withBlock = retval;
                withBlock.AddColumn("Store Name");
                withBlock.AddColumn("Address");
                withBlock.AddColumn("City");
                withBlock.AddColumn("Postcode");
                withBlock.AddColumn("Region");
                withBlock.AddColumn("Country");
            }

            foreach (var item in items)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                oRow.AddCell(item.Nom);
                oRow.AddCell(item.Adr);
                oRow.AddCell(item.Location.Nom);
                oRow.AddCell(item.ZipCod);
                oRow.AddCell(item.Zona.Nom);
                oRow.AddCell(item.Country.Nom);
            }

            return retval;
        }
    }
}
