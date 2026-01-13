using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Text;

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

        public DirtyTableModel() { }
        public DirtyTableModel(Ids id, DateTime fch) {
            Id = id; 
            Fch = fch;
        }

        public static DirtyTableModel Factory(string table, DateTime fch)
        {
            DirtyTableModel retval = null;
            Ids id;
            if (Enum.TryParse<Ids>(table, true, out id))
                retval = new DirtyTableModel(id, fch);
            return retval;
        }
    }
}
