using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class GuidNom : BaseGuid,  IModel
    {
        //public Guid Guid { get; set; }
        public string? Nom { get; set; }

        //public bool IsNew { get; set; }

        public GuidNom() : base()
        {
            //Guid = System.Guid.NewGuid();
            //IsNew = true;
        }

        public GuidNom(Guid guid, string? nom = null) : base(guid)
        {
            Nom = nom;
        }
        //public GuidNom(Guid guid, string? nom = null)
        //{
        //    this.Guid = guid;
        //    this.Nom = nom;
        //    IsNew = false;
        //}

        public static GuidNom? Factory(Guid? guid = null, string? nom = null)
        {
            GuidNom? retval = null;
            if (guid != null)
            {
                retval = new GuidNom((Guid)guid, nom);
            }
            return retval;
        }

        public new bool Matches(string? searchTerm)
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

        public string Caption() => Nom ?? "?";
        public override string ToString() => Nom ?? "Guidnom?";
    }
}
