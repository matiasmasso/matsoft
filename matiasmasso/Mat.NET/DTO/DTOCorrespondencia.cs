using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCorrespondencia : DTOBaseGuid
    {
        public int Id { get; set; }
        public DTOEmp Emp { get; set; }
        public DateTime Fch { get; set; }
        public List<DTOContact> Contacts { get; set; }
        public string Subject { get; set; }
        public string Atn { get; set; }
        public Cods Cod { get; set; }
        public DTODocFile DocFile { get; set; }
        // Property userCreated As DTOUser
        // Property userLastEdited As DTOUser
        // Property fchCreated As DateTime
        // Property fchLastEdited As DateTime

        public DTOUsrLog2 UsrLog { get; set; }

        public enum Cods
        {
            NotSet,
            Rebut,
            Enviat
        }

        public DTOCorrespondencia() : base()
        {
            Contacts = new List<DTOContact>();
        }

        public DTOCorrespondencia(Guid oGuid) : base(oGuid)
        {
            Contacts = new List<DTOContact>();
        }

        public static DTOCorrespondencia Factory(DTOUser oUser, DTOContact oContact, DTOCorrespondencia.Cods oCod)
        {
            DTOCorrespondencia retval = new DTOCorrespondencia();
            {
                var withBlock = retval;
                withBlock.Emp = oUser.Emp;
                withBlock.Fch = DTO.GlobalVariables.Today();
                withBlock.Cod = oCod;
                withBlock.Contacts = new List<DTOContact>();
                withBlock.Contacts.Add(oContact);
                withBlock.UsrLog = DTOUsrLog2.Factory(oUser);
            }
            return retval;
        }

        public string Formatted()
        {
            string retval = string.Format("{0:0000}.{1:00000}", Fch.Year, Id);
            return retval;
        }
    }
}
