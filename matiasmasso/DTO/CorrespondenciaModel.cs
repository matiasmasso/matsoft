using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ContactCorrespondenciaModel
    {
        public List<CorrespondenciaModel> Items { get; set; } = new();
        public ContactModel? Contact { get; set; }

    }

    public class CorrespondenciaModel:BaseGuid
    {
        public EmpModel.EmpIds Emp { get; set; }
        public int Id { get; set; }
        public DateOnly? Fch { get; set; }
        public string? Subject { get; set; }

        public Rts Rt { get; set; } = Rts.Received;

        public List<Guid> Targets { get; set; } = new();
        public DocfileModel? Docfile { get; set; }

        public UsrLogModel? UsrLog { get; set; }

        public enum Rts
        {
            Received,
            Sent
        }

        public CorrespondenciaModel() : base() { }
        public CorrespondenciaModel(Guid guid) : base(guid) { }

        public string DownloadUrl() => (!string.IsNullOrEmpty(Docfile?.Hash) ? String.Format("correspondencia/pdf/{0}", Guid.ToString()):"");

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Subject;
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public override string ToString()
        {
            return Subject;
        }
    }
}
