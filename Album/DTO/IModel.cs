using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public interface IModel
    {
        public Guid Guid { get; set; }
        public bool IsNew { get; set; }
        public bool Matches(string? searchTerm);
    }
}
