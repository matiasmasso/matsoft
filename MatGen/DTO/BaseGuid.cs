using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BaseGuid
    {
        public bool IsNew { get; set; } = true;

        public Guid Guid { get; set; } = System.Guid.NewGuid();

        public BaseGuid() { }
        public BaseGuid(Guid guid)
        {
            Guid = guid;
            IsNew = false;
        }
    }

    public class GuidNom
    {
        public Guid Guid { get; set; }
        public string? Nom { get; set; }

        public GuidNom(Guid guid, string? nom)
        {
            Guid = guid;
            Nom = nom;
        }
    }
}
