using System;
using System.Collections.Generic;

namespace EdiHelperStd
{
    public class GLN
    {
        public List<Exception> Exs { get; set; }
        public string Value { get; set; }
        public bool IsValid { get; set; }
        public bool IsEmpty { get; set; }

        public GLN()
        {
            Exs = new List<Exception>();
            IsEmpty = true;
            IsValid = false;
        }

        public static GLN Factory(string src)
        {
            GLN retval = new GLN();
            string cleanDigits = EdiFile.CleanDigits(src);
            if (cleanDigits.Length == 13)
            {
                retval.Value = cleanDigits;
                retval.IsValid = true;
                retval.IsEmpty = false;
            }
            else
                retval.Exs.Add(new Exception(string.Format("Wrong GLN format at '{0}'", src)));
            return retval;
        }


    }
}
