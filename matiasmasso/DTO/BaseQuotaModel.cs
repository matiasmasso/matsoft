using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BaseQuotaModel
    {
        public decimal? Base { get; set; }
        public decimal? Tipo { get; set; }
        public decimal? Quota { get; set; }

        public decimal? Total => (Base ?? 0)+(Quota ?? 0);

        public bool IsEmpty() => (Base == null || Base == 0);
        public bool IsExenta() => (!IsEmpty() && (Tipo == null || Tipo == 0));
        public bool IsSujeta() => (!IsEmpty() && Tipo != null && Tipo != 0);

    }
}
