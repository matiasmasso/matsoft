using DocumentFormat.OpenXml.Bibliography;

namespace Cash.Models
{
    public class SwipeMenuModel
    {
        public List<Item> LeftItems { get; set; } = new();
        public List<Item> RightItems { get; set; } = new();
        public class Item
        {
            public int Id { get; set; }
            public string? Caption { get; set; }
            public Modes Mode { get; set; }
            public Styles Style { get; set; }
            public object? Tag { get; set; }

            public string? NavigateTo()
            {
                string? retval = Tag?.ToString();
                if (!string.IsNullOrEmpty(retval) && !retval.StartsWith("http")) 
                    retval = $"https://{retval}";
                return retval;
            }
        }

        public static SwipeMenuModel Default()
        {
            var retval = new SwipeMenuModel();
            retval.AddDeleteButton();
            retval.AddBrowseButton();
            retval.AddCopyToClipboardButton();
            return retval;
        }

        public void AddDeleteButton(object? tag = null)
        {
            LeftItems.Add(new Item {Mode = Modes.Action,  Caption = "Eliminar", Style = Styles.Red, Tag = tag });
        }

        public void AddBrowseButton(string? url=null)
        {
            RightItems.Add(new Item { Mode = Modes.Navigation, Caption = "Navegar", Style = Styles.Blue, Tag = url });
        }

        public void AddCopyToClipboardButton(string? text=null, string? caption = null)
        {
            RightItems.Add(new Item { Mode = Modes.CopyToClipboard, Style = Styles.Gray, Tag = text, Caption = string.IsNullOrEmpty(caption) ? "Copiar" : caption });
        }

        public enum Modes
        {
            Action,
            Navigation,
            CopyToClipboard
        }
        public enum Styles
        {
            Blue,
            Red,
            Green,
            Gray
        }


    }
}
