using Api.Entities;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{

    public class StringLocalizerService
    {
        public static bool Delete(string stringKey)
        {
            using (var db = new MaxiContext())
            {
                var itemsToDelete = db.StringLocalizers
                    .Where(x => x.StringKey == stringKey)
                    .ToList();

                db.StringLocalizers.RemoveRange(itemsToDelete);
                db.SaveChanges();
                return true;
            }
        }
    }

    public class StringsLocalizerService
    {
        public static List<StringLocalizerModel> GetValues()
        {
            using (var db = new MaxiContext())
            {
                return GetValues(db);
            }
        }
        public static List<StringLocalizerModel> GetValues(MaxiContext db)
        {
            var retval = new List<StringLocalizerModel>();
            var items = db.StringLocalizers
                        .AsNoTracking()
                .OrderBy(x => x.StringKey)
                .ThenBy(x => x.Lang)
                .ToList();

            StringLocalizerModel value = new();
            foreach (var item in items)
            {
                if (item.StringKey != value.StringKey)
                {
                    value = new StringLocalizerModel(item.StringKey);
                    retval.Add(value);
                }

                value.AddItem(item.Lang, item.Value);
            }

            return retval;
        }


        public static void Update(List<StringLocalizerModel> values)
        {
            using (var db = new MaxiContext())
            {
                var allEntities = db.StringLocalizers.ToList();
                foreach (var value in values)
                {
                    var entities = allEntities.Where(x => x.StringKey == value.StringKey);
                    foreach (var item in value.Items)
                    {
                        var entity = entities.FirstOrDefault(x => x.Lang == item.LangTag);
                        if (entity == null)
                            db.StringLocalizers.Add(new StringLocalizer
                            {
                                StringKey = value.StringKey,
                                Lang = item.LangTag!,
                                Value = item.Value!
                            });
                        else if (entity.Value != item.Value)
                            entity.Value = item.Value!;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
