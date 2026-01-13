using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TemplateModel : BaseGuid, IModel
    {
        public string Caption()
        {
            throw new NotImplementedException();
        }

        public string CreatePageUrl()
        {
            throw new NotImplementedException();
        }

        public bool Matches(string? searchTerm)
        {
            throw new NotImplementedException();
        }

        public string PropertyPageUrl()
        {
            throw new NotImplementedException();
        }
    }
}
