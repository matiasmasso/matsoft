using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOCredencial : DTOBaseGuid
    {
        public string Referencia { get; set; }
        public string Url { get; set; }
        public string Usuari { get; set; }
        public string Password { get; set; }
        public string Obs { get; set; }

        public DTOUsrLog UsrLog { get; set; }

        public List<DTORol> Rols { get; set; }
        public List<DTOUser> Owners { get; set; }

        public enum Wellknowns
        {
            Notset,
            AmazonSeller
        }

        public DTOCredencial() : base()
        {
            Owners = new List<DTOUser>();
            Rols = new List<DTORol>();
            UsrLog = new DTOUsrLog();
        }

        public DTOCredencial(Guid oGuid) : base(oGuid)
        {
            Owners = new List<DTOUser>();
            Rols = new List<DTORol>();
            UsrLog = new DTOUsrLog();
        }

        public static DTOCredencial Factory(DTOUser oUser)
        {
            DTOCredencial retval = new DTOCredencial();
            {
                var withBlock = retval;
                withBlock.UsrLog = DTOUsrLog.Factory(oUser);
                withBlock.Owners.Add(oUser);
            }
            return retval;
        }

        public static DTOCredencial Wellknown(Wellknowns id)
        {
            DTOCredencial retval = null;
            switch (id)
            {
                case Wellknowns.AmazonSeller:
                    {
                        retval = new DTOCredencial(new Guid("78157745-8C32-4DDC-BC54-789620728996"));
                        break;
                    }
            }
            return retval;
        }

        public ViewModel Model()
        {
            DTOCredencial.ViewModel retval = new DTOCredencial.ViewModel();
            {
                var withBlock = retval;
                withBlock.Guid = base.Guid;
                withBlock.Referencia = Referencia;
                withBlock.Url = Url;
                withBlock.Usuari = Usuari;
                withBlock.Password = Password;
                withBlock.Obs = Obs;
                withBlock.Owners = Owners.Select(x => x.Guid).ToList();
                withBlock.IsNew = base.IsNew;
            }
            return retval;
        }

        public class ViewModel
        {
            public Guid Guid { get; set; }
            public string Referencia { get; set; }
            public string Url { get; set; }
            public string Usuari { get; set; }
            public string Password { get; set; }
            public string Obs { get; set; }
            public List<Guid> Owners { get; set; }
            public bool IsNew { get; set; }
        }
    }
}
