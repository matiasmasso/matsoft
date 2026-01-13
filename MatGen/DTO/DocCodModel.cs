using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DocCodModel:BaseGuid, IModel
    {
        public string? Nom { get; set; }
        public string? Ord { get; set; }
        public int? Id { get; set; }

        public enum Wellknowns
        {
            RegistreCivilNaixement,
            Bateig,
            RegistreCivilMatrimoni,
            RegistreParroquialMatrimoni,
            CapitolsMatrimonials,
            RegistreCivilDefuncio,
            RegistreParroquialObit,
            Testament,
            AcceptacioHerencia,
            DispensaMatrimonial,
            ExpedientMatrimonial,
            Llinatge
        }
        public DocCodModel() : base() { }
        public DocCodModel(Guid guid) : base(guid) { }
        public static DocCodModel Factory() => new DocCodModel { Nom = "(nou codi de document)" };

        public static string CollectionPageUrl() => Globals.PageUrl("DocCods");
        public string CreatePageUrl() => Globals.PageUrl("DocCod");
        public string PropertyPageUrl() => Globals.PageUrl("DocCod", Guid.ToString());
        public string Caption() => Nom ?? "" ?? "?"; //To implement iModel Interface for property grid selectors

        public static DocCodModel? Wellknown(Wellknowns id)
        {
            DocCodModel? retval = null;
            switch (id)
            {
                case Wellknowns.RegistreCivilNaixement:
                    retval = new DocCodModel(new Guid("B5536599-ED73-4126-9F56-75F4FF0FF171"));
                    break;
                case Wellknowns.Bateig:
                    retval = new DocCodModel(new Guid("D025B89F-1AB1-42E6-AE95-A68CB85186A9"));
                    break;
                case Wellknowns.RegistreCivilMatrimoni:
                    retval = new DocCodModel(new Guid("0AC0FFE9-C01C-4F23-BE91-4A7E2BB2C821"));
                    break;
                case Wellknowns.RegistreParroquialMatrimoni:
                    retval = new DocCodModel(new Guid("ABA8C418-8003-40FC-9132-733533B44D28"));
                    break;
                case Wellknowns.CapitolsMatrimonials:
                    retval = new DocCodModel(new Guid("E9AD2383-96D3-4EDF-B1C6-2A958C20A49C"));
                    break;
                case Wellknowns.RegistreCivilDefuncio:
                    retval = new DocCodModel(new Guid("4689BE02-C196-40DB-88BC-6EB46C34DA3C"));
                    break;
                case Wellknowns.RegistreParroquialObit:
                    retval = new DocCodModel(new Guid("7C96EBA7-44DF-46B8-8C6B-8D6CC5AD6228"));
                    break;
                case Wellknowns.Testament:
                    retval = new DocCodModel(new Guid("D434ED7A-B9FF-4257-AA7B-20240D0FE67E"));
                    break;
                case Wellknowns.AcceptacioHerencia:
                    retval = new DocCodModel(new Guid("E0D13CA9-FFE8-4F59-89CB-E388442D3424"));
                    break;
                case Wellknowns.DispensaMatrimonial:
                    retval = new DocCodModel(new Guid("000F7C9E-535D-47D7-960C-B3DD1B5584B5"));
                    break;
                case Wellknowns.ExpedientMatrimonial:
                    retval = new DocCodModel(new Guid("892387EF-1B93-4269-8F75-2238A1E901E7"));
                    break;
                case Wellknowns.Llinatge:
                    retval = new DocCodModel(new Guid("60E17359-D51C-4535-BD3B-72B9C34E07DC"));
                    break;
            }
            return retval;
        }
        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nom ?? "";
                var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace;
                retval = searchTerms.All(x => compareInfo.IndexOf(searchTarget, x, options) >= 0);
            }
            return retval;
        }

        public override string ToString() => Caption();

    }
}
