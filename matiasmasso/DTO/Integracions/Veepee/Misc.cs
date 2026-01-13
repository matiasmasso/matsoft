using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Veepee
{
    public class Misc
    {
        public static ContactModel Contact() => new ContactModel(new Guid("5C39587A-B0D8-4B4F-BEAA-D81C6AA5955F"));
    }
}
