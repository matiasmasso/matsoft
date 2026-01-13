using System;

namespace DTO
{
    public class DTOUsrLog
    {
        public DTOUser UsrCreated { get; set; }
        public DTOUser UsrLastEdited { get; set; }
        public DateTime FchCreated { get; set; }
        public DateTime FchLastEdited { get; set; }

        public static DTOUsrLog Factory(DTOUser oUser = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOUsrLog retval = new DTOUsrLog();
            {
                var withBlock = retval;
                withBlock.UsrCreated = oUser;
                withBlock.UsrLastEdited = oUser;
            }
            return retval;
        }

        public string text()
        {
            string retval = "";
            var sameUser = UsrCreated != null && UsrCreated.Equals(UsrLastEdited);
            var sameFch = Math.Abs((FchCreated - FchLastEdited).TotalSeconds) < 2;
            var sameUserFch = sameUser & sameFch;
            if (UsrLastEdited == null | sameUserFch)
            {
                if (FchCreated == default(DateTime))
                    retval = string.Format("Registrat per {0}", DTOUser.NicknameOrElse(UsrCreated));
                else
                    retval = string.Format("Registrat per {0} el {1:dd/MM/yy HH:mm}", DTOUser.NicknameOrElse(UsrCreated), FchCreated);
            }
            else if (FchLastEdited == default(DateTime))
                retval = string.Format("Registrat per {0} el {1:dd/MM/yy HH:mm}  i modificat per {2} ", DTOUser.NicknameOrElse(UsrCreated), FchCreated, DTOUser.NicknameOrElse(UsrLastEdited));
            else
                retval = string.Format("Registrat per {0} el {1:dd/MM/yy HH:mm}  i modificat per {2} el {3:dd/MM/yy HH:mm}", DTOUser.NicknameOrElse(UsrCreated), FchCreated, DTOUser.NicknameOrElse(UsrLastEdited), FchLastEdited);
            return retval;
        }

        public static string logText(DTOUser oUserCreated, DTOUser oUserLastEdited = null/* TODO Change to default(_) if this is not a reference type */, DateTime DtFchCreated = default(DateTime), DateTime DtFchLastEdited = default(DateTime))
        {
            var oUsrLog = DTOUsrLog.Factory(oUserCreated);
            {
                var withBlock = oUsrLog;
                withBlock.UsrLastEdited = oUserLastEdited;
                withBlock.FchCreated = DtFchCreated;
                withBlock.FchLastEdited = DtFchLastEdited;
            }
            return oUsrLog.text();
        }

        public DTOTracking Tracking()
        {
            DTOTracking retval = DTOTracking.Factory(DTOCod.Wellknowns.Logged, this.UsrCreated);
            retval.UsrLog.FchCreated = this.FchCreated;
            return retval;
        }
    }
}
