using System.Collections.Generic;

namespace DTO
{
    public class BoxViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string NavigateTo { get; set; }
        public string Tag { get; set; }

        public static BoxViewModel Factory(string title, string navigateTo = "", string text = "", string tag = "")
        {
            BoxViewModel retval = new BoxViewModel();
            {
                var withBlock = retval;
                withBlock.Title = title;
                withBlock.NavigateTo = navigateTo;
                withBlock.Text = text;
                withBlock.Tag = tag;
            }
            return retval;
        }

        public class Collection : List<BoxViewModel>
        {
            public BoxViewModel Add(string title, string navigateTo = "", string text = "", string tag = "")
            {
                var retval = BoxViewModel.Factory(title, navigateTo, text, tag);
                base.Add(retval);
                return retval;
            }
        }

        public override string ToString()
        {
            return string.Format("BoxViewModel: {0} -> {1}", Title ?? "", NavigateTo ?? "");
        }
    }

    public class ImageBoxViewModel : BoxViewModel
    {
        public string ImageUrl { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }


        public static new ImageBoxViewModel Factory(string title, string navigateTo = "", string text = "", string tag = "")
        {
            ImageBoxViewModel retval = new ImageBoxViewModel();
            {
                var withBlock = retval;
                withBlock.Title = title;
                withBlock.NavigateTo = navigateTo;
                withBlock.Text = text;
                withBlock.Tag = tag;
            }
            return retval;
        }


        public static ImageBoxViewModel Factory(string imageUrl, int imageWidth, int imageHeight, string title, string navigateTo = "", string text = "", string tag = "")
        {
            ImageBoxViewModel retval = new ImageBoxViewModel();
            {
                var withBlock = retval;
                withBlock.ImageUrl = imageUrl;
                withBlock.ImageWidth = imageWidth;
                withBlock.ImageHeight = imageHeight;
                withBlock.Title = title;
                withBlock.NavigateTo = navigateTo;
                withBlock.Text = text;
                withBlock.Tag = tag;
            }
            return retval;
        }

        public new class Collection : List<ImageBoxViewModel>
        {
            public ImageBoxViewModel Add(string imageUrl, int imageWidth, int imageHeight, string title, string navigateTo = "", string text = "", string tag = "")
            {
                ImageBoxViewModel retval = ImageBoxViewModel.Factory(imageUrl, imageWidth, imageHeight, title, navigateTo, text, tag);
                base.Add(retval);
                return retval;
            }

            public ImageBoxViewModel Add(string title, string navigateTo = "", string text = "", string tag = "")
            {
                ImageBoxViewModel retval = ImageBoxViewModel.Factory(title, navigateTo, text, tag);
                base.Add(retval);
                return retval;
            }
        }
    }
}
