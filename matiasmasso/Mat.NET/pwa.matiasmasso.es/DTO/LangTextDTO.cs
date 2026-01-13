using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DTO
{
    public class LangTextDTO
    {
        public string? Esp { get; set; }
        public string? Cat { get; set; }
        public string? Eng { get; set; }
        public string? Por { get; set; }

        public LangTextDTO() { }
        public LangTextDTO(string? esp = null, string? cat = null, string? eng = null, string? por = null)
        {
            Esp = esp;
            Cat = cat;
            Eng = eng;
            Por = por;
        }
        public static  LangTextDTO Factory (string? esp = null, string? cat = null, string? eng = null, string? por = null)
        {
            var retval = new LangTextDTO();
            retval.Esp = esp;
            retval.Cat = cat;
            retval.Eng = eng;
            retval.Por = por;
            return retval;
        }

        public string Tradueix(LangDTO lang)
        {
            return lang.Tradueix(Esp, Cat, Eng, Por);
        }

        public string? Text(LangDTO lang)
        {
            string? retval = null;
            if (lang.Id == LangDTO.Ids.ESP)
                retval = Esp;
            else if (lang.Id == LangDTO.Ids.CAT)
                retval = Cat;
            else if (lang.Id == LangDTO.Ids.ENG)
                retval = Eng;
            else if (lang.Id == LangDTO.Ids.POR)
                retval = Por;
            return retval;
        }

        public bool Contains(string searchTerm)
        {
            var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);

            if (Esp != null && searchTerms.All(x => Esp.Contains(x, System.StringComparison.CurrentCultureIgnoreCase)))
                return true;
            else if (Cat != null && searchTerms.All(x => Cat.Contains(x, System.StringComparison.CurrentCultureIgnoreCase)))
                return true;
            else if (Eng != null && searchTerms.All(x => Eng.Contains(x, System.StringComparison.CurrentCultureIgnoreCase)))
                return true;
            else if (Por != null && searchTerms.All(x => Por.Contains(x, System.StringComparison.CurrentCultureIgnoreCase)))
                return true;
            else
                return false;
        }

        public bool Matches(string? searchTerm)
        {
            if(string.IsNullOrEmpty(searchTerm))
                return false;
            else if (string.Equals(Esp, searchTerm, StringComparison.CurrentCultureIgnoreCase))
                return true;
            else if (string.Equals(Cat, searchTerm, StringComparison.CurrentCultureIgnoreCase))
                return true;
            else if (string.Equals(Eng, searchTerm, StringComparison.CurrentCultureIgnoreCase))
                return true;
            else if (string.Equals(Por, searchTerm, StringComparison.CurrentCultureIgnoreCase))
                return true;
            else
                return false;
        }



        public override string ToString()
        {
            return Esp ?? "LangTextDTO";
        }

    }
}
