using System;

namespace DTO
{
    public class DTODocFileSrc
    {
        public DTODocFile Docfile { get; set; }
        public DTOBaseGuid Src { get; set; }
        public DTODocFile.Cods Cod { get; set; }
        public DateTime Fch { get; set; }
        public string Nom { get; set; }
    }

}
