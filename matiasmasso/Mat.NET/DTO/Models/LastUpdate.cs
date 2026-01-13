using System;

namespace DTO.Models
{
    public class LastUpdate
    {
        public ServerCache.Tables Table { get; set; }
        public DateTime Fch { get; set; }

        public LastUpdate()
        {
        }
        public LastUpdate(ServerCache.Tables table)
        {
            Fch = DateTime.MinValue;
            Table = table;
        }

        public override string ToString()
        {
            return string.Format("{0} {1:dd/MM/yy HH:mm:ss}", Table, Fch);
        }
    }

}
