using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatHelper.Excel
{
    public class ValidationResult
    {
        public int Row { get; set; }
        public string Text { get; set; }

        public Cods Cod { get; set; }

        public enum Cods
        {
            Success,
            Fail
        }

        public static ValidationResult Factory(int row, Cods cod, string text)
        {
            ValidationResult retval = new ValidationResult();
            {
                var withBlock = retval;
                withBlock.Row = row;
                withBlock.Cod = cod;
                withBlock.Text = text;
            }
            return retval;
        }
    }

}


