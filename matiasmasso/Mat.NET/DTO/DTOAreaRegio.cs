using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOAreaRegio : DTOArea
    {
        public new DTOCountry Country { get; set; }
        public List<DTOAreaProvincia> Provincias { get; set; }

        public DTOAreaRegio() : base()
        {
            Provincias = new List<DTOAreaProvincia>();
        }

        public DTOAreaRegio(Guid oGuid, string sNom = "") : base(oGuid, sNom)
        {
            Provincias = new List<DTOAreaProvincia>();
        }

        public static DTOAreaRegio Factory(DTOCountry oCountry)
        {
            DTOAreaRegio retval = new DTOAreaRegio();
            {
                var withBlock = retval;
                withBlock.Country = oCountry;
            }
            return retval;
        }
    }
}
