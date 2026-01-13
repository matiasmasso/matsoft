using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOBlogPostModel
    {
        public Guid Guid { get; set; }
        public DateTime Fch { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Text { get; set; }

        public DTOUrl Url { get; set; }

        public string ThumbnailUrl { get; set; }


        public DTOPostComment.TreeModel Comments { get; set; }

        public List<DTOBlogPostModel> Posts { get; set; }

        public DTOBlogPostModel() : base()
        {
            this.Posts = new List<DTOBlogPostModel>();
            this.Comments = new DTOPostComment.TreeModel();
            this.Url = DTOUrl.Factory("blog");
        }

        public static DTOBlogPostModel Factory(DTOContent post, DTOLang lang)
        {
            DTOBlogPostModel retval = new DTOBlogPostModel();
            retval.Guid = post.Guid;
            retval.Fch = post.Fch;
            if (post.Title != null)
            {
                retval.Title = post.Title.Tradueix(lang);
            }
            if (post.Text != null)
            {
                retval.Text = post.Text.Tradueix(lang);
            }
            if (post.Excerpt != null)
            {
                retval.Excerpt = post.Excerpt.Tradueix(lang);
            }

            if (post is DTOBlogPost)
            {
                retval.ThumbnailUrl = ((DTOBlogPost)post).ThumbnailUrl();
                retval.Url = ((DTOBlogPost)post).Url();
            }
            return retval;
        }

        public string FbImg()
        {
            string retval = MatHelperStd.TextHelper.FbImg(this.Text);
            return retval;
        }

        public string FbUrl(DTOWebDomain domain)
        {
            string retval = this.Url.AbsoluteUrl(domain.DefaultLang());
            return retval;
        }
    }
}
