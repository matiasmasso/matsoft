using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SkuPncModel
    {
        public Guid Sku { get; set; }
        public int Clients { get; set; } // totes les unitats pendents de servir a clients
        public int AlPot { get; set; } // unitats de comandes en standby a la espera indefinida de confirmació
        public int EnProgramacio { get; set; } // unitats en programació a mes de una setmana vista
        public int BlockStock { get; set; } // unitats de comandes amb stock reservat
        public int Proveidors { get; set; }
    }
}
