using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ClaimModel : BaseGuid, IModel
    {
        public LangTextModel Nom { get; set; }
        public LangTextModel Description { get; set; }
        public Cods Cod { get; set; } = Cods.None;

        public ClaimModel():base()
        {
            Nom = new LangTextModel(base.Guid, LangTextModel.Srcs.ClaimNom);
            Description = new LangTextModel(base.Guid, LangTextModel.Srcs.ClaimDescription);
        }

        public ClaimModel(Guid guid):base(guid)
        {
            Nom = new LangTextModel(base.Guid, LangTextModel.Srcs.ClaimNom);
            Description = new LangTextModel(base.Guid, LangTextModel.Srcs.ClaimDescription);
        }

        public enum Cods
        {
            None = 0,
            SuperUser,
            Developer,
            MainBoard,
            Staff,
            Operadora,
            Marketing,
            Service,
            Accounts
        }

        public string PropertyPageUrl()
        {
            throw new NotImplementedException();
        }
    }
}
