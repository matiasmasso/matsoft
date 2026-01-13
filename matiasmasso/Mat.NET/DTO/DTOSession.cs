using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOSession : DTOBaseGuid
    {
        public DTOApp.AppTypes AppType { get; set; }
        public string AppVersion { get; set; }
        public string MinVersion { get; set; }
        public string LastVersion { get; set; }
        //public List<DTOEmp> Emps { get; set; }
        public DTOEmp Emp { get; set; }
        public DTOCur Cur { get; set; }
        public DTOLang Lang { get; set; }
        public DTORol Rol { get; set; }
        public DTOUser User { get; set; }
        public DTOContact Contact { get; set; }
        public bool IsAuthenticated { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public string Culture { get; set; }

        public DTOIncidencia Incidencia { get; set; }

        public const string CookieSessionNameBackup = "UserSessionBackup";
        public const string CookieSessionName = "UserSession";
        public const string CookiePersistName = "UserPersist";
        public const string CookiesAccepted = "CookiesAccepted";

        public enum CookieIds
        {
            None,
            UserSession,
            UserPersist,
            CookiesAccepted,
            LastProductBrowsed,
            LastSessionStart,
            LastSessionEnd
        }


        public enum Settings
        {
            none,
            User_Persisted,
            FrmIdx_Width,
            FrmIdx_Height,
            FrmIdx_Splitter,
            Last_Menu_Selection,
            Last_Balance_Fch,
            Last_Product_Selected,
            Last_WinMenuItems_SpriteHash
        }

        public DTOSession() : base()
        {
        }

        public DTOSession(Guid oGuid) : base(oGuid)
        {
        }

        public DTOSession(DTOEmp.Ids EmpId) : base()
        {
            Emp = new DTOEmp(EmpId);
        }

        public static DTOSession Factory(string sCulture, DTOUser oUser = null/* TODO Change to default(_) if this is not a reference type */)
        {
            // requires loaded User
            List<Exception> exs = new List<Exception>();
            DTOSession retval = new DTOSession();
            {
                var withBlock = retval;
                withBlock.Culture = sCulture;
                withBlock.FchFrom = DTO.GlobalVariables.Now();
                if (oUser == null)
                {
                    withBlock.Rol = new DTORol(DTORol.Ids.unregistered);
                    withBlock.Lang = DTOLang.FromCulture(sCulture);
                }
                else
                {
                    withBlock.User = oUser;
                    withBlock.Rol = oUser.Rol;
                    withBlock.Lang = oUser.Lang;
                }
            }
            return retval;
        }



        public static bool GetIsAuthenticated(DTOSession oSession)
        {
            bool retval = false;
            if (oSession != null)
            {
                DTOUser oUser = oSession.User;
                DTORol oRol = oUser.Rol;
                retval = oRol.isAuthenticated();
            }
            return retval;
        }

        public string Tradueix(string Esp, string Cat, string Eng, string Por = "")
        {
            string retval = Esp;
            if (Lang != null)
                retval = Lang.Tradueix(Esp, Cat, Eng, Por);
            return retval;
        }

        public void LogOff()
        {
            IsAuthenticated = false;
            Incidencia = null;
        }
    }
}