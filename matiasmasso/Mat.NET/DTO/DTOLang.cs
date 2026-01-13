using DocumentFormat.OpenXml.Office.ActiveX;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DTO
{
    public class DTOLang
    {
        public Ids id { get; set; } //lowercase due to dependency on IOS iMat 24/3/2022

        public enum Ids
        {
            NotSet,
            ESP,
            CAT,
            ENG,
            POR,
            EUS
        }

        public DTOLang() : base()
        {
        }

        public DTOLang(Ids oId) : base()
        {
            id = oId;
        }

        public static DTOLang Factory(Ids oId)
        {
            DTOLang retval = new DTOLang();
            retval.id = oId;
            return retval;
        }

        public static DTOLang Factory(string sTag)
        {
            DTOLang retval;
            switch (sTag.ToLower())
            {
                case "pt":
                case "por":
                    retval = DTOLang.POR();
                    break;
                case "en":
                case "eng":
                    retval = DTOLang.ENG();
                    break;
                case "ca":
                case "cat":
                    retval = DTOLang.CAT();
                    break;
                default:
                    retval = DTOLang.ESP();
                    break;
            }
            return retval;
        }


        public static DTOLang Default()
        {
            return DTOLang.ESP();
        }


        public DTOWebDomain Domain(bool absoluteUrl = true)
        {
            return DTOWebDomain.Factory(this, absoluteUrl);
        }

        public static DTOLang FromBrowserLanguages(List<string> srcs)
        {
            DTOLang retval = null;
            foreach (string src in srcs)
            {
                retval = FromISO639(src);
                if (retval != null)
                    break;
            }

            return retval;
        }



        public static DTOLang FromISO639(string src)
        {
            DTOLang retval = null;
            string topLevel = src.ToLower();
            if (topLevel.Length > 2)
                topLevel = topLevel.Substring(0, 2);
            if (topLevel == "es")
            {
                retval = DTOLang.ESP();
            }
            else if (topLevel == "ca")
            {
                retval = DTOLang.CAT();
            }
            else if (src.ToLower() == "pt")
            {
                retval = DTOLang.POR();
            }
            else if (src.ToLower() == "en")
            {
                retval = DTOLang.ENG();
            }
            return retval;
        }

        public static string LangTagFromISO639OrEmpty(string src)
        {
            string retval = "";
            DTOLang oLang = FromISO639(src);
            if (oLang != null)
                retval = oLang.Tag;
            return retval;
        }

        public static DTOLang FromISO639OrDefault(string src)
        {
            DTOLang retval = FromISO639(src);
            if (retval == null)
                retval = DTOLang.ESP();
            return retval;
        }


        public string ISO6391()
        {
            string retval;
            switch (this.id)
            {
                case Ids.ESP:
                    //retval = "es-ES";
                    retval = "es";
                    break;
                case Ids.CAT:
                    retval = "ca";
                    break;
                case Ids.ENG:
                    //retval = "en-US";
                    retval = "en";
                    break;
                case Ids.POR:
                    //retval = "pt";
                    retval = "pt";
                    break;
                default:
                    retval = "es";
                    break;
            }
            return retval;
        }



        public string ISO639()
        {
            string retval;
            switch (this.id)
            {
                case Ids.ESP:
                    //retval = "es-ES";
                    retval = "es";
                    break;
                case Ids.CAT:
                    retval = "ca-ES";
                    break;
                case Ids.ENG:
                    //retval = "en-US";
                    retval = "en";
                    break;
                case Ids.POR:
                    //retval = "pt";
                    retval = "pt-PT";
                    break;
                default:
                    retval = "es";
                    break;
            }
            return retval;
        }

        static public string Resources(string stringKey)
        {
            string retval = "";
            string esp = DTOLang.ESP().resource(stringKey);
            string cat = DTOLang.CAT().resource(stringKey);
            string eng = DTOLang.ENG().resource(stringKey);
            string por = DTOLang.POR().resource(stringKey);
            retval = esp + "/" + cat + "/" + eng + "/" + por;
            return retval;
        }

        public string resource(string stringKey)
        {
            string retval = "";
            if (stringKey.isNotEmpty())
            {
                string ISO639 = this.ISO639();
                CultureInfo ci = CultureInfo.GetCultureInfo(ISO639);
                retval = GlobalStrings.ResourceManager.GetString(stringKey, ci);
                string s = GlobalStrings.Blog_Title;
            }
            return retval;
        }

        public static string Nom(string iso639, DTOLang oLang)
        {
            string retval = "";
            DTOLang lang = DTOLang.FromISO639(iso639);
            if (lang != null)
                retval = LangNom(lang).Text(oLang);
            return retval;
        }

        public static DTOLangText LangNom(DTOLang oLang)
        {
            DTOLangText retval = new DTOLangText();
            if (oLang != null)
            {
                switch (oLang.Tag)
                {
                    case "ESP":
                        retval = new DTOLangText("Español", "Espanyol", "Spanish", "Espanhol");
                        break;
                    case "CAT":
                        retval = new DTOLangText("Catalán", "Catalá", "Catalan", "Catalão");
                        break;
                    case "ENG":
                        retval = new DTOLangText("Inglés", "Anglès", "English", "Inglês");
                        break;
                    case "POR":
                        retval = new DTOLangText("Portugués", "Portuguès", "Portuguese", "Português");
                        break;
                }
            }
            return retval;
        }

        public string Nom(DTOLang oLang = null)
        {
            string retval = "";
            if (oLang == null)
                oLang = this;

            retval = DTOLang.LangNom(this).Text(oLang);
            return retval;
        }



        public static string Mes(DTOLang oLang, int IntMes)
        {
            string retval;
            switch (oLang.id)
            {
                case DTOLang.Ids.CAT:
                    {
                        retval = VbUtilities.Choose(IntMes, "Gener", "Febrer", "Març", "Abril", "Maig", "Juny", "Juliol", "Agost", "Setembre", "Octubre", "Novembre", "Desembre");
                        break;
                    }

                case DTOLang.Ids.ENG:
                    {
                        retval = VbUtilities.Choose(IntMes, "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
                        break;
                    }

                default:
                    {
                        retval = VbUtilities.Choose(IntMes, "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                        break;
                    }
            }
            return retval;
        }

        public static string MesAbr(DTOLang oLang, int IntMes)
        {
            string retval;
            switch (oLang.id)
            {
                case DTOLang.Ids.CAT:
                    {
                        retval = VbUtilities.Choose(IntMes, "Gen", "Feb", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Oct", "Nov", "Des");
                        break;
                    }

                case DTOLang.Ids.ENG:
                    {
                        retval = VbUtilities.Choose(IntMes, "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                        break;
                    }

                default:
                    {
                        retval = VbUtilities.Choose(IntMes, "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic");
                        break;
                    }
            }
            return retval;
        }




        public static DTOLang FromCulture(string sCulture)
        {
            DTOLang retval = null;
            switch (sCulture)
            {
                case "ca":
                    {
                        retval = DTOLang.CAT();
                        break;
                    }

                case "en":
                    {
                        retval = DTOLang.ENG();
                        break;
                    }

                case "pt":
                    {
                        retval = DTOLang.POR();
                        break;
                    }

                default:
                    {
                        retval = DTOLang.ESP();
                        break;
                    }
            }
            return retval;
        }




        public static DTOLang FactoryByLocale(string value)
        {
            DTOLang retval = null;
            if (value.Length == 2)
            {
                switch (value)
                {
                    case "ca":
                        {
                            retval = DTOLang.CAT();
                            break;
                        }

                    case "en":
                        {
                            retval = DTOLang.ENG();
                            break;
                        }

                    case "pt":
                        {
                            retval = DTOLang.POR();
                            break;
                        }

                    case "es":
                        {
                            retval = DTOLang.ESP();
                            break;
                        }

                    default:
                        {
                            retval = DTOLang.ESP();
                            break;
                        }
                }
            }
            else
                switch (VbUtilities.Left(value, 3))
                {
                    case "ca_":
                        {
                            retval = DTOLang.CAT();
                            break;
                        }

                    case "en_":
                        {
                            retval = DTOLang.ENG();
                            break;
                        }

                    case "pt_":
                        {
                            retval = DTOLang.POR();
                            break;
                        }

                    case "es_":
                        {
                            retval = DTOLang.ESP();
                            break;
                        }

                    default:
                        {
                            retval = DTOLang.ESP();
                            break;
                        }
                }
            return retval;
        }
        public static string Locale(DTOLang oLang)
        {
            string retval = "";
            switch (oLang.id)
            {
                case DTOLang.Ids.CAT:
                    {
                        retval = "ca_ES";
                        break;
                    }

                case DTOLang.Ids.ENG:
                    {
                        retval = "en_US";
                        break;
                    }

                case DTOLang.Ids.POR:
                    {
                        retval = "pt_PT";
                        break;
                    }

                default:
                    {
                        retval = "es_ES";
                        break;
                    }
            }
            return retval;
        }

        public new bool Equals(object oCandidate)
        {
            bool retval = false;
            if (oCandidate != null)
                if (oCandidate is DTOLang)
                    retval = ((DTOLang)oCandidate).id == this.id;
            return retval;
        }

        public bool UnEquals(object oCandidate)
        {
            bool retval = !Equals(oCandidate);
            return retval;
        }

        public string Tag
        {
            get
            {
                string retval = id.ToString();
                return retval;
            }
        }

        public string NomEsp
        {
            get
            {
                string retval = Tradueix("Español", "Catalán", "Inglés", "Portugués");
                return retval;
            }
        }

        public string NomCat
        {
            get
            {
                string retval = Tradueix("Espanyol", "Català", "Anglés", "Portuguès");
                return retval;
            }
        }

        public string NomEng
        {
            get
            {
                string retval = Tradueix("Spanish", "Catalan", "English", "Portuguese");
                return retval;
            }
        }

        public string NomPor
        {
            get
            {
                string retval = Tradueix("Espanhol", "Catalão", "Inglês", "Português");
                return retval;
            }
        }

        public string Tradueix(string Esp, string Cat = "", string Eng = "", string Por = "")
        {
            string retval = Esp;
            switch (id)
            {
                case Ids.CAT:
                    {
                        if (Cat.isNotEmpty())
                            retval = Cat;
                        break;
                    }

                case Ids.ENG:
                    {
                        if (Eng.isNotEmpty())
                            retval = Eng;
                        break;
                    }

                case Ids.POR:
                    {
                        if (Por.isNotEmpty())
                            retval = Por;
                        break;
                    }
            }
            return retval;
        }

        public string Format(string Esp, string Cat, string Eng, string Por, params string[] Params)
        {
            string retval = string.Format(Tradueix(Esp, Cat, Eng, Por), Params);
            return retval;
        }

        public string Mes(int IntMes)
        {
            string retval;
            switch (id)
            {
                case Ids.CAT:
                    {
                        retval = VbUtilities.Choose(IntMes, "Gener", "Febrer", "Març", "Abril", "Maig", "Juny", "Juliol", "Agost", "Setembre", "Octubre", "Novembre", "Desembre");
                        break;
                    }

                case Ids.ENG:
                    {
                        retval = VbUtilities.Choose(IntMes, "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
                        break;
                    }

                default:
                    {
                        retval = VbUtilities.Choose(IntMes, "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                        break;
                    }
            }
            return retval;
        }

        public string MesAbr(int IntMes)
        {
            string retval;
            switch (id)
            {
                case Ids.CAT:
                    {
                        retval = VbUtilities.Choose(IntMes, "Gen", "Feb", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Oct", "Nov", "Des");
                        break;
                    }

                case Ids.ENG:
                    {
                        retval = VbUtilities.Choose(IntMes, "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                        break;
                    }

                default:
                    {
                        retval = VbUtilities.Choose(IntMes, "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic");
                        break;
                    }
            }
            return retval;
        }

        public string WeekDay(int base0Day)
        {
            string retval;

            //base0Day is 0=Sunday, 1=Monday, ... 6=Saturday
            var base1Day = base0Day == 0 ? 7 : base0Day;
            //base0Day is 1=Monday, 2=Tuesday, ... 7=Sunday

            //VbUtilities.Choose es base 1
            switch (id)
            {
                case Ids.CAT:
                    {
                        retval = VbUtilities.Choose(base1Day, "Dilluns", "Dimarts", "Dimecres", "Dijous", "Divendres", "Dissabte", "Diumenge");
                        break;
                    }

                case Ids.ENG:
                    {
                        retval = VbUtilities.Choose(base1Day, "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday");
                        break;
                    }

                default:
                    {
                        retval = VbUtilities.Choose(base1Day, "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo");
                        break;
                    }
            }
            return retval;
        }

        public string WeekDay(DateTime DtFch)
        {
            int iWeekDay = (int)DtFch.DayOfWeek;
            string sWeekdayNom = WeekDay(iWeekDay);
            return sWeekdayNom;
        }

        public static string WeekDay(DTOLang oLang, int base0Day)=> oLang.WeekDay(base0Day);

        public static string WeekDay(DTOLang oLang, DateTime DtFch) => oLang.WeekDay(DtFch);



        public static DTOLang ESP()
        {
            DTOLang retval = new DTOLang(DTOLang.Ids.ESP);
            return retval;
        }

        public static DTOLang CAT()
        {
            DTOLang retval = new DTOLang(DTOLang.Ids.CAT);
            return retval;
        }

        public static DTOLang ENG()
        {
            DTOLang retval = new DTOLang(DTOLang.Ids.ENG);
            return retval;
        }

        public static DTOLang POR()
        {
            DTOLang retval = new DTOLang(DTOLang.Ids.POR);
            return retval;
        }

        public static DTOLang PortugueseOrEsp(DTOLang oLang)
        {
            // si no es portugués, retorna espanyol
            if (oLang.id == DTOLang.Ids.POR)
            {
                return oLang;
            }
            else
            {
                return DTOLang.ESP();
            }
        }

        public bool IsEsp()
        {
            return (this.id == DTOLang.Ids.ESP);
        }
        public bool IsCat()
        {
            return (this.id == DTOLang.Ids.CAT);
        }
        public bool IsEng()
        {
            return (this.id == DTOLang.Ids.ENG);
        }
        public bool IsPor()
        {
            return (this.id == DTOLang.Ids.POR);
        }

        public class Collection
        {
            public static List<DTOLang> All()
            {
                List<DTOLang> retval = new List<DTOLang>();
                {
                    var withBlock = retval;
                    withBlock.Add(new DTOLang(Ids.ESP));
                    withBlock.Add(new DTOLang(Ids.CAT));
                    withBlock.Add(new DTOLang(Ids.ENG));
                    withBlock.Add(new DTOLang(Ids.POR));
                }
                return retval;
            }

            public static string[] ISO6391Array()
            {
                string[] retval = { "es", "ca", "en", "pt" };
                return retval;
            }


        }

        public class Set
        {
            public string Value { get; set; }

            public Set() {
                Value = "0000";
            }

            public Set(string value)
            {
                Value = value;
            }

            public  static Set Default()
            {
                return new Set("1111");
            }

            public Set(bool esp,bool cat, bool eng, bool por)
            {
                var sb = new StringBuilder();
                sb.Append(esp ? "1" : "0");
                sb.Append(cat ? "1" : "0");
                sb.Append(eng ? "1" : "0");
                sb.Append(por ? "1" : "0");
                Value = sb.ToString();
            }

            public bool HasLang(DTOLang lang)
            {
                var retval = (lang.IsEsp() && Value[0] == '1' ||
                    lang.IsCat() && Value[1] == '1' ||
                    lang.IsEng() && Value[2] == '1' ||
                    lang.IsPor() && Value[3] == '1');
                return retval;
            }


        }
    }

}