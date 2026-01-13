using DTO;
using NuGet.Protocol;

namespace Api.Services
{
    public class MsgService
    {

        public static MsgModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Msgs
                    .Where(x => x.Guid == guid).Select(x => new MsgModel(guid) {
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
        public static List<MsgModel> All()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Msgs.Select(x => new MsgModel(x.Guid)
                    {
                        FchCreated = x.FchCreated,
                        UsrCreated = x.UsrCreated,
                        Content = x.Text
                    }).ToList();
            }

        }
    }
}
