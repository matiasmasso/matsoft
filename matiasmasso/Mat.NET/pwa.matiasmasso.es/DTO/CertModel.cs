using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CertModel
    {
        public string? Password  { get; set; }
        public DateOnly? FchTo { get; set; }

        public Media? Data { get; set; }
        public Media? Image { get; set; }

        public bool HasValue() => Data?.Data != null && !string.IsNullOrEmpty(Password);

    }
}
