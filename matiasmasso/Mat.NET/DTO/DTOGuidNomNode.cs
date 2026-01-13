using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOGuidNomNode : DTOGuidNom
    {
        public DTOGuidNomNode Parent { get; set; }
        public List<DTOGuidNomNode> Children { get; set; } = new List<DTOGuidNomNode>();

        public DTOGuidNomNode() : base()
        {
        }

        public DTOGuidNomNode(Guid oGuid) : base(oGuid)
        {
        }

        public void AddChild(DTOGuidNomNode value)
        {
            value.Parent = this;
            Children.Add(value);
        }
    }
}
