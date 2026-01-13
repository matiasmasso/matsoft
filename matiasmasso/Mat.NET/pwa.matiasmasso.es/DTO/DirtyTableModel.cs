using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DirtyTableModel
    {
        public Ids Id { get; set; }
        public DateTime Fch { get; set; }
        public enum Ids
        {
            Pnc,
            Arc
        }
    }
}
