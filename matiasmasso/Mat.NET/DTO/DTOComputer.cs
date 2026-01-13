using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DTOComputer:DTOGuidNom
    {
        public string Text { get; set; }
        public DateTime? FchFrom { get; set; }
        public DateTime? FchTo { get; set; }

        public DTOComputer() : base() { }
        public DTOComputer(Guid guid, string nom="") : base(guid,nom) { }

        public string Url()
        {
            return MmoUrl.Factory(true, "Computer", Guid.ToString());
        }

        public string Html()
        {
            var retval = MatHelperStd.TextHelper.Html(Text);
            return retval;
        }

        public override string ToString()
        {
            return base.Nom ?? "{DTOComputer}";
        }

    }
}
