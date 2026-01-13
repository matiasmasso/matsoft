using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class EscripturaModel:BaseGuid, IModel
    {
        public EmpModel.EmpIds Emp { get; set; }
        public string? Nom { get; set; }
        public Guid? Notari { get; set; }
        public Guid? RegistreMercantil { get; set; }
        public DateOnly? FchFrom { get; set; }
        public DateOnly? FchTo { get; set; }
        public int? NumProtocol { get; set; }
        public int? Tomo { get; set; } 
        public int? Folio { get; set; }
        public string? Hoja { get; set; } 
        public int? Inscripcio { get; set; }

        public DocfileModel? Docfile { get; set; }


        public EscripturaModel():base() { }
        public EscripturaModel(Guid guid):base(guid){ }

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nom ?? "";
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public bool IsOutdated()
        {
            var retval = false;
            if(FchTo != null && FchTo< DateOnly.FromDateTime(DateTime.Now)) retval= true;
            return retval;
        }
        public string DownloadUrl() => Globals.ApiUrl("escriptura/pdf", Guid.ToString());
        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());
        public string Caption() => string.Format("{0:dd/MM/yy} {1}", FchFrom, Nom);
        public override string ToString() => Caption();

    }
}
