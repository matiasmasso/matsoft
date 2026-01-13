using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MarketPlaceModel:BaseGuid, IModel
    {
        public string? Nom { get; set; }
        public string? Web { get; set; }
        public decimal? Commission{ get; set; }

        public enum Wellknowns
        {
            Shop4moms,
            Miravia,
            Others
        }

        public MarketPlaceModel() : base() { }
        public MarketPlaceModel(Guid guid) : base(guid) { }

        public static MarketPlaceModel Factory() => new MarketPlaceModel()
            {
                Nom = "(nou marketplace)"
            };

        public static MarketPlaceModel? Wellknown(Wellknowns id)
        {
            MarketPlaceModel? retval = null;
            switch (id)
            {
                case Wellknowns.Shop4moms: retval = new MarketPlaceModel(new Guid("B8691C41-CFE8-4DA4-8F92-0786054922F8")); break;
                case Wellknowns.Miravia: retval = new MarketPlaceModel(new Guid("A0CC2DE5-509A-4354-94E2-6862AE0783F9")); break;
            }
            return retval;
        }

        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());

    }
}
