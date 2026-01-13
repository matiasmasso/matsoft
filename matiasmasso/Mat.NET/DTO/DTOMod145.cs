using DTO.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DTOMod145:DTOBaseGuid
    {
        public GuidNom Titular { get; set; }
        public DateTime Fch { get; set; }
        public string Hash { get; set; }

        public DTOMod145() : base() { }
        public DTOMod145(Guid guid) : base(guid) { }

    }
}
