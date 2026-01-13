using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CitaModel: BaseGuid, IModel
    {
        public Guid? Pub {  get; set; }
        public string? Author { get; set; }
        public int? Year { get; set; }
        public string? Title { get; set; }
        public string? Pags { get; set; }
        public string? Container { get; set; }
        public string? Url { get; set; }
        public string? Content { get; set; }
        public DateTime? FchCreated { get; set; }
        public CitaModel() : base() { }
        public CitaModel(Guid guid) : base(guid) { }

        public string Caption()
        {
            return ToString() ?? "?";
        }

        public string PropertyPageUrl()
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

        public override string ToString()
        {
            return Title ?? "?";
        }
    }
}
