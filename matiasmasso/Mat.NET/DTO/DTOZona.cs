using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOZona : DTOArea
    {
        public new DTOCountry Country { get; set; }
        public DTOAreaProvincia Provincia { get; set; }

        public DTOLang Lang { get; set; }
        public DTOInvoice.ExportCods ExportCod { get; set; }

        public bool SplitByComarcas { get; set; }
        public List<DTOLocation> Locations { get; set; }
        public List<DTOContact> Contacts { get; set; }

        public DTOPortsCondicio PortsCondicio { get; set; }

        public enum Wellknowns
        {
            bizkaia,
            barcelona,
            girona,
            lleida,
            tarragona,
            CanariasTenerife,
            CanariasLaPalma,
            CanariasGranCanaria,
            CanariasHierro,
            CanariasLaGomera,
            CanariasFuerteventura,
            CanariasLanzarote,
            Ceuta,
            Melilla,
            Madeira,
            Azores,
            Andorra
        }

        public DTOZona() : base()
        {
            base.Cod = Cods.Zona;
            this.Locations = new List<DTOLocation>();
        }

        public DTOZona(Guid oGuid) : base(oGuid)
        {
            base.Cod = Cods.Zona;
            this.Locations = new List<DTOLocation>();
        }

        public static DTOZona Factory(DTOCountry country, string nom = "")
        {
            DTOZona retval = new DTOZona();
            retval.Country = country;
            retval.Nom = nom;
            return retval;
        }

        public static DTOZona Wellknown(DTOZona.Wellknowns id)
        {
            DTOZona retval = null;
            string sGuid = "";
            switch (id)
            {
                case DTOZona.Wellknowns.bizkaia:
                    {
                        sGuid = "8EC77DB0-BE4C-448C-8A50-986F925188DF";
                        break;
                    }

                case DTOZona.Wellknowns.barcelona:
                    {
                        sGuid = "5D799DC5-B56B-4F8D-AA86-BB318EBFB89F";
                        break;
                    }

                case DTOZona.Wellknowns.girona:
                    {
                        sGuid = "383EBF0E-2557-493B-B1A2-4F99EA458165";
                        break;
                    }

                case DTOZona.Wellknowns.lleida:
                    {
                        sGuid = "C4E352AC-88D0-4DDC-9B56-39BB679239DE";
                        break;
                    }

                case DTOZona.Wellknowns.tarragona:
                    {
                        sGuid = "D5289044-9986-41B0-A16D-75261EE67EFD";
                        break;
                    }

                case DTOZona.Wellknowns.CanariasTenerife:
                    {
                        sGuid = "F38971B4-001D-4128-8FF9-1F4C7B9CE3A2";
                        break;
                    }

                case DTOZona.Wellknowns.CanariasLaPalma:
                    {
                        sGuid = "BE926DE7-ED8D-4F54-B965-2DFD182B5E90";
                        break;
                    }

                case DTOZona.Wellknowns.CanariasGranCanaria:
                    {
                        sGuid = "20391DF1-9687-4F46-A645-3EBE8E8C2730";
                        break;
                    }

                case DTOZona.Wellknowns.CanariasHierro:
                    {
                        sGuid = "3BC6964A-29C0-4B50-A3DD-C518B26A7D23";
                        break;
                    }

                case DTOZona.Wellknowns.CanariasLaGomera:
                    {
                        sGuid = "69BDF9DF-15F8-4B89-9CBE-E78BCCAC9D9A";
                        break;
                    }

                case DTOZona.Wellknowns.CanariasFuerteventura:
                    {
                        sGuid = "7D2513A9-B459-4661-A2F3-F69AE0A9102E";
                        break;
                    }

                case DTOZona.Wellknowns.CanariasLanzarote:
                    {
                        sGuid = "0DB00A60-4DE8-4B5C-8214-F8CCD61F909E";
                        break;
                    }

                case DTOZona.Wellknowns.Ceuta:
                    {
                        sGuid = "F059B58D-6E0C-49E5-AC2E-9EBCE7707B11";
                        break;
                    }

                case DTOZona.Wellknowns.Melilla:
                    {
                        sGuid = "49CF61A5-F56A-4C48-B824-E08134D277B9";
                        break;
                    }

                case DTOZona.Wellknowns.Madeira:
                    {
                        sGuid = "B4236E17-8D42-48E9-80A6-E96321D1D324";
                        break;
                    }

                case DTOZona.Wellknowns.Azores:
                    {
                        sGuid = "55C49704-1019-4805-8D44-A7894D1EB9CF";
                        break;
                    }

                case DTOZona.Wellknowns.Andorra:
                    {
                        sGuid = "D0F8B86A-6BCD-4F1B-A581-DAED87CB809E";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOZona(oGuid);
            }
            return retval;
        }

        public bool IsCanariasCeutaMelillaAndorraMadeiraOAzores()
        {
            List<DTOZona> zonas = new List<DTOZona>();
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasTenerife));
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasLaPalma));
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasLaGomera));
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasHierro));
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasGranCanaria));
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasLanzarote));
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.CanariasFuerteventura));
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Ceuta));
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Melilla));
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Andorra));
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Madeira));
            zonas.Add(DTOZona.Wellknown(DTOZona.Wellknowns.Azores));
            bool retval = zonas.Any(x => x.Guid.Equals(this.Guid));
            return retval;
        }

        public static DTOZona Factory(DTOCountry oCountry)
        {
            DTOZona retval = new DTOZona();
            {
                var withBlock = retval;
                withBlock.Country = oCountry;
                withBlock.Lang = oCountry.Lang;
                withBlock.ExportCod = oCountry.ExportCod;
            }
            return retval;
        }

        public bool Matches(string searchKey)
        {
            return MatHelperStd.TextHelper.Match(this.Nom, searchKey);
        }

        public string FullNom(DTOLang oLang)
        {
            string retval = base.Nom;
            if (this.Country != null)
            {
                if (this.Country.ISO != "ES")
                {
                    string sNom = this.Country.LangNom.Tradueix(oLang);
                    if (sNom.isNotEmpty())
                        retval = retval + " (" + sNom + ")";
                }
            }
            return retval;
        }


        public string FullNomSegmented(DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(base.Nom);
            sb.Append("/");
            sb.Append(this.Country.LangNom.Tradueix(oLang));
            string retval = sb.ToString();
            return retval;
        }

        public string FullNomSegmentedReversed(DTOLang oLang)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(this.Country.LangNom.Tradueix(oLang));
            sb.Append("/");
            sb.Append(base.Nom);
            string retval = sb.ToString();
            return retval;
        }

        public DTOLang SuggestedLang()
        {
            var retval = this.Lang ?? this.Country.SuggestedLang();
            return retval;
        }

        public string CountryNom(DTOLang oLang)
        {
            string retval = "";
            if (this.Country != null)
                retval = this.Country.LangNom.Tradueix(oLang);
            return retval;
        }

        public static bool IsCanarias(DTOZona oZona)
        {
            bool retval = oZona.Nom.Contains("Canarias");
            return retval;
        }

        public static string ISOCountryPrefixed(DTOZona oZona)
        {
            string retval = "";
            if (!(oZona.Country == null))
            {
                retval = retval + oZona.Country.ISO + ".";
                retval = retval + oZona.Nom;
            }
            return retval;
        }
    }
}
