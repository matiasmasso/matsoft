using DTO;
using Api.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Api.Entities;
using DocumentFormat.OpenXml.Spreadsheet;
using System;

namespace Api.Services
{
    public class NoticiaService
    {

        //public static NoticiaModel? LandingPage(string urlSegment)
        //{
        //    var cache = CacheService.CatalogRequest(CacheDTO.Table.TableIds.Noticia);
        //    var retval = cache.Noticias.FirstOrDefault(x => x.UrlSegment.Matches(urlSegment));
        //    if (retval != null)
        //    {
        //        using (var db = new Entities.MaxiContext())
        //        {
        //            retval = Load(db, retval);
        //            //var rawContent = Load(db,  retval)?.Content.Tradueix(lang);
        //            //var htmlContent = rawContent?.Html();
        //            //retval.Content = cache.ExpandPlugins(htmlContent, lang);
        //        }
        //    }
        //    return retval;

        //}

        public static NoticiaModel? Load(Entities.MaxiContext db, NoticiaModel? model)
        {
            var retval = model;
            if (model != null)
            {
                var entities = db.VwLangTexts
                        .AsNoTracking()
                .Where(x => x.Guid == model.Guid)
                .ToList();
                if (entities.Count() > 0)
                {
                    var excerpt = entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ContentExcerpt);
                    if (excerpt != null)
                        retval!.Excerpt.Load(excerpt.Esp, excerpt.Cat, excerpt.Eng, excerpt.Por);

                    var content = entities.FirstOrDefault(x => x.Src == (int)LangTextModel.Srcs.ContentText);
                    if (content != null)
                        retval!.Content.Load(content.Esp, content.Cat, content.Eng, content.Por);
                }
            }
            return retval;
        }


        public static Byte[]? Thumbnail(Guid guid)
        {
            Byte[]? retval;
            using (var db = new Entities.MaxiContext())
            {
                retval = db.Noticia.Where(x => x.Guid.Equals(guid)).FirstOrDefault()?.Image265x150;
            }
            return retval;
        }
    }
    public class NoticiasService
    {
        public static List<NoticiaModel> All(UserModel? user, LangDTO lang)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = All(db, user);

                return retval;
            }
        }
        public static List<NoticiaModel> All(Entities.MaxiContext db, UserModel? user, bool? forCache = false)
        {
            var retval = db.VwNoticias
                        .AsNoTracking()
                .OrderByDescending(x => x.Fch)
                .Select(x => new NoticiaModel(x.Guid)
                {
                    Fch = x.Fch,
                    Visible = x.Visible,
                    Professional = x.Professional,
                    Caption = new LangTextModel(x.Guid, LangTextModel.Srcs.ContentTitle)
                    {
                        Esp = x.TitleEsp,
                        Cat = x.TitleCat,
                        Eng = x.TitleEng,
                        Por = x.TitlePor
                    },
                    UrlSegment = new LangTextModel(x.Guid, LangTextModel.Srcs.ContentUrl)
                    {
                        Esp = x.UrlEsp,
                        Cat = x.UrlCat,
                        Eng = x.UrlEng,
                        Por = x.UrlPor
                    }
                }).ToList();

            if (!forCache ?? false)
            {
                if (user == null || !user.isProfesional())
                    retval = retval.Where(x => x.Professional == false).ToList();
            }

            return retval;
        }

        public static List<Box> Boxes(Entities.MaxiContext db, UserModel? user, LangDTO lang)
        {
            var retval = All(db, user)
                .Select(x => new Box
                {
                    Caption = x.Caption.Tradueix(lang),
                    Url = x.Url(lang),
                    ImageUrl = x.ThumbnailUrl()
                }).ToList();
            return retval;
        }
    }



}

