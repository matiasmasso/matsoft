using System;

namespace DTO
{
    public class DTOTemplate : DTOBaseGuid
    {
        public string Nom { get; set; }
        public DTODocFile Docfile { get; set; }
        public bool Obsoleto { get; set; }

        public DTOTemplate() : base()
        {
        }

        public DTOTemplate(Guid oGuid) : base(oGuid)
        {
        }
    }

}
