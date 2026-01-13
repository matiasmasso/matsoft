using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class BaseGuidNom:BaseGuid
    {
        public string? Nom { get; set; }
        public BaseGuidNom() { }
        public BaseGuidNom(Guid guid, string? nom = null):base(guid)
        {
            Nom = nom;
        }
    }
    public class BaseGuid
    {
        private bool _isNew = true;
        public bool IsNew {
            get {
                return _isNew;
            }
            set
            {
                _isNew = value;
            }
        }
        private Guid _guid;
        public Guid Guid
        {
            get { 
                return _guid; 
            }
            set {
                _guid = value; 
                //IsNew = false; 
            }
        }


        public BaseGuid() : base()
        {
            _guid = System.Guid.NewGuid();
        }

        public BaseGuid(Guid oGuid) : base()
        {
            Guid = oGuid;
            _isNew = false;
        }

        public override bool Equals(object? candidate)
        {
            var retval = false;
            if (candidate != null && candidate is BaseGuid)
                retval = Guid.Equals(((BaseGuid)candidate).Guid);
            return retval;
        }

        public virtual bool Matches(string? searchTerm)
        {
            //this method should be overriden.
            //implemented here to appear as generic type member
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = this.ToString();
                retval = searchTerms.All(x => searchTarget!.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;

        }

        public GuidNom ToGuidnom() => new GuidNom(Guid, ToString());


        public override int GetHashCode()
        {
            return this.Guid.GetHashCode(); 
        }
    }
}
