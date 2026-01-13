using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CountryDTO:BaseGuid
    {
        public LangTextDTO? Nom { get; set; } = new();
        public string? Iso { get; set; }

        public enum Wellknowns
        {
            NotSet,
            Spain,
            Portugal,
            Andorra,
            Germany
        }


        public CountryDTO() : base() { }
        public CountryDTO(Guid guid) : base(guid) { }

        public static CountryDTO? Wellknown(Wellknowns id)
        {
            CountryDTO? retval = null;
            switch (id)
            {
                case Wellknowns.Spain: retval = new CountryDTO(new Guid("AEEA6300-DE1D-4983-9AA2-61B433EE4635")); break;
                case Wellknowns.Andorra: retval = new CountryDTO(new Guid("AE3E6755-8FB7-40A5-A8B3-490ED2C44061")); break;
                case Wellknowns.Portugal: retval = new CountryDTO(new Guid("631B1258-9761-4254-8ED9-25B9E42FD6D1")); break;
                case Wellknowns.Germany: retval = new CountryDTO(new Guid("B21500BA-2891-4742-8CFF-8DD65EBB0C82")); break;
            }
            return retval;
        }

        public bool IsSpain() => Guid.Equals(Spain().Guid);
        public bool IsPortugal() => Guid.Equals(Portugal().Guid);
        public bool IsAndorra() => Guid.Equals(Andorra().Guid);

        public static CountryDTO Spain() => CountryDTO.Wellknown(Wellknowns.Spain)!;
        public static CountryDTO Andorra() => CountryDTO.Wellknown(Wellknowns.Andorra)!;
        public static CountryDTO Portugal() => CountryDTO.Wellknown(Wellknowns.Portugal)!;

        public new string ToString() => string.Format("DTO.CountryDTO: {0}", Nom?.Esp ?? "?");

    }

}
