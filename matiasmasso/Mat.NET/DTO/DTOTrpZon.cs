using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOTrpZon : DTOBaseGuid
    {
        public DTOTransportista Transportista { get; set; }
        public int Ord { get; set; }
        public string Nom { get; set; }
        public int Cubicatje { get; set; }
        public bool Activat { get; set; }
        public List<DTOTrpCost> Costs { get; set; }
        public List<DTOZona> Zonas { get; set; }

        public DTOTrpZon() : base()
        {
        }

        public DTOTrpZon(Guid oGuid) : base(oGuid)
        {
        }


        public static DTOTrpCost Cost(DTOTrpZon oTrpZon, decimal DcVolumeM3, decimal DcWeightKg = 0)
        {
            int IntKgCubicats = KgCubicats(oTrpZon, DcVolumeM3, DcWeightKg);
            DTOTrpCost retval = oTrpZon.Costs.OrderBy(x => x.HastaKg).FirstOrDefault(x => x.HastaKg >= IntKgCubicats);
            return retval;
        }

        public static int KgCubicats(DTOTrpZon oTrpZon, decimal DcVolumeM3, decimal DcWeightKg = 0)
        {
            decimal DcCubicatje = CubicatjeOrDefault(oTrpZon);
            int retval = (int)(DcVolumeM3 * DcCubicatje);
            if (DcWeightKg > retval)
                retval = (int)DcWeightKg;
            return retval;
        }

        public static decimal CubicatjeOrDefault(DTOTrpZon oTrpZon)
        {
            decimal retval = oTrpZon.Cubicatje;
            if (retval == 0)
                retval = oTrpZon.Transportista.Cubicaje;
            return retval;
        }
    }
}
