using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ContactDocModel:BaseGuid
    {
        public Guid? Target { get; set; }
        public Cods Cod { get; set; }
        public string? Ref { get; set; }
        public DateOnly? Fch { get; set; }
        public bool Obsoleto { get; set; }
        public DocfileModel? Docfile { get; set; }

        public ContactDocModel():base() { }
        public ContactDocModel(Guid guid):base(guid) { }

        public enum Cods
        {
            None = 0,
        }
    }
}
