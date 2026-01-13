using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Verifactu
{
    public class VfQrCodeResponse
    {
        public string? Return { get; set; }
        public int ResultCode { get; set; }
        public string? ResultMessage { get; set; }
    }
}
