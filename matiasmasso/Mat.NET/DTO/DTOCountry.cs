using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOCountry : DTOArea
    {
        public string ISO { get; set; }
        // Shadows Property nom As DTOLangText

        public DTOLangText LangNom { get; set; }
        public DTOLang Lang { get; set; }
        public DTOInvoice.ExportCods ExportCod { get; set; }
        public string PrefixeTelefonic { get; set; }
        [JsonIgnore]
        public List<DTOAreaRegio> Regions { get; set; }
        public List<DTOZona> Zonas { get; set; }
        public List<DTOZip> Zips { get; set; }
        public List<DTOContact> Contacts { get; set; }

        public enum Wellknowns
        {
            NotSet,
            Spain,
            Portugal,
            Andorra,
            Germany
        }

        public DTOCountry() : base()
        {
            this.Cod = Cods.Country;
            this.LangNom = new DTOLangText();
            this.Zonas = new List<DTOZona>();
            this.Regions = new List<DTOAreaRegio>();
            this.Lang = DTOLang.ENG();
        }

        public DTOCountry(Guid oGuid) : base(oGuid)
        {
            this.Cod = Cods.Country;
            this.LangNom = new DTOLangText();
            this.Zonas = new List<DTOZona>();
            this.Regions = new List<DTOAreaRegio>();
            this.Lang = DTOLang.ENG();
        }

        public DTOCountry(Guid oGuid, string siso) : base(oGuid)
        {
            this.Cod = Cods.Country;
            this.LangNom = new DTOLangText();
            this.ISO = siso;
            this.Zonas = new List<DTOZona>();
            this.Lang = DTOLang.ENG();
        }

        public static new DTOCountry Factory(Guid oGuid, string sIso = "")
        {
            DTOCountry retval = new DTOCountry(oGuid);
            retval.ISO = sIso;
            return retval;
        }


        public static DTOCountry Wellknown(DTOCountry.Wellknowns id)
        {
            DTOCountry retval = null;
            string sGuid = "";
            string Iso = "";
            switch (id)
            {
                case DTOCountry.Wellknowns.Spain:
                    {
                        sGuid = "AEEA6300-DE1D-4983-9AA2-61B433EE4635";
                        Iso = "ES";
                        break;
                    }

                case DTOCountry.Wellknowns.Portugal:
                    {
                        sGuid = "631B1258-9761-4254-8ED9-25B9E42FD6D1";
                        Iso = "PT";
                        break;
                    }

                case DTOCountry.Wellknowns.Andorra:
                    {
                        sGuid = "AE3E6755-8FB7-40A5-A8B3-490ED2C44061";
                        Iso = "AD";
                        break;
                    }

                case DTOCountry.Wellknowns.Germany:
                    {
                        sGuid = "B21500BA-2891-4742-8CFF-8DD65EBB0C82";
                        Iso = "DE";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOCountry(oGuid);
                retval.ISO = Iso;
            }
            return retval;
        }

        public static bool IsEsp(DTOCountry oCountry)
        {
            bool retval = false;
            if (oCountry.ISO == "")
                retval = oCountry.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain));
            else if (oCountry.ISO == "ES")
                retval = true;
            return retval;
        }

        public static bool IsAnd(DTOCountry oCountry)
        {
            bool retval = oCountry.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Andorra));
            return retval;
        }

        public static bool IsPt(DTOCountry oCountry)
        {
            bool retval = oCountry.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Portugal));
            return retval;
        }

        public static DTOCountry Parse(string src, List<DTOCountry> allCountries)
        {
            var retval = allCountries.FirstOrDefault(x => DTOCountry.Match(x, src));
            return retval;
        }

        public bool Matches(string searchKey)
        {
            return this.LangNom.Contains(searchKey);
        }


        public static bool Match(DTOCountry oCountry, string src)
        {
            bool retval = false;
            if (oCountry != null)
            {
                switch (src)
                {
                    case "":
                        {
                            break;
                        }

                    default:
                        {
                            {
                                var withBlock = oCountry;
                                if (string.Compare(src, withBlock.ISO, true) == 0)
                                    retval = true;
                                else if (string.Compare(src, withBlock.LangNom.Esp, true) == 0)
                                    retval = true;
                                else if (string.Compare(src, withBlock.LangNom.Cat, true) == 0)
                                    retval = true;
                                else if (string.Compare(src, withBlock.LangNom.Eng, true) == 0)
                                    retval = true;
                                else if (string.Compare(src, withBlock.LangNom.Por, true) == 0)
                                    retval = true;
                            }

                            break;
                        }
                }
            }
            return retval;
        }

        public DTOLang SuggestedLang()
        {
            DTOLang retval = DTOLang.ENG();
            if (this.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)))
                retval = DTOLang.ESP();
            else if (this.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Andorra)))
                retval = DTOLang.CAT();
            else if (this.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Portugal)))
                retval = DTOLang.POR();
            return retval;
        }

        public static string NomTraduit(DTOCountry oCountry, DTOLang oLang)
        {
            string retval = "";
            if (oCountry != null)
                retval = oCountry.LangNom.Tradueix(oLang);
            return retval;
        }

        public static List<DTOCountry> Clon(List<DTOCountry> oCountries, List<Exception> exs)
        {
            List<DTOCountry> retval = new List<DTOCountry>();
            foreach (var oCountry in oCountries)
            {
                DTOCountry oClon = new DTOCountry();
                if (DTOBaseGuid.CopyPropertyValues<DTOCountry>(oCountry, oClon, exs))
                    retval.Add(oClon);
            }
            return retval;
        }

        public class Collection : List<DTOCountry>
        {
            public static Collection Favorites()
            {
                Collection retval = new Collection();
                retval.Add(DTOCountry.Wellknown(Wellknowns.Spain));
                retval.Add(DTOCountry.Wellknown(Wellknowns.Portugal));
                retval.Add(DTOCountry.Wellknown(Wellknowns.Andorra));
                return retval;
            }
        }
    }
}
