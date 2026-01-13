using ApiCore.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Api.Services
{
    public class CommentService
    {
        public static ContentModel.Comment? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.PostComments
                   .Include(x => x.UserNavigation)
                   .AsNoTracking()
                   .Where(x => x.Guid == guid)
                    .Select(x => new ContentModel.Comment(x.Guid)
                    {
                        Fch = x.Fch,
                        Content = x.Parent == null ? null : new ContentModel
                        {
                            Guid = (Guid)x.Parent
                        },
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        Answering = x.Answering,
                        AnswerRoot = x.AnswerRoot,
                        FchApproved = x.FchApproved,
                        FchDeleted = x.FchDeleted,
                        Text = x.Text,
                        HasAnswers = x.InverseAnsweringNavigation.Any(),
                        User = new UserModel
                        {
                            Guid = x.User,
                            EmailAddress = x.UserNavigation.Adr,
                            Nickname = x.UserNavigation.Nickname,
                            Nom = x.UserNavigation.Nom,
                            Cognoms = x.UserNavigation.Cognoms
                        },
                    })
                    .FirstOrDefault();

                if (retval != null)
                {
                    //get the message it answers
                    retval.AnsweringMsg = retval.Answering == null ? null : db.PostComments
                     .Include(x => x.UserNavigation)
                     .AsNoTracking()
                     .Where(x => x.Guid == retval.Answering)
                    .Select(x => new ContentModel.Comment(x.Guid)
                    {
                        Fch = x.Fch,
                        Content = x.Parent == null ? null : new ContentModel
                        {
                            Guid = (Guid)x.Parent
                        },
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        Answering = x.Answering,
                        AnswerRoot = x.AnswerRoot,
                        FchApproved = x.FchApproved,
                        FchDeleted = x.FchDeleted,
                        Text = x.Text,
                        HasAnswers = x.InverseAnsweringNavigation.Any(),
                        User = new UserModel
                        {
                            Guid = x.User,
                            EmailAddress = x.UserNavigation.Adr,
                            Nickname = x.UserNavigation.Nickname,
                            Nom = x.UserNavigation.Nom,
                            Cognoms = x.UserNavigation.Cognoms
                        },
                    })
                     .FirstOrDefault();

                    //get all answers to this message
                    retval.Answers = db.PostComments
                    .Include(x => x.UserNavigation)
                    .AsNoTracking()
                        .Where(x => x.Answering == guid)
                        .OrderByDescending(x => x.Fch)
                    .Select(x => new ContentModel.Comment(x.Guid)
                    {
                        Fch = x.Fch,
                        Content = x.Parent == null ? null : new ContentModel
                        {
                            Guid = (Guid)x.Parent
                        },
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        Answering = x.Answering,
                        AnswerRoot = x.AnswerRoot,
                        FchApproved = x.FchApproved,
                        FchDeleted = x.FchDeleted,
                        Text = x.Text,
                        HasAnswers = x.InverseAnsweringNavigation.Any(),
                        User = new UserModel
                        {
                            Guid = x.User,
                            EmailAddress = x.UserNavigation.Adr,
                            Nickname = x.UserNavigation.Nickname,
                            Nom = x.UserNavigation.Nom,
                            Cognoms = x.UserNavigation.Cognoms
                        },
                    })
                        .ToList();
                    retval.HasAnswers = retval.Answers.Any();
                }
                return retval;
            }
        }

        public static bool Update(ContentModel.Comment value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.PostComment? entity;
                if (value.IsNew)
                {
                    entity = new Entities.PostComment();
                    db.PostComments.Add(entity);
                    entity.Guid = value.Guid;
                    entity.User = value.User!.Guid;
                    entity.Fch = value.Fch ?? DateTime.Now;
                }
                else
                    entity = db.PostComments.Find(value.Guid);

                if (entity == null) throw new Exception("Credencial not found");

                entity.Parent = value.Content?.Guid;
                entity.ParentSource = (int)value.ContentSrc;
                entity.Lang = value.Lang?.Tag() ?? "ESP";
                entity.Text = value.Text ?? "";
                entity.Answering = value.Answering;
                entity.AnswerRoot = value.AnswerRoot;
                entity.FchApproved = value.FchApproved;
                entity.FchDeleted = value.FchDeleted;

                // Save changes in database
                db.SaveChanges();
                return true;

            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.PostComments.Remove(db.PostComments.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;
        }

        public static ContentModel.Comment FromEntity(Entities.PostComment x)
        {
            return new ContentModel.Comment(x.Guid)
            {
                Fch = x.Fch,
                Content = x.Parent == null ? null : new ContentModel
                {
                    Guid = (Guid)x.Parent
                },
                Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                Answering = x.Answering,
                AnswerRoot = x.AnswerRoot,
                FchApproved = x.FchApproved,
                FchDeleted = x.FchDeleted,
                Text = x.Text,
                User = new UserModel
                {
                    Guid = x.User,
                    EmailAddress = x.UserNavigation?.Adr,
                    Nickname = x.UserNavigation?.Nickname,
                    Nom = x.UserNavigation?.Nom,
                    Cognoms = x.UserNavigation?.Cognoms
                },
                HasAnswers = x.InverseAnsweringNavigation.Any()
            };
        }

    }


    public class CommentsService
    {
        public static List<ContentModel.Comment> OpenFromSrc(int src)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.PostComments
                    .Include(x => x.UserNavigation)
                    .Where(x => x.ParentSource == src && x.Answering == null && !x.InverseAnsweringNavigation.Any())
                    .OrderByDescending(x => x.Fch)
                    .Select(x => new ContentModel.Comment(x.Guid)
                    {
                        Fch = x.Fch,
                        Content = x.Parent == null ? null : new ContentModel
                        {
                            Guid = (Guid)x.Parent
                        },
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        Answering = x.Answering,
                        AnswerRoot = x.AnswerRoot,
                        FchApproved = x.FchApproved,
                        FchDeleted = x.FchDeleted,
                        Text = x.Text,
                        HasAnswers = x.InverseAnsweringNavigation.Any(),
                        User = new UserModel
                        {
                            Guid = x.User,
                            EmailAddress = x.UserNavigation.Adr,
                            Nickname = x.UserNavigation.Nickname,
                            Nom = x.UserNavigation.Nom,
                            Cognoms = x.UserNavigation.Cognoms
                        },
                    })
                    .ToList();
            }
        }
        public static List<ContentModel.Comment> FromSrc(int src)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.PostComments
                    .Include(x => x.UserNavigation)
                    .Where(x => x.ParentSource == src)
                    .OrderByDescending(x => x.Fch)
                    .Select(x => new ContentModel.Comment(x.Guid)
                    {
                        Fch = x.Fch,
                        Content = x.Parent == null ? null : new ContentModel
                        {
                            Guid = (Guid)x.Parent
                        },
                        Lang = x.Lang == null ? null : new LangDTO(x.Lang),
                        Answering = x.Answering,
                        AnswerRoot = x.AnswerRoot,
                        FchApproved = x.FchApproved,
                        FchDeleted = x.FchDeleted,
                        Text = x.Text,
                        HasAnswers = x.InverseAnsweringNavigation.Any(),
                        User = new UserModel
                        {
                            Guid = x.User,
                            EmailAddress = x.UserNavigation.Adr,
                            Nickname = x.UserNavigation.Nickname,
                            Nom = x.UserNavigation.Nom,
                            Cognoms = x.UserNavigation.Cognoms
                        },
                    })
                    .ToList();
            }
        }

        public static List<ContentModel.Comment> Thread(Guid answering)
        {
            List<ContentModel.Comment> retval = new();
            //var allValues = FromSrc(src);
            //var root = ThreadRoot(answering, allValues);
            //var guids = new HashSet<Guid>();
            //guids.Add(answering);
            //guids.UnionWith(allValues.Where(x => x.Answering == answering).Select(x => x.Guid));
            //var root = allValues.FirstOrDefault(x => x.Guid == answering);
            //if (root.Answering != null)
            //    retval.Add()
            return retval;
        }

        private ContentModel.Comment? ThreadRoot(Guid candidate, List<ContentModel.Comment> allValues)
        {
            ContentModel.Comment? retval = allValues.FirstOrDefault(x => x.Guid == candidate);
            do
            {
                retval = allValues.FirstOrDefault(x => x.Guid == retval?.Answering);
            } while (retval?.Answering != null);
            return retval;
        }

        private void AddToThread(HashSet<Guid> thread, Guid guid, List<ContentModel.Comment> allValues)
        {
            foreach (var x in allValues.Where(x => x.Answering == guid))
            {
                if (thread.Add(x.Guid))
                    AddToThread(thread, x.Guid, allValues);
            }
        }

        public static List<ContentModel.Comment> FromUser(Guid user)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.PostComments
                    .Where(x => x.User == user)
                    .OrderByDescending(x => x.Fch)
                    .Select(x => CommentService.FromEntity(x))
                    .ToList();
            }
        }
    }
}
