using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CustomerPortfolioModel
    {
        public Guid Customer { get; set; }
        public Guid Product { get; set; }

        public int Cod { get; set; }
        public string? Obs { get; set; }
        public enum Cods
        {
            standard,
            exclusiva,
            noAplicable,
            exclos,
            distribuidorOficial,
            altresEnExclusiva
        }
    }
}
