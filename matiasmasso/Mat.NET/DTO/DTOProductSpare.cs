using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DTOProductSpare
    {
        public Guid Target { get; set; } //parent product
        public Guid Product { get; set; } //spare or accessory from target product
        public Cods Cod { get; set; }

        public enum Cods
        {
            NotSet,
            Accessories,
            Spares,
            Relateds
        }
    }
}
