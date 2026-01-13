using DTO;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Text;
using Api.Entities;

namespace Api.Services
{
    public class LangTextService
    {
        //from other services using db
        public static LangTextModel Find(Entities.MaxiContext db, Guid guid, LangTextModel.Srcs src)
        {
            var entities = db.LangTexts
                        .AsNoTracking()
                .Where(x => x.Guid == guid && x.Src == (int)src)
                .ToList();
            var retval = new LangTextModel(guid, src);
            retval.Esp = entities.FirstOrDefault(x => x.Lang == "ESP")?.Text;
            retval.Cat = entities.FirstOrDefault(x => x.Lang == "CAT")?.Text;
            retval.Eng = entities.FirstOrDefault(x => x.Lang == "ENG")?.Text;
            retval.Por = entities.FirstOrDefault(x => x.Lang == "POR")?.Text;
            return retval;
        }
        public static void Update(Entities.MaxiContext db, LangTextModel? value)
        {
            if (value != null && value.HasValue())
            {
                var entitiesToDelete = db.LangTexts.Where(x => x.Guid == value.Guid & x.Src == (int)value.Src).ToList();
                db.LangTexts.RemoveRange(entitiesToDelete);
                //db.SaveChanges();

                Create(db, value);
            }
        }

        public static DateTime? UpdateItem(LangTextModel.LangItem? value)
        {
            if (value != null)
            {
                using (var db = new Entities.MaxiContext())
                {
                    Entities.LangText? entity = db.LangTexts
                        .FirstOrDefault(x => x.Guid == value.Guid && x.Src == value.Src && x.Lang == value.Lang.Id.ToString());
                    if (entity == null)
                    {
                        entity = new Entities.LangText();
                        db.LangTexts.Add(entity);
                        entity.Guid = value.Guid;
                        entity.Src = (int)value.Src!;
                        entity.Lang = value.Lang!.Id.ToString();
                        entity.Text = value.Text;
                        entity.FchCreated = DateTime.Now;

                    }
                    else
                    {
                        value.FchCreated = DateTime.Now;
                        entity.Text = value.Text;
                        entity.FchCreated = (DateTime)value.FchCreated;
                    }

                    db.SaveChanges();
                }
            }
            return value?.FchCreated;
        }

        public static void Create(Entities.MaxiContext db, LangTextModel? value)
        {
            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.Esp))
                    db.LangTexts.Add(LangTextEntity(value, LangDTO.Esp()));
                if (!string.IsNullOrEmpty(value.Cat))
                    db.LangTexts.Add(LangTextEntity(value, LangDTO.Cat()));
                if (!string.IsNullOrEmpty(value.Eng))
                    db.LangTexts.Add(LangTextEntity(value, LangDTO.Eng()));
                if (!string.IsNullOrEmpty(value.Por))
                    db.LangTexts.Add(LangTextEntity(value, LangDTO.Por()));
            }
        }

        public static void Delete(Entities.MaxiContext db, Guid guid, LangTextModel.Srcs src)
        {
            var entities = db.LangTexts.Where(x => x.Guid.Equals(guid) && x.Src == (int)src).ToList();
            db.LangTexts.RemoveRange(entities);
        }

        public static void Update(Entities.LangText value)
        {
            using (var db = new Entities.MaxiContext())
            {
                // Retrieve entity by id
                var entity = db.LangTexts.Find(value.Pkey);

                // Validate entity is not null
                if (entity != null)
                {
                    // Make changes on entity
                    entity.Text = value.Text;

                    // Save changes in database
                    db.SaveChanges();
                }
            }
        }


        public static Entities.LangText LangTextEntity(LangTextModel value, LangDTO lang)
        {
            return new Entities.LangText()
            {
                Guid = (Guid)value.Guid!,
                Src = (int)value.Src!,
                Lang = lang.ToString(),
                Text = value.Text(lang)
            };
        }

        public static List<Entities.LangText> SearchPattern(string pattern)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.LangTexts
                    .AsNoTracking()
                    .Where(x => EF.Functions.Like(x.Text, "%" + pattern + "%")).ToList();
            }
        }
    }

    public class LangTextsService
    {
        public static List<LangTextModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.VwLangTexts
                        .AsNoTracking()
                    .Select(x => new LangTextModel(x.Guid,(LangTextModel.Srcs)x.Src)
                    {
                        Esp = x.Esp,
                        Cat = x.Cat,
                        Eng = x.Eng,
                        Por = x.Por,
                        FchEsp = x.FchEsp,
                        FchCat = x.FchCat,
                        FchEng = x.FchEng,
                        FchPor = x.FchPor
                    }).ToList();
            }
        }
    }
}
