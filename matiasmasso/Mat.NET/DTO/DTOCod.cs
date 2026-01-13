using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCod : DTOBaseGuid
    {
        public DTOCod Parent { get; set; }
        public int Id { get; set; }
        public DTOLangText Nom { get; set; }
        public DTOUsrLog2 UsrLog { get; set; }

        public String DisplayMember
        {
            get
            {
                string retval = "DTO.Cod";
                if (this.Nom != null)
                {
                    retval = this.Nom.Esp;
                }
                return retval;
            }

        }

        public enum Wellknowns
        {
            Incidencias,
            Logged,
            Read,
            SSCategories,
            BadMail
        }

        public DTOCod() : base()
        {
            this.Nom = new DTOLangText(this.Guid, DTOLangText.Srcs.Cods);
            this.UsrLog = new DTOUsrLog2();
        }
        public DTOCod(Guid guid) : base(guid)
        {
            this.Nom = new DTOLangText(this.Guid, DTOLangText.Srcs.Cods);
            this.UsrLog = new DTOUsrLog2();
        }

        public static DTOCod Factory(DTOUser user, DTOCod parent)
        {
            DTOCod retval = new DTOCod();
            retval.Parent = parent;
            retval.UsrLog = DTOUsrLog2.Factory(user);
            return retval;
        }

        public static DTOCod Wellknown(Wellknowns wellknown)
        {
            DTOCod retval = null;
            switch (wellknown)
            {
                case Wellknowns.Incidencias:
                    retval = new DTOCod(new Guid("A82017B7-2FC0-413F-8D94-A78DCB1668CC"));
                    break;
                case Wellknowns.Logged:
                    retval = new DTOCod(new Guid("197A92A3-BE60-4424-B694-38F394597DEF"));
                    retval.Nom.Load("Registro", "Registre", "Logged");
                    break;
                case Wellknowns.Read:
                    retval = new DTOCod(new Guid("01E914BE-4E94-49C7-A6EC-FE4607AAFA15"));
                    retval.Nom.Load("Leido", "Llegit", "Read");
                    break;
                case Wellknowns.SSCategories:
                    retval = new DTOCod(new Guid(""));
                    retval.Nom.Load("Leido", "Llegit", "Read");
                    break;
                case Wellknowns.BadMail:
                    retval = new DTOCod(new Guid("62E132A6-41FB-4F09-B312-03CE9A78E475"));
                    retval.Nom.Load("Motivos de devolución de emails", "Motius de devolució d'emails", "Reasons for returned emails");
                    break;
                default:
                    break;
            }
            return retval;
        }

        public new string ToString()
        {
            string retval = "DTO.Cod";
            if (this.Nom != null)
            {
                retval = this.Nom.Esp;
            }
            return retval;
        }

        public class Collection : List<DTOCod>
        {

        }

        public class Root : DTOCod
        {
            public Collection Items { get; set; }

            public Root() : base()
            {
                this.Items = new Collection();
            }
            public Root(Guid guid) : base(guid)
            {
                this.Items = new Collection();
            }

        }
    }
}
