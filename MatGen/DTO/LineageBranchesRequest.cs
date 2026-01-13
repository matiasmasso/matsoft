using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class LineageBranchesRequest
    {
        public List<AncestorModel> Left { get; set; } = new();
        public List<AncestorModel> Right { get; set; } = new();
    }
}