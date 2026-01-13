using MatHelperStd;
using System;

namespace DTO
{
    public class DTOCategoriaDeNoticia : DTOBaseGuid
    {
        public string Nom { get; set; }
        public string Excerpt { get; set; }
        public DateTime FchLastEdited { get; set; }

        public DTOCategoriaDeNoticia() : base()
        {
        }

        public DTOCategoriaDeNoticia(Guid oGuid) : base(oGuid)
        {
        }

        public new string ToString()
        {
            return Nom;
        }

        public string Url(DTOWebDomain domain)
        {
            if (domain == null) domain = DTOWebDomain.Default();
            string retval = domain.Url("noticias", TextHelper.RemoveDiacritics(this.Nom));
            return retval;
        }

    }
}
