using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProjecteModel:BaseGuid, IModel
    {
        public EmpModel.EmpIds? Emp { get; set; }
        public string? Nom { get; set; }

        public ProjecteModel():base() { }
        public ProjecteModel(Guid guid):base(guid) { }

        public override string ToString()
        {
            return Nom ?? "?";
        }

        public GuidNom ToGuidNom()=>new GuidNom(Guid, ToString());

    }
}
