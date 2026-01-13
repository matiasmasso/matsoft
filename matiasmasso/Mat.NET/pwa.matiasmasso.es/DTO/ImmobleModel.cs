using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ImmobleModel:BaseGuid, IModel
    {
        public EmpModel.EmpIds? Emp { get; set; }
        public string? Nom { get; set; }
        public string? Cadastre { get; set; }
        public string? Registre { get; set; }
        public AddressModel? Address { get; set; }
        public DateOnly? FchFrom { get; set; }
        public DateOnly? FchTo { get; set; }

        public int? Titularitat { get; set; }
        public decimal? Part { get; set; }

        public string? Obs { get; set; }
        public List<DocfileSrcModel> Docfiles { get; set; } = new();
        public List<InventariItem> Inventari { get; set; } = new();


        public ImmobleModel() : base() { }
        public ImmobleModel(Guid guid) : base(guid) { }

        public string Url() => String.Format("immoble/{0}", Guid.ToString());

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nom + " " + Obs;
                retval = searchTerms.All(x => searchTarget!.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());
        public string Caption()
        {
            return Nom ?? "{ImmobleModel}";
        }
        public override string ToString()
        {
            return Nom ?? "{ImmobleModel}";
        }

        public new GuidNom ToGuidnom() => new GuidNom(Guid, ToString());


        public class InventariItem:BaseGuid
        {
            public Guid? Target { get; set; }
            public string? Nom { get; set; }
            public string? Obs { get; set; }

            public List<DocfileSrcModel> Docfiles { get; set; } = new();


            public InventariItem() : base() { }
            public InventariItem(Guid guid) : base(guid) { }

            public string PageUrl() => Globals.PageUrl("PgInventariItem", Guid.ToString());
            public override string ToString()
            {
                return Nom ?? "?";
            }

        }

        public NavDTO ContextMenu()
        {
            var retval = new NavDTO();
            retval.AddItem(Globals.PageUrl("PgImmoble", Guid.ToString()), "Ficha", "Fitxa", "Properties");
            retval.AddAction(Globals.PageUrl("CopyRef", Guid.ToString()), "Copiar ref.catastral", "Copiar ref.cadastral", "Copy reference");
            return retval;
        }
    }
}
