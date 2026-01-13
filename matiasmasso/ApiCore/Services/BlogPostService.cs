using DTO;
using Api.Shared;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class BlogPostService
    {
        public static Byte[]? Thumbnail(Guid guid)
        {
            Byte[]? retval;
            using (var db = new Entities.MaxiContext())
            {
                retval = db.BlogPosts
                         .AsNoTracking()
                        .Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Thumbnail;
            }
            return retval;
        }
    }
    public class BlogPostsService
    {
        public static List<Box> ForLayout(Entities.MaxiContext db, UserModel? user, LangDTO lang)
        {
            var retval = (from post in db.BlogPosts
                          join title in db.VwLangTexts
                          on post.Guid equals title.Guid
                          join url in db.VwLangTexts
                          on post.Guid equals url.Guid
                          where title.Src == (int)LangTextModel.Srcs.BlogTitle
                          && url.Src == (int)LangTextModel.Srcs.BlogUrl
                          && post.Visible == true
                          orderby post.Fch descending
                          select new Box
                          {
                              Guid = post.Guid,
                              Caption = lang.Tradueix(title.Esp, title.Cat, title.Eng, title.Por),
                              Url = BlogPostDTO.RelativeUrl(lang.Tradueix(url.Esp, url.Cat, url.Eng, url.Por)),
                              ImageUrl = AppState.ApiUrl("blogpost/thumbnail", post.Guid.ToString())
                          }).AsNoTracking()
                          .Take(3)
                          .ToList();
            return retval;
        }

    }
}
