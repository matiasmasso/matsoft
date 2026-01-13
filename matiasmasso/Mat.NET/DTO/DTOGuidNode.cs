using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOGuidNode : DTOGuidNom
    {
        public List<DTOGuidNode> Items { get; set; }
        public DTOGuidNode(Guid oGuid, string sNom = "") : base(oGuid, sNom)
        {
            Items = new List<DTOGuidNode>();
        }
    }
}
