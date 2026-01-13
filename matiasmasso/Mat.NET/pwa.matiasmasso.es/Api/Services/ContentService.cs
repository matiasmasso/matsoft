using DTO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Api.Services
{
    public class ContentService
    {
        public static ContentModel? FromUrlSegment(string urlSegment)
        {
            ContentModel? retval = null;
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.LangTexts
                .Where(x => x.Src == (int)LangTextModel.Srcs.ContentUrl && x.Text == urlSegment)
                .FirstOrDefault();

                if (entity != null)
                {
                    retval = new ContentModel(entity.Guid);
                    var entities = db.VwLangTexts
                    .Where(x => x.Guid == retval.Guid)
                    .ToList();
                    if (entities.Count() > 0)
                    {
                        var caption = entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ContentTitle);
                        if (caption != null)
                            retval.Caption.Load(caption.Esp, caption.Cat, caption.Eng, caption.Por);

                        var excerpt = entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ContentExcerpt);
                        if (excerpt != null)
                            retval.Excerpt.Load(excerpt.Esp, excerpt.Cat, excerpt.Eng, excerpt.Por);

                        var content = entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ContentText);
                        if (content != null)
                            retval.Content.Load(content.Esp, content.Cat, content.Eng, content.Por);

                        var url = entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ContentUrl);
                        if (url != null)
                            retval.UrlSegment.Load(url.Esp, url.Cat, url.Eng, url.Por);
                    }
                }
            }
            return retval;
        }
    }
}
