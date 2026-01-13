using System;

namespace DTO.Models.Base
{
    public class GuidNom
    {
        public Guid Guid { get; set; }
        public string Nom { get; set; }

        public GuidNom() { }
        public GuidNom(Guid guid, string nom = "")
        {
            Guid = guid;
            Nom = nom;
        }
    }
}
