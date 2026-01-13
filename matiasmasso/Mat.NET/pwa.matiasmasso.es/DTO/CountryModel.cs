using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CountryModel:BaseGuid, IModel
    {
        public LangTextModel? Nom { get; set; } = new();
        public string? ISO { get; set; }
        public LangDTO? Lang { get; set; }
        public DTO.ZonaModel.ExportCods ExportCod { get; set; } = 0;
        public string? PrefixeTelefonic { get; set; }


        public enum Wellknowns
        {
            Spain,
            Portugal,
            Andorra,
            Germany
        }

        public enum ExportCods
        {
            Inherits,
            Nacional,
            Intracomunitario,
            Extracomunitario
        }

        //Constructors:
        public CountryModel() : base() {        }
        public CountryModel(Guid guid) : base(guid) { }

        public static CountryModel Default() => CountryModel.Wellknown(Wellknowns.Spain)!;

        //Functions:

        public static CountryModel? Wellknown(Wellknowns id)
        {
            CountryModel? retval = null;
            switch (id)
            {
                case Wellknowns.Spain: retval = new CountryModel(new Guid("AEEA6300-DE1D-4983-9AA2-61B433EE4635")) {ISO="ES" }; break;
                case Wellknowns.Andorra: retval = new CountryModel(new Guid("AE3E6755-8FB7-40A5-A8B3-490ED2C44061")) { ISO = "AD" }; break;
                case Wellknowns.Portugal: retval = new CountryModel(new Guid("631B1258-9761-4254-8ED9-25B9E42FD6D1")) { ISO = "PT" }; break;
                case Wellknowns.Germany: retval = new CountryModel(new Guid("B21500BA-2891-4742-8CFF-8DD65EBB0C82")) { ISO = "DE" }; break;
            }
            return retval;
        }

        public static CountryModel Spain() => Wellknown(Wellknowns.Spain)!;

        public bool IsSpain() => Guid == Wellknown(Wellknowns.Spain)!.Guid;
        public bool IsPortugal() => Guid == Wellknown(Wellknowns.Portugal)!.Guid;

        public static List<CountryModel> Favorites(List<CountryModel>? src = null, CountryModel? selectedValue = null)
        {
            List<CountryModel> retval = new List<CountryModel>();
            if(src == null)
            {
                retval.Add(Wellknown(Wellknowns.Spain));
                retval.Add(Wellknown(Wellknowns.Portugal));
                retval.Add(Wellknown(Wellknowns.Andorra));
            }
            else
            {
            if (src.Any(x => x.ISO == "ES")) retval.Add(src.First(x => x.ISO == "ES"));
            if (src.Any(x => x.ISO == "PT")) retval.Add(src.First(x => x.ISO == "PT"));
            if (src.Any(x => x.ISO == "AD")) retval.Add(src.First(x => x.ISO == "AD"));
            }
            //add selected value if not included in the list
            if(selectedValue != null && !retval.Any(x=>x.ISO == selectedValue.ISO))
                retval.Add(selectedValue);
            return retval;
        }

        public override bool Matches(string searchKey)
        {
            return Nom?.Contains(searchKey) ?? false;
        }

        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());

        public string Caption() => Nom?.Esp ?? "?";

        public GuidNom ToGuidNom(LangDTO lang) => new GuidNom(Guid) {Nom = Nom?.Tradueix(lang) };
        public override string ToString() => Caption();


    }
}
