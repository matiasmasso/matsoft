using DTO;

namespace Api.Services
{
    public class CommentsServices
    {
        public static List<ContentModel.Comment> FromUser(Guid user)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.PostComments
                    .Where(x => x.User == user)
                    .Select(x => new ContentModel.Comment
                    {
                        Guid = x.Guid,
                        Fch = x.Fch,
                        Content =new ContentModel
                        {
                            Guid = x.Parent
                        },
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        Answering = x.Answering,
                        AnswerRoot = x.AnswerRoot,
                        FchApproved = x.FchApproved,
                        FchDeleted = x.FchDeleted,
                        Text= x.Text
                    }).ToList();
            }
        }
    }
}
