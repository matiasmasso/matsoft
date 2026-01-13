using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class SessionDTO
    {
        private LangDTO? _lang = null;
        public string? CultureName { get; set; }
        public LangDTO Lang { 
            get {
                if (_lang == null) _lang = LangDTO.FromCultureInfo(CultureName);
                return _lang;
            }
            set {
                _lang = value;
            }
        }

        public static SessionDTO Factory(string cultureName)
        {
            var retval = new SessionDTO();
            retval.CultureName = cultureName;
            return retval;
        }

    }
}
