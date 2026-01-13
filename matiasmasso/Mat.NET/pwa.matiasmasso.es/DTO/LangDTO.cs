using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class LangDTO
    {
        public Ids Id { get; set; }
        //public Guid Guid { get; set; } = Guid.NewGuid(); //fake to meet IModel requirements
        //public bool IsNew { get; set; } = false; //fake to meet IModel requirements

        public enum Ids
        {
            Unknown,
            ESP,
            CAT,
            ENG,
            POR
        }

        public LangDTO() { }
        public LangDTO(Ids id)
        {
            Id = id;
        }
        public LangDTO(string? tag)
        {
            Id = (Enum.TryParse(tag, out Ids id)) ? id : Ids.ESP;
        }

        public string Tag() => Id.ToString();

        public static LangDTO FromCultureInfo(string? cultureInfo)
        {
            LangDTO retval = Default();
            if (cultureInfo != null && cultureInfo.Length >= 2)
            {
                string[] esp = { "es" };
                string[] cat = { "ca" };
                string[] eng = { "en", "de", "pl" };
                string[] por = { "pt" };

                var ci = cultureInfo.Substring(0, 2).ToLower();
                if (esp.Contains(ci))
                    retval = LangDTO.Esp();
                else if (cat.Contains(ci))
                    retval = LangDTO.Cat();
                else if (eng.Contains(ci))
                    retval = LangDTO.Eng();
                else if (por.Contains(ci))
                    retval = LangDTO.Por();
            }
            return retval;
        }


        public static LangDTO? FromUrl(string url)
        {
            LangDTO? retval = null;
            var uri = new Uri(url);
            var segments = uri.AbsolutePath.Split('/');
            var cc = segments.FirstOrDefault(x => LangUrlSegments().Contains(x));
            if (cc != null)
                retval = LangDTO.FromCultureInfo(cc);
            return retval;
        }

        public static List<string> LangUrlSegments() => new List<string> { "es", "ca", "en", "pt" };
        public string Culture() => LangDTO.Tradueix(this, "es-ES", "ca", "en-US", "pt-PT");
        public string Culture2Digits() => LangDTO.Tradueix(this, "es", "ca", "en", "pt");



        public static LangDTO Default() => Esp();
        public static LangDTO Esp() => new LangDTO(Ids.ESP);
        public static LangDTO Cat() => new LangDTO(Ids.CAT);
        public static LangDTO Eng() => new LangDTO(Ids.ENG);
        public static LangDTO Por() => new LangDTO(Ids.POR);
        public bool IsEsp() => Id == Ids.ESP;
        public bool IsCat() => Id == Ids.CAT;
        public bool IsEng() => Id == Ids.ENG;
        public bool IsPor() => Id == Ids.POR;

        public string Tradueix(string? Esp = null, string? Cat = null, string? Eng = null, string? Por = null) => LangDTO.Tradueix(this, Esp, Cat, Eng, Por);
        public static string Tradueix(LangDTO lang, string? Esp = null, string? Cat = null, string? Eng = null, string? Por = null)
        {
            var retval = Esp ?? "";
            if (lang.Id == Ids.CAT && !string.IsNullOrEmpty(Cat))
                retval = Cat;
            else if (lang.Id == Ids.ENG && !string.IsNullOrEmpty(Eng))
                retval = Eng;
            else if (lang.Id == Ids.POR && !string.IsNullOrEmpty(Por))
                retval = Por;
            return retval;
        }

        public string? StringKey()
        {
            string? retval = null;
            switch (Id)
            {
                case Ids.ESP: retval = "Spanish"; break;
                case Ids.CAT: retval = "Catalan"; break;
                case Ids.ENG: retval = "English"; break;
                case Ids.POR: retval = "Portuguese"; break;
            }
            return retval;
        }
        public string Nom(LangDTO uxLang)
        {
            if (Id == Ids.CAT)
                return uxLang.Tradueix("Catalán", "Català", "Catalan", "Catalão");
            else if (Id == Ids.ENG)
                return uxLang.Tradueix("Inglés", "Anglès", "English", "Inglês");
            else if (Id == Ids.POR)
                return uxLang.Tradueix("Portugués", "Portuguès", "Portuguese", "Português");
            else
                return uxLang.Tradueix("Español", "Espanyol", "Spanish", "Espanhol");
        }
        public override string ToString() => Id.ToString();

        public bool Equals(LangDTO? candidate) => Id == candidate?.Id;

        public string UrlSegment() => Tradueix("es", "ca", "en", "pt");

        public CountryDTO DefaultCountry() => Id == Ids.POR ? CountryDTO.Portugal() : CountryDTO.Spain();

        public string MonthAbr(int month)
        {
            string[] esp = new string[] { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
            string[] cat = new string[] { "Gen", "Feb", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Oct", "Nov", "Dic" };
            string[] eng = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string[] por = new string[] { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
            string[] array = esp;
            switch (Id)
            {
                case Ids.CAT:
                    array = cat;
                    break;
                case Ids.ENG:
                    array = eng;
                    break;
                case Ids.POR:
                    array = por;
                    break;
                default:
                    array = esp;
                    break;
            }
            return array[month - 1];
        }

        public string Weekday(int base0Day)
        {
            string retval;

            //base0Day is 0=Sunday, 1=Monday, ... 6=Saturday
            var base1Day = base0Day == 0 ? 7 : base0Day;
            //base0Day is 1=Monday, 2=Tuesday, ... 7=Sunday

            //VbUtilities.Choose es base 1
            switch (Id)
            {
                case Ids.CAT:
                    {
                        retval = new[] { "Diumenge", "Dilluns", "Dimarts", "Dimecres", "Dijous", "Divendres", "Dissabte" }[base0Day];
                        break;
                    }

                case Ids.ENG:
                    {
                        retval = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" }[base0Day];
                        break;
                    }

                default:
                    {
                        retval = new[] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" }[base0Day];
                        break;
                    }
            }
            return retval;
        }

        public string Weekday(int year, int month, int day)
        {
            return Weekday(new DateTime(year, month, day));
        }
        public string Weekday(DateTime fch)
        {
            string[] esp = new string[] { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sábado", "Domingo" };
            string[] cat = new string[] { "Dilluns", "Dimarts", "Dimecres", "Dijous", "Divendres", "Dissabte", "Diumenge" };
            string[] eng = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string[] por = new string[] { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sábado", "Domingo" };
            string[] array = esp;
            switch (Id)
            {
                case Ids.CAT:
                    array = cat;
                    break;
                case Ids.ENG:
                    array = eng;
                    break;
                case Ids.POR:
                    array = por;
                    break;
                default:
                    array = esp;
                    break;
            }
            var msWeekday = (int)fch.DayOfWeek;
            int weekday = msWeekday == 0 ? 6 : msWeekday - 1;
            return array[weekday];
        }


        public static List<LangDTO> All()
        {
            var retval = new List<LangDTO>();
            retval.Add(Esp());
            retval.Add(Cat());
            retval.Add(Eng());
            retval.Add(Por());
            return retval;
        }

        public string PropertyPageUrl()
        {
            throw new NotImplementedException();
        }

        public bool Matches(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public static List<LangDTO> LangSet(string src)
        {
            var retval = new List<LangDTO>();
            if (src[0] == '1') retval.Add(Esp());
            if (src[1] == '1') retval.Add(Cat());
            if (src[2] == '1') retval.Add(Eng());
            if (src[3] == '1') retval.Add(Por());
            return retval;
        }

        public class Set
        {
            public string Value { get; set; }

            public Set()
            {
                Value = "0000";
            }

            public Set(string value)
            {
                Value = value;
            }

            public Set(bool esp, bool cat, bool eng, bool por)
            {
                var sb = new StringBuilder();
                sb.Append(esp ? "1" : "0");
                sb.Append(cat ? "1" : "0");
                sb.Append(eng ? "1" : "0");
                sb.Append(por ? "1" : "0");
                Value = sb.ToString();
            }

            public bool HasLang(LangDTO lang)
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
