using DTO;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Text;
using Api.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Api.Services
{
    public class LangTextService
    {
        //from other services using db
        public static LangTextModel Find(Entities.MaxiContext db, Guid guid, LangTextModel.Srcs src)
        {
            var entities = db.LangTexts
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
            if (value != null)
            {
                var entitiesToDelete = db.LangTexts.Where(x => x.Guid == value.Guid & x.Src == value.Src).ToList();
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
                        if (entity.FchCreated.Truncate(TimeSpan.TicksPerSecond) != ((DateTime)value.FchCreated!).Truncate(TimeSpan.TicksPerSecond))
                            throw new System.Exception("dirty value"){HResult = 555 };

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

        public static void Test()
        {
            //var pattern = @"<iframe.*?skucolors.*?><\/iframe>";
            var pattern = @"<iframe.*?\/plugin\/sku.*?><\/iframe>";
            var guidPattern = @"\s?[a-zA-Z0-9]{8}\s?[-]?\s?(?:[a-zA-Z0-9]{4}\s?[-]?\s?){3}\s?[a-zA-Z0-9]{12}\s?";
            var entities = SearchPattern("iframe");
            foreach (Entities.LangText entity in entities)
            {
                var text = entity.Text;
                Regex rg = new Regex(pattern);
                var snippets = rg.Matches(text);
                if (snippets.Count > 0)
                {
                    StringBuilder sb = new StringBuilder(text);
                    foreach (Match snippet in snippets)
                    {
                        Regex rgGuid = new Regex(guidPattern);
                        var guidMatch = rgGuid.Match(snippet.Value);
                        var sGuid = guidMatch.Value;
                        string spareSnippet = string.Format("<div data-ProductPlugin='{0}'></div>", sGuid);

                        //replace plugin markups
                        sb = sb.Replace(snippet.Value, spareSnippet);
                        string replacedText = sb.ToString();
                    }

                    entity.Text = sb.ToString();
                    Update(entity);
                }
            }
        }

        public static List<Entities.LangText> SearchPattern(string pattern)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.LangTexts.Where(x => EF.Functions.Like(x.Text, "%" + pattern + "%")).ToList();
            }
        }
    }

    public class LangTextsService
    {
        /// <summary>
        /// Returns all string resources except those related to outdated products
        /// </summary>
        /// <returns></returns>
        public static List<LangTextModel> Active()
        {
            using (var db = new Entities.MaxiContext())
            {
                return (from langtext in db.VwLangTexts
                        join product in db.VwProductGuids on langtext.Guid equals product.Guid into arts
                        from art in arts.DefaultIfEmpty()
                        where (art.Guid == null || art.Obsoleto == false)
                        select new LangTextModel
                        {
                            Guid = langtext.Guid,
                            Src = langtext.Src,
                            Esp = langtext.Esp,
                            Cat = langtext.Cat,
                            Eng = langtext.Eng,
                            Por = langtext.Por,
                            FchEsp = langtext.FchEsp,
                            FchCat = langtext.FchCat,
                            FchEng = langtext.FchEng,
                            FchPor = langtext.FchPor
                        }).ToList();
            };
        }
    }
}
