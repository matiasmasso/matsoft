using System;

namespace DTO
{
    public class DTODescatalogat : DTOGuidNom.Compact
    {
        public DateTime Fch { get; set; }
        public DTOGuidNom.Compact Brand { get; set; }
        public DTOGuidNom.Compact Category { get; set; }
        public int Id { get; set; }
        public string Ref { get; set; }
        public string Ean { get; set; }
        public DTODescatalogat Substitute { get; set; }
        public bool Confirmed { get; set; }
        public bool Warn { get; set; }

        public DTODescatalogat() : base() { }
        public DTODescatalogat(Guid guid) : base()
        {
            Guid = guid;
        }

    }
}
