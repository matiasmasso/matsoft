using System;

namespace DTO.Models.Compact
{
    public class Cca
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string Concept { get; set; }
        public DateTime Fch { get; set; }
        public Amt Eur { get; set; }
        public DocFile DocFile { get; set; }
        public UsrLog UsrLog { get; set; }
        public int Ccd { get; set; }

    }




}

