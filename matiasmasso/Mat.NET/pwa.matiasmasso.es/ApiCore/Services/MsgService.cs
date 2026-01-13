using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class MsgService
    {

        public static MsgModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Msgs
                        .AsNoTracking()
                        .Where(x => x.Guid == guid).Select(x => new MsgModel(guid)
                        {
                            Src = (MsgModel.Srcs)x.Src,
                            FchCreated = x.FchCreated,
                            UsrCreated = x.UsrCreated,
                            Content = x.Text
                        }).FirstOrDefault();
            }
        }

        public static bool Update(MsgModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.Msg? entity;
                if (value.IsNew)
                {
                    //EmailMessageService.Send("matias@matiasmasso.es", "Nou missatge desde la App", value.Content ?? "");
                    entity = new Entities.Msg();
                    db.Msgs.Add(entity);
                    entity.Guid = guid;
                    entity.Id = LastId(db) + 1;
                }
                else
                    entity = db.Msgs.Find(guid);

                if (entity == null) throw new Exception("Msg not found");

                entity.FchCreated = value.FchCreated;
                entity.UsrCreated = value.UsrCreated;
                entity.Text = value.Content ?? "";
                entity.Src = (int)value.Src;

                db.SaveChanges();
                return true;
            }
        }

        private static int LastId(Entities.MaxiContext db)
        {
            return db.Msgs.Max(x => (int?)x.Id) ?? 0;
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                db.Msgs.RemoveRange(db.Msgs.Where(x => x.Guid == guid));
                db.SaveChanges();
            }
            return true;

        }

    }

    public class MsgsService
    {
        public static List<MsgModel> All(int? src = null)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Msgs
                        .AsNoTracking()
                        .Where(x => src == null ? true : x.Src == src)
                        .Select(x => new MsgModel(x.Guid)
                        {
                            Id = x.Id ?? 0,
                            Src = (MsgModel.Srcs)x.Src,
                            Content = x.Text,
                            UsrLog = new UsrLogModel
                            {
                                FchCreated = x.FchCreated,
                                UsrCreated = new GuidNom(x.UsrCreated, UserModel.DisplayNom(x.UsrCreatedNavigation.Adr, x.UsrCreatedNavigation.Nickname))
                            }
                        }).ToList();
            }

        }
    }
}
