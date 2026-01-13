using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOIncentiu : DTOBaseGuid
    {
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public DTOProduct Product { get; set; } // marca per discriminar a qui se li comunica
        public List<DTODistributionChannel> Channels { get; set; }
        public List<DTOProduct> Products { get; set; } // productes als que se li aplicará automaticament la promo
        public List<DTOQtyDto> QtyDtos { get; set; }

        public DTOLangText Title { get; set; }
        public DTOLangText Excerpt { get; set; }
        public DTOLangText Bases { get; set; }
        public string ManufacturerContribution { get; set; }

        public bool CliVisible { get; set; }
        public bool RepVisible { get; set; }

        public bool OnlyInStk { get; set; }
        public Cods Cod { get; set; }
        public DTOEvento Evento { get; set; }
        public int PdcsCount { get; set; }
        public List<DTOContact> PointsOfSale { get; set; }

        [JsonIgnore]
        public Byte[] Thumbnail { get; set; }
        public int Unitats { get; set; }
        public int Dto { get; set; }

        public enum Wellknowns
        {
            notSet,
            feriasLocales
        }

        public enum Cods
        {
            notSet,
            dto,
            freeUnits
        }

        public DTOIncentiu() : base()
        {
            this.Title = new DTOLangText(base.Guid, DTOLangText.Srcs.IncentiuTitle);
            this.Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.IncentiuExcerpt);
            this.Bases = new DTOLangText(base.Guid, DTOLangText.Srcs.IncentiuBases);
        }

        public DTOIncentiu(Guid oGuid) : base(oGuid)
        {
            this.Title = new DTOLangText(base.Guid, DTOLangText.Srcs.IncentiuTitle);
            this.Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.IncentiuExcerpt);
            this.Bases = new DTOLangText(base.Guid, DTOLangText.Srcs.IncentiuBases);
        }

        public static DTOIncentiu Wellknown(DTOIncentiu.Wellknowns id)
        {
            DTOIncentiu retval = null;
            switch (id)
            {
                case DTOIncentiu.Wellknowns.feriasLocales:
                    {
                        retval = new DTOIncentiu(new Guid("DA758FDB-983F-4A43-B0A9-70DF027CC878"));
                        break;
                    }
            }
            return retval;
        }


        public static decimal GetDto(DTOIncentiu oIncentiu, int iQty)
        {
            decimal retval = 0;
            foreach (DTOQtyDto oItm in oIncentiu.QtyDtos)
            {
                if (iQty < oItm.Qty)
                    break;
                retval = oItm.Dto;
            }
            return retval;
        }
    }

    public class DTOIncentiuResult
    {
        public DTOIncentiu Incentiu { get; set; }
        public DTOQtyDto QtyDto { get; set; } // acumula les unitats dels albarans i fixa el descompte que li toca
    }
}
