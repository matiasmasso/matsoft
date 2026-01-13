using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOCliApertura : DTOBaseGuid
    {
        public string Nom { get; set; }
        public string RaoSocial { get; set; }
        public string NomComercial { get; set; }
        public string Nif { get; set; }
        public string Adr { get; set; }
        public string Zip { get; set; }
        public string Cit { get; set; }
        public DTOZona Zona { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public DTOContactClass ContactClass { get; set; }
        public CodsSuperficie CodSuperficie { get; set; } = CodsSuperficie.NotSet;
        public CodsVolumen CodVolumen { get; set; } = CodsVolumen.NotSet;
        public int SharePuericultura { get; set; }
        public string OtherShares { get; set; }
        public CodsSalePoint CodSalePoint { get; set; } = CodsSalePoint.NotSet;
        public string Associacions { get; set; }
        public CodsAntiguedad CodAntiguedad { get; set; } = CodsAntiguedad.NotSet;
        public DateTime FchApertura { get; set; }
        public CodsExperiencia CodExperiencia { get; set; } = CodsExperiencia.NotSet;
        public List<DTOGuidNom> Brands { get; set; }
        public string OtherBrands { get; set; }
        public string Obs { get; set; }
        public DateTime FchCreated { get; set; }
        public CodsTancament CodTancament { get; set; } = CodsTancament.StandBy;
        public DTOContact Contact { get; set; }
        public string RepObs { get; set; }
        public DTOLang Lang { get; set; }

        public enum CodsSuperficie
        {
            NotSet,
            lt50,
            from50to100,
            from100to200,
            from200to300,
            gt300
        }

        public enum CodsVolumen
        {
            NotSet,
            lt50K,
            from50to100,
            from100to300,
            from300to600,
            gt600k
        }

        public enum CodsSalePoint
        {
            NotSet,
            Single,
            Twin,
            ThreeOrMore
        }

        public enum CodsAntiguedad
        {
            NotSet,
            EnEstudio,
            EnEjecucion,
            Lt1year,
            From1to3Years,
            Gt3Years
        }

        public enum CodsExperiencia
        {
            NotSet,
            PrimeraExperiencia,
            VieneDeOtroSector,
            HaTrabajadoYaEnElSector
        }

        public enum CodsTancament
        {
            StandBy,
            Visitat,
            ClientNou,
            Cancelled
        }

        public DTOCliApertura() : base()
        {
            Brands = new List<DTOGuidNom>();
        }

        public DTOCliApertura(Guid oGuid) : base(oGuid)
        {
        }

        public string Url(bool absoluteUrl = false)
        {
            return DTOWebDomain.Default(absoluteUrl).Url("apertura", base.Guid.ToString());
        }

        public string StatusLabel(DTOLang oLang)
        {
            return DTOCliApertura.StatusLabel(CodTancament, oLang);
        }

        public static string StatusLabel(DTOCliApertura.CodsTancament oStatus, DTOLang oLang)
        {
            string retval = "";
            switch (oStatus)
            {
                case DTOCliApertura.CodsTancament.Cancelled:
                    {
                        retval = oLang.Tradueix("Cancelado", "Cancel·lat", "Cancelled");
                        break;
                    }

                case DTOCliApertura.CodsTancament.ClientNou:
                    {
                        retval = oLang.Tradueix("Completado", "Complert", "Completed");
                        break;
                    }

                case DTOCliApertura.CodsTancament.StandBy:
                    {
                        retval = oLang.Tradueix("A la espera", "A la espera", "Waiting");
                        break;
                    }

                case DTOCliApertura.CodsTancament.Visitat:
                    {
                        retval = oLang.Tradueix("Visitado", "Visitat", "Visited");
                        break;
                    }
            }
            return retval;
        }

        public static string CodSuperficieText(DTOCliApertura.CodsSuperficie oCodSuperficie, DTOLang oLang)
        {
            string retval = "";
            switch (oCodSuperficie)
            {
                case DTOCliApertura.CodsSuperficie.lt50:
                    {
                        retval = oLang.Tradueix("menos de 50 m2", "menys de 50 m2", "less than 50m2", "menos de 50 m2");
                        break;
                    }

                case DTOCliApertura.CodsSuperficie.from50to100:
                    {
                        retval = oLang.Tradueix("entre 50 y 100 m2", "entre 50 i 100 m2", "from 50 to 100 m2", "entre 50 e 100 m2");
                        break;
                    }

                case DTOCliApertura.CodsSuperficie.from100to200:
                    {
                        retval = oLang.Tradueix("entre 100 y 200 m2", "entre 100 i 200 m2", "from 100 to 200 m2", "entre 100 e 200 m2");
                        break;
                    }

                case DTOCliApertura.CodsSuperficie.from200to300:
                    {
                        retval = oLang.Tradueix("entre 200 y 300 m2", "entre 200 i 300 m2", "from 200 to 300 m2", "entre 200 e 300 m2");
                        break;
                    }

                case DTOCliApertura.CodsSuperficie.gt300:
                    {
                        retval = oLang.Tradueix("más de 300 m2", "més de 300 m2", "more than 300 m2", "mais de 300 m2");
                        break;
                    }
            }
            return retval;
        }

        public string CodSuperficieText(DTOLang oLang)
        {
            return CodSuperficieText(CodSuperficie, oLang);
        }

        public static string CodVolumenText(DTOCliApertura.CodsVolumen oCodVolumen, DTOLang oLang)
        {
            string retval = "";
            switch (oCodVolumen)
            {
                case DTOCliApertura.CodsVolumen.lt50K:
                    {
                        retval = oLang.Tradueix("menos de 50.000,00 €", "menys de 50.000,00 €", "less than 50.000,00 €", "menos de 50.000,00 €");
                        break;
                    }

                case DTOCliApertura.CodsVolumen.from50to100:
                    {
                        retval = oLang.Tradueix("entre 50.000,00 € y 100.000,00 €", "entre 50.000,00 € i 100.000,00 €", "from 50.000,00 € to 100.000,00 €", "entre 50.000,00 € e 100.000,00 €");
                        break;
                    }

                case DTOCliApertura.CodsVolumen.from100to300:
                    {
                        retval = oLang.Tradueix("entre 100.000,00 € y 300.000,00 €", "entre 100.000,00 € i 300.000,00 €", "from 100.000,00 € to 300.000,00 €", "entre 100.000,00 € e 300.000,00 €");
                        break;
                    }

                case DTOCliApertura.CodsVolumen.from300to600:
                    {
                        retval = oLang.Tradueix("entre 300.000,00 € y 600.000,00 €", "entre 300.000,00 € i 600.000,00 €", "from 300.000,00 € to 600.000,00 €", "entre 300.000,00 € e 600.000,00 €");
                        break;
                    }

                case DTOCliApertura.CodsVolumen.gt600k:
                    {
                        retval = oLang.Tradueix("más de 600.000,00 €", "més de 600.000,00 €", "more than 600.000,00 €", "mais de 600.000,00 €");
                        break;
                    }
            }
            return retval;
        }

        public string CodVolumenText(DTOLang oLang)
        {
            return CodVolumenText(CodVolumen, oLang);
        }


        public static string CodAntiguedadText(DTOCliApertura.CodsAntiguedad oCodAntiguedad, DTOLang oLang)
        {
            string retval = "";
            switch (oCodAntiguedad)
            {
                case DTOCliApertura.CodsAntiguedad.EnEstudio:
                    {
                        retval = oLang.Tradueix("en proyecto. Estoy estudiando aun su viabilidad", "en projecte. Estic estudiant la seva viabilitat", "In project. Still evaluating viability", "em projeto. Estou a estudar a viabilidade");
                        break;
                    }

                case DTOCliApertura.CodsAntiguedad.EnEjecucion:
                    {
                        retval = oLang.Tradueix("en ejecución. Con fecha de inauguración en un plazo concreto", "en execució, amb inauguració en termini fixat", "on execution, with fixed launching date", "em execução. Com data de inauguração num prazo concreto");
                        break;
                    }

                case DTOCliApertura.CodsAntiguedad.Lt1year:
                    {
                        retval = oLang.Tradueix("menos de un año en funcionamiento", "menys de un any en funcionament", "less than 1 year activity", "menos de um ano em funcionamento");
                        break;
                    }

                case DTOCliApertura.CodsAntiguedad.From1to3Years:
                    {
                        retval = oLang.Tradueix("entre uno y tres años", "entre un i tres anys", "between 1 and 3 years", "entre um e três anos");
                        break;
                    }

                case DTOCliApertura.CodsAntiguedad.Gt3Years:
                    {
                        retval = oLang.Tradueix("más de 3 años", "més de tres anys", "more than 3 years", "mais de 3 anos");
                        break;
                    }
            }
            return retval;
        }

        public string CodAntiguedadText(DTOLang oLang)
        {
            return CodAntiguedadText(CodAntiguedad, oLang);
        }


        public static string CodSalePointText(DTOCliApertura.CodsSalePoint oCodSalePoint, DTOLang oLang)
        {
            string retval = "";
            switch (oCodSalePoint)
            {
                case DTOCliApertura.CodsSalePoint.Single:
                    {
                        retval = oLang.Tradueix("uno solo", "un solsament", "just one", "só um");
                        break;
                    }

                case DTOCliApertura.CodsSalePoint.Twin:
                    {
                        retval = oLang.Tradueix("dos", "dos", "two", "dois");
                        break;
                    }

                case DTOCliApertura.CodsSalePoint.ThreeOrMore:
                    {
                        retval = oLang.Tradueix("tres o más", "tres o més", "three or more", "três ou mais");
                        break;
                    }
            }
            return retval;
        }

        public string CodSalePointText(DTOLang oLang)
        {
            return CodSalePointText(CodSalePoint, oLang);
        }

        public static string CodExperienciaText(DTOCliApertura.CodsExperiencia oCodExperiencia, DTOLang oLang)
        {
            string retval = "";
            switch (oCodExperiencia)
            {
                case DTOCliApertura.CodsExperiencia.PrimeraExperiencia:
                    {
                        retval = oLang.Tradueix("es mi primera experiencia comercial", "es la meva primera experiencia comercial", "this is my first retail experience", "é a minha primeira experiência comercial");
                        break;
                    }

                case DTOCliApertura.CodsExperiencia.VieneDeOtroSector:
                    {
                        retval = oLang.Tradueix("tengo experiencia comercial de otros sectores", "tinc experiencia comercial en altres sectors", "I've got commercial experience on alternative fields", "tenho experiência comercial de outros sectores");
                        break;
                    }

                case DTOCliApertura.CodsExperiencia.HaTrabajadoYaEnElSector:
                    {
                        retval = oLang.Tradueix("he trabajado anteriormente en el sector de la puericultura", "he treballat anteriorment al sector de la puericultura", "I've already been working on child care field", "trabalhei anteriormente no sector da puericultura");
                        break;
                    }
            }
            return retval;
        }

        public string CodExperienciaText(DTOLang oLang)
        {
            return CodExperienciaText(CodExperiencia, oLang);
        }

        public string FullLocation()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(Cit))
                sb.Append(Cit);
            if (Zona != null)
            {
                if (Cit != Zona.Nom)
                    sb.Append(" (" + Zona.Nom + ")");
            }
            if (!string.IsNullOrEmpty(Adr))
                sb.Append(" - " + Adr);
            var textInfo = (new System.Globalization.CultureInfo("es-ES", false)).TextInfo;
            string retval = textInfo.ToTitleCase(sb.ToString());
            return retval;
        }

        public string FullNom()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(Nom))
                sb.Append(Nom);
            if (!string.IsNullOrEmpty(RaoSocial))
            {
                if (RaoSocial != Nom)
                    sb.Append(" " + RaoSocial);
            }
            if (!string.IsNullOrEmpty(NomComercial))
            {
                if (NomComercial != RaoSocial & NomComercial != Nom)
                    sb.Append(" " + NomComercial);
            }
            var textInfo = (new System.Globalization.CultureInfo("es-ES", false)).TextInfo;
            string retval = textInfo.ToTitleCase(sb.ToString());
            return retval;
        }

        public class Collection : List<DTOCliApertura>
        {
            public Collection Open()
            {
                Collection retval = new Collection();
                retval.AddRange(base.ToArray().Where(x => x.CodTancament == CodsTancament.StandBy));
                return retval;
            }
            public Collection Closed()
            {
                Collection retval = new Collection();
                retval.AddRange(base.ToArray().Where(x => x.CodTancament != CodsTancament.StandBy));
                return retval;
            }
        }

    }
}
