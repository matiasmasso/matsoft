using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class UsrLogModel
    {
        public GuidNom? UsrCreated { get; set; }
        public GuidNom? UsrLastEdited { get; set; }
        public DateTime? FchCreated { get; set; }
        public DateTime? FchLastEdited { get; set; }

        public UsrLogModel() { }

        public static UsrLogModel Factory(UserModel? user)
        {
            var now = DateTime.Now;
            var usr = user == null ? null : new GuidNom(user.Guid, user.NomiCognomsOrNickname());
            return new UsrLogModel
            {
                UsrCreated = usr,
                FchCreated = now,
                UsrLastEdited = usr,
                FchLastEdited = now
            };
        }

        public bool IsEdited()
        {
            var retval = false;
            if(FchLastEdited != null)
            {
                TimeSpan ts = (DateTime)FchLastEdited - (DateTime)FchCreated!;
                retval = ts.Minutes > 1;
            }
            return retval;
        }

        public string Caption(LangDTO lang)
        {
            string retval = string.Empty;
            switch(lang.Id)
            {
                case LangDTO.Ids.CAT:
                    retval = string.Format("Registrat el {0:dd/MM/yy} a les {0:HH:mm} per {1}", FchCreated, UsrCreated?.Nom ?? "?");
                    if (IsEdited())
                    {
                        if (((DateTime)FchCreated!).Date == ((DateTime)FchLastEdited!).Date)
                            retval += string.Format("  i modificat a les {0:HH:mm}", FchLastEdited);
                        else
                            retval += string.Format("  i modificat el {0:dd/MM/yy} a les {0:HH:mm}", FchLastEdited);
                        if(UsrCreated!.Guid == UsrLastEdited!.Guid)
                            retval += " per ell/a mateix/a";
                        else
                            retval += string.Format(" per {0}", UsrLastEdited.Nom);
                    }
                    break;
                case LangDTO.Ids.ENG:
                    retval = string.Format("Registered on {0:dd/MM/yy} at {0:HH:mm} by {1}", FchCreated, UsrCreated?.Nom ?? "?");
                    if (IsEdited())
                    {
                        if (((DateTime)FchCreated!).Date == ((DateTime)FchLastEdited!).Date)
                            retval += string.Format(" and modified at {0:HH:mm}", FchLastEdited);
                        else
                            retval += string.Format(" and modified on {0:dd/MM/yy} at {0:HH:mm}", FchLastEdited);
                        if (UsrCreated!.Guid == UsrLastEdited!.Guid)
                            retval += " by same user";
                        else
                            retval += string.Format(" by {0}", UsrLastEdited.Nom);
                    }
                    break;
                case LangDTO.Ids.POR:
                default:
                    retval = string.Format("Registrado el {0:dd/MM/yy} a las {0:HH:mm} por {1}", FchCreated, UsrCreated?.Nom ?? "?");
                    if (IsEdited())
                    {
                        if (((DateTime)FchCreated!).Date == ((DateTime)FchLastEdited!).Date)
                            retval += string.Format(" y modificado a las {0:HH:mm}", FchLastEdited);
                        else
                            retval += string.Format(" y modificado el {0:dd/MM/yy} a las {0:HH:mm}", FchLastEdited);
                        if (UsrCreated!.Guid == UsrLastEdited!.Guid)
                            retval += " por el/la mismo/a usuario/a";
                        else
                            retval += string.Format(" por {0}", UsrLastEdited.Nom);
                    }
                    break;
            }
            return retval;
        }
    }
}
