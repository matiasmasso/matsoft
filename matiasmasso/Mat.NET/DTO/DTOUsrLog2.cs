using System;

namespace DTO
{
    public class DTOUsrLog2
    {
        public DTOGuidNom UsrCreated { get; set; }
        public DTOGuidNom UsrLastEdited { get; set; }
        public DateTime FchCreated { get; set; }
        public DateTime FchLastEdited { get; set; }

        public static DTOUsrLog2 Factory(DTOUser oUser = null)
        {
            DTOUsrLog2 retval = new DTOUsrLog2();
            if (oUser != null)
            {
                retval.UsrCreated = oUser.ToGuidNom();
                retval.UsrLastEdited = oUser.ToGuidNom();
            }
            retval.FchCreated = DTO.GlobalVariables.Now();
            retval.FchLastEdited = retval.FchCreated;
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
                if (UsrCreated == null)
                {
                    if (FchCreated != default(DateTime))
                        retval = string.Format("Registrat el {0:dd/MM/yy HH:mm}", FchCreated);
                }
                else
                {

                    if (FchCreated == default(DateTime))
                        retval = string.Format("Registrat per {0}", UsrCreated.Nom);
                    else
                        retval = string.Format("Registrat per {0} el {1:dd/MM/yy HH:mm}", UsrCreated.Nom, FchCreated);
                }
            }
            else if (FchLastEdited == default(DateTime))
            {
                if (UsrLastEdited == null)
                {
                    retval = string.Format("Registrat per {0} el {1:dd/MM/yy HH:mm}", UsrCreated.Nom, FchCreated);
                }
                else
                {
                    retval = string.Format("Registrat per {0} el {1:dd/MM/yy HH:mm}  i modificat per {2} ", UsrCreated.Nom, FchCreated, UsrLastEdited.Nom);
                }
            }


            else
            {
                if (UsrLastEdited == null)
                {
                    retval = string.Format("Registrat per {0} el {1:dd/MM/yy HH:mm}  i modificat el {2:dd/MM/yy HH:mm}", UsrCreated.Nom, FchCreated, FchLastEdited);
                }
                else
                {
                    if (UsrCreated == null)
                    {
                        retval = string.Format("Registrat el {0:dd/MM/yy HH:mm}  i modificat per {1} el {2:dd/MM/yy HH:mm}", FchCreated, UsrLastEdited.Nom, FchLastEdited);
                    }
                    else
                    {
                        retval = string.Format("Registrat per {0} el {1:dd/MM/yy HH:mm}  i modificat per {2} el {3:dd/MM/yy HH:mm}", UsrCreated.Nom, FchCreated, UsrLastEdited.Nom, FchLastEdited);
                    }

                }
            }
            return retval;
        }

        public static string logText(DTOUser oUserCreated, DTOUser oUserLastEdited = null, DateTime DtFchCreated = default(DateTime), DateTime DtFchLastEdited = default(DateTime))
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

        public static String UsrCreatedNom(DTOUsrLog2 src)
        {
            return src == null ? "" : src.UsrCreated == null ? "" : src.UsrCreated.Nom;
        }


    }
}

