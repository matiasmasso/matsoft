using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DocRelModel : BaseGuid, IModel
    {
        public string? Nom { get; set; }
        public int Sex { get; set; }
        public bool SubjectePassiu { get; set; }
        public string? Ord { get; set; }
        public int Cod { get; set; }

        public enum Wellknowns
        {
            None,
            Pare,
            Mare,
            Conjuge,
            AviPatern,
            AviaPaterna,
            AviMatern,
            AviaMaterna,
            Fill,
            Filla,
            Net,
            Neta,
            Germa,
            Germana,
            Germanastre,
            CosiGerma,
            CosinaGermana,
            Nebot,
            Neboda,
            Oncle,
            Tieta,
            Nuvi,
            Nuvia,
            PareDelNuvi,
            MareDelNuvi,
            PareDeLaNuvia,
            MareDeLaNuvia,
            Marit,
            Muller,
            Nora,
            Gendre,
            Testador,
            Testadora,
            Difunt,
            Cita,
            Batejat,
            Batejada,
            Cunyat,
            Cunyada,
            Sogre,
            Sogra
        }

        public static DocRelModel? Wellknown(Wellknowns id)
        {
            DocRelModel? retval = null;
            switch (id)
            {
                case Wellknowns.Pare:
                    retval = new DocRelModel(new Guid("3BEB9740-BB0F-4711-9540-8944720CE9E7"));
                    break;
                case Wellknowns.Mare:
                    retval = new DocRelModel(new Guid("5E95CEEA-97EF-40BB-981F-00330B0347BA"));
                    break;
                case Wellknowns.AviPatern:
                    retval = new DocRelModel(new Guid("4006A2E4-5DC2-489A-B169-9FBAFC9E4F9E"));
                    break;
                case Wellknowns.AviaPaterna:
                    retval = new DocRelModel(new Guid("FF3E6DD0-E2ED-4A7B-9FA8-4EBFA3637A7E"));
                    break;
                case Wellknowns.AviMatern:
                    retval = new DocRelModel(new Guid("82FFFA6E-B795-442A-B1D2-A786D20A1BD4"));
                    break;
                case Wellknowns.AviaMaterna:
                    retval = new DocRelModel(new Guid("DC43ED45-0943-43A1-9C64-3C79B78F3C4A"));
                    break;
                case Wellknowns.Fill:
                    retval = new DocRelModel(new Guid("01C3C66B-30AE-41D7-9DBE-F75ECB18C59C"));
                    break;
                case Wellknowns.Filla:
                    retval = new DocRelModel(new Guid("B3265008-4FBE-47D1-9EE8-63AB9D3A324E"));
                    break;
                case Wellknowns.Net:
                    retval = new DocRelModel(new Guid("37385DF4-C2D3-4A65-98C8-6364855261D2"));
                    break;
                case Wellknowns.Neta:
                    retval = new DocRelModel(new Guid("A471C3BD-401A-4563-9A5E-7A27D4542724"));
                    break;
                case Wellknowns.Germa:
                    retval = new DocRelModel(new Guid("08710A58-B1F6-4DE7-A891-58252208D43F"));
                    break;
                case Wellknowns.Germana:
                    retval = new DocRelModel(new Guid("0FA2742B-230A-468F-AC58-13917E3DD231"));
                    break;
                case Wellknowns.CosiGerma:
                    retval = new DocRelModel(new Guid("812893C9-BA0E-410D-9313-A0D0732D0AE9"));
                    break;
                case Wellknowns.CosinaGermana:
                    retval = new DocRelModel(new Guid("B2510DB0-BC52-44F9-8610-DEA8E90BF565"));
                    break;
                case Wellknowns.Nebot:
                    retval = new DocRelModel(new Guid("76D2C84B-5ED6-46D8-A5AB-B8ADB9AEABFE"));
                    break;
                case Wellknowns.Neboda:
                    retval = new DocRelModel(new Guid("65D891E0-0B7F-4AFE-85DA-5742FF94D75A"));
                    break;
                case Wellknowns.Cunyat:
                    retval = new DocRelModel(new Guid("918B1068-A849-49DA-ABA7-E931E813C1FF"));
                    break;
                case Wellknowns.Cunyada:
                    retval = new DocRelModel(new Guid("F2F893B0-1272-45F5-B7FE-1466721B4429"));
                    break;
                case Wellknowns.Oncle:
                    retval = new DocRelModel(new Guid("2F4E09AE-D3ED-4A43-81E5-93D2B27BAF77"));
                    break;
                case Wellknowns.Tieta:
                    retval = new DocRelModel(new Guid("963C3B53-2113-425B-AB04-4E3F77D52FAD"));
                    break;
                case Wellknowns.Nuvi:
                    retval = new DocRelModel(new Guid("EDAD4D79-FF05-48E0-AFA3-9EB07E7380A1"));
                    break;
                case Wellknowns.Nuvia:
                    retval = new DocRelModel(new Guid("F1C18E07-032A-43EF-B65C-EEAA5B4E62F6"));
                    break;
                case Wellknowns.PareDelNuvi:
                    retval = new DocRelModel(new Guid("B42E48CF-B094-4060-BA5A-6C7D2ED85AA8"));
                    break;
                case Wellknowns.MareDelNuvi:
                    retval = new DocRelModel(new Guid("BA6C4AEA-CF13-423B-9F48-1623340AA219"));
                    break;
                case Wellknowns.PareDeLaNuvia:
                    retval = new DocRelModel(new Guid("D761BE46-E052-480F-9314-A4F3373492D9"));
                    break;
                case Wellknowns.MareDeLaNuvia:
                    retval = new DocRelModel(new Guid("326AADCA-DAF7-4C9B-9AE8-7FB390D5FEA9"));
                    break;
                case Wellknowns.Testador:
                    retval = new DocRelModel(new Guid("D899F31F-3C8E-49BB-85EB-F5422031A61D"));
                    break;
                case Wellknowns.Testadora:
                    retval = new DocRelModel(new Guid("04D94D9E-ECDF-4746-B61B-4A6D44318CF6"));
                    break;
                case Wellknowns.Difunt:
                    retval = new DocRelModel(new Guid("B39422CB-CE44-445A-AB74-517B5AD754C9"));
                    break;
                case Wellknowns.Cita:
                    retval = new DocRelModel(new Guid("5CB075CB-7817-47CF-8973-C5FABBE4C925"));
                    break;
                case Wellknowns.Marit:
                    retval = new DocRelModel(new Guid("7937DADE-E5DC-4F20-BA15-9543F84815E8"));
                    break;
                case Wellknowns.Muller:
                    retval = new DocRelModel(new Guid("A79A5C5B-9DAA-4064-9C45-5049D5208D86"));
                    break;
                case Wellknowns.Gendre:
                    retval = new DocRelModel(new Guid("1B66244F-9C16-4833-8BF9-5CC90D1D56D3"));
                    break;
                case Wellknowns.Nora:
                    retval = new DocRelModel(new Guid("147D14CB-FE6D-4276-B354-ABF498BAE27D"));
                    break;
                case Wellknowns.Batejat:
                    retval = new DocRelModel(new Guid("7D9F1179-B34F-4BD7-ADCB-0D729DCB6E80"));
                    break;
                case Wellknowns.Batejada:
                    retval = new DocRelModel(new Guid("8DAB6D3F-FD7E-4D8D-B01E-B8094BD7A10F"));
                    break;
                case Wellknowns.Sogre:
                    retval = new DocRelModel(new Guid("5545B5F6-5AE6-4AC2-A887-D161B671B834"));
                    break;
                case Wellknowns.Sogra:
                    retval = new DocRelModel(new Guid("29D3EE1A-2BF0-4681-9456-7887ACB01792"));
                    break;

            }
            return retval;
        }

        public DocRelModel() : base() { }
        public DocRelModel(Guid guid) : base(guid) { }
        public static DocRelModel Factory() => new DocRelModel { Nom = "(nou rol)" };

        public static string CollectionPageUrl() => Globals.PageUrl("docrels");
        public string PropertyPageUrl() => Globals.PageUrl("docrel", Guid.ToString());
        public string CreatePageUrl() => Globals.PageUrl("docrel");
        public string Caption() => Nom ?? "" ?? "?"; //To implement iModel Interface for property grid selectors
        public override string ToString() => Caption();

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
    }
}
