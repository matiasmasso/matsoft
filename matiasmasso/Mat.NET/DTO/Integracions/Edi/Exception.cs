using System;

namespace DTO.Integracions.Edi
{
    public class Exception
    {
        public Guid Guid { get; set; }
        public DTOEdiversaFile.Tags FileTag { get; set; }
        public Guid FileGuid { get; set; }
        public Guid DocGuid { get; set; }
        public string DocNum { get; set; }
        public DateTime Fch { get; set; }
        public int Cod { get; set; }
        public string Msg { get; set; }
        public string Tag { get; set; }
        public int TagCod { get; set; }

    }
}
