using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCondicio : DTOBaseGuid
    {
        public DTOLangText Title { get; set; }
        public DTOLangText Excerpt { get; set; }
        public Capitol.Collection Capitols { get; set; }
        public DTOUsrLog2 UsrLog { get; set; }

        public DTOCondicio() : base()
        {
            this.Capitols = new Capitol.Collection();
            this.Title = new DTOLangText(base.Guid, DTOLangText.Srcs.CondicioTitle);
            this.Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.CondicioText);
        }

        public DTOCondicio(Guid oGuid) : base(oGuid)
        {
            this.Capitols = new Capitol.Collection();
            this.Title = new DTOLangText(base.Guid, DTOLangText.Srcs.CondicioTitle);
            this.Excerpt = new DTOLangText(base.Guid, DTOLangText.Srcs.CondicioText);
        }

        public string Url(DTOLang lang, bool absoluteUrl)
        {
            string segment = lang.Tradueix("Condiciones", "Condicions", "Conditions", "Condições");
            string retval = lang.Domain(absoluteUrl).Url(segment);
            return retval;
        }
        public class Collection : List<DTOCondicio>
        {

        }

        public class Capitol : DTOBaseGuid
        {
            public DTOCondicio Parent { get; set; }
            public int Ord { get; set; }
            public DTOLangText Caption { get; set; }
            public DTOLangText Text { get; set; }
            public DTOUsrLog2 UsrLog { get; set; }

            public Capitol() : base()
            {
                this.Caption = new DTOLangText(base.Guid, DTOLangText.Srcs.CondicioCapitolTitle);
                this.Text = new DTOLangText(base.Guid, DTOLangText.Srcs.CondicioCapitolText);
                this.UsrLog = new DTOUsrLog2();
            }

            public Capitol(Guid oGuid) : base(oGuid)
            {
                this.Caption = new DTOLangText(base.Guid, DTOLangText.Srcs.CondicioCapitolTitle);
                this.Text = new DTOLangText(base.Guid, DTOLangText.Srcs.CondicioCapitolText);
                this.UsrLog = new DTOUsrLog2();
            }


            public class Collection : List<Capitol>
            {

            }

        }
    }

}
