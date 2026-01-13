using System.Collections.Generic;

namespace DTO
{
    public class BoxModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string NavigateTo { get; set; }
        public string ImageUrl { get; set; }
        public string Tag { get; set; }

        public static BoxModel Factory(string title, string navigateTo = null, string imageUrl = "", string text = "", string tag = "")
        {
            BoxModel retval = new BoxModel();
            {
                var withBlock = retval;
                withBlock.Title = title;
                withBlock.NavigateTo = navigateTo;
                withBlock.ImageUrl = imageUrl;
                withBlock.Text = text;
                withBlock.Tag = tag;
            }
            return retval;
        }

        public class Collection : List<BoxModel>
        {
            public BoxModel Add(string title, string navigateTo = "", string imageUrl = "", string text = "", string tag = "")
            {
                BoxModel retval = BoxModel.Factory(title, navigateTo, imageUrl, text, tag);
                this.Add(retval);
                return retval;
            }
        }
    }

}
