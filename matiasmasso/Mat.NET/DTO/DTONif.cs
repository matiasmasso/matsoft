using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTONif
    {
        public String Value { get; set; }
        public Cods Cod { get; set; }

        public enum Cods
        {
            Unknown,
            Nif,
            EORI,
            NRT, //Num.de registre Tributari Andorra
            NCM, //num.de comerç
            College,
            CR, //Certificat de Residencia (SII ID 4 per Windeln alemanya)
            Passport
        }

        public static DTONif Factory(Cods cod, String value)
        {
            DTONif retval = new DTONif();
            retval.Cod = cod;
            retval.Value = value;
            return retval;
        }

        public String QualifiedValue(DTOLang lang)
        {
            string codNom = CodNom(this.Cod, lang);
            string retval = string.Format("{0}: {1}", codNom, this.Value);
            return retval;
        }
        public static String CodNom(Cods cod, DTOLang lang)
        {
            return LangText(cod).Tradueix(lang);
        }

        public static DTOLangText LangText(Cods cod)
        {
            DTOLangText retval = null;
            switch (cod)
            {
                case Cods.Nif:
                    retval = DTOLangText.Factory("NIF");
                    break;
                case Cods.EORI:
                    retval = DTOLangText.Factory("EORI");
                    break;
                case Cods.NRT:
                    retval = DTOLangText.Factory("Num. de Registro Tributario", "Num. de Registre Tributari", "Tax Number");
                    break;
                case Cods.NCM:
                    retval = DTOLangText.Factory("Num. de Comercio", "Num. de Comerç", "Commerce Id");
                    break;
                case Cods.College:
                    retval = DTOLangText.Factory("Colegiado num.", "Col·legiat num.", "College membership");
                    break;
                case Cods.CR:
                    retval = DTOLangText.Factory("Certificado de Residencia", "Certificat de Residencia.", "Resident Certificate");
                    break;
                case Cods.Passport:
                    retval = DTOLangText.Factory("Pasaporte", "Passaport", "Passport");
                    break;
                default:
                    retval = DTOLangText.Factory("(No especificado)", "(No especificat)", "(Not specified)");
                    break;
            }
            return retval;
        }

        public class Collection : List<DTONif>
        {

            public static Collection Factory(String value1, Cods cod1 = Cods.Nif, string value2 = "", Cods cod2 = Cods.Unknown)
            {
                Collection retval = new Collection();
                if (value1.isNotEmpty())
                    retval.Add(DTONif.Factory(cod1, value1));
                if (value2.isNotEmpty())
                    retval.Add(DTONif.Factory(cod2, value2));
                return retval;
            }

            public String PrimaryNifValue()
            {
                string retval = "";
                if (PrimaryNif() != null)
                    retval = PrimaryNif().Value;
                return retval;
            }

            public Boolean IsEstrangerResident()
            {
                DTONif primaryNif = this.PrimaryNif();
                Boolean retval = (primaryNif != null && primaryNif.Cod == DTONif.Cods.CR);
                return retval;
            }

            public DTONif PrimaryNif()
            {
                return this.Count == 0 ? null : this[0];
            }
            public DTONif AlternateNif()
            {
                return this.Count < 2 ? null : this[1];
            }
        }
    }
}
