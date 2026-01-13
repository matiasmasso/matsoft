using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOAreaProvincia : DTOArea
    {
        public DTOAreaRegio Regio { get; set; }
        public string Mod347 { get; set; }
        public string Intrastat { get; set; }
        public List<DTOZona> Zonas { get; set; }

        public enum Wellknowns
        {
            barcelona,
            ceuta,
            melilla,
            tenerife,
            laspalmas
        }

        public DTOAreaProvincia() : base()
        {
            Zonas = new List<DTOZona>();
        }

        public DTOAreaProvincia(Guid oGuid) : base(oGuid)
        {
            Zonas = new List<DTOZona>();
        }

        public static DTOAreaProvincia Factory(DTOCountry oCountry)
        {
            DTOAreaProvincia retval = new DTOAreaProvincia();
            {
                var withBlock = retval;
                withBlock.Regio = DTOAreaRegio.Factory(oCountry);
                withBlock.Regio.Nom = "(sel·lecionar regió)";
                withBlock.Nom = "(nova provincia)";
            }
            return retval;
        }

        public static DTOAreaProvincia Wellknown(DTOAreaProvincia.Wellknowns id)
        {
            DTOAreaProvincia retval = null;
            string sGuid = "";
            switch (id)
            {
                case DTOAreaProvincia.Wellknowns.barcelona:
                    {
                        sGuid = "C4E41C16-93C9-41CA-9340-28ADA23B299D";
                        break;
                    }
                case DTOAreaProvincia.Wellknowns.ceuta:
                    {
                        sGuid = "9B4F3448-BD92-4595-A405-11533A0E5DC0";
                        break;
                    }
                case DTOAreaProvincia.Wellknowns.melilla:
                    {
                        sGuid = "9A6DCCC9-19C7-4A04-BE35-1F4F27762840";
                        break;
                    }
                case DTOAreaProvincia.Wellknowns.tenerife:
                    {
                        sGuid = "C3E51A01-1E52-4691-99F1-9433F5A461C7";
                        break;
                    }
                case DTOAreaProvincia.Wellknowns.laspalmas:
                    {
                        sGuid = "E2467D3B-E111-4A6E-9D1A-89A7490D0818";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOAreaProvincia(oGuid);
            }
            return retval;
        }
    }
}
