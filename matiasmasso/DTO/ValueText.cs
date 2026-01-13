using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ValueText
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public ValueText(string value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}
