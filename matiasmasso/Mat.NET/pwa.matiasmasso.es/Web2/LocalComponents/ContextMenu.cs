using DTO;

namespace Web.LocalComponents
{
    public class ContextMenu
    {
        public Object? Tag { get; set; }
        public List<Item> Items = new();

        public ContextMenu(Object? tag = null)
        {
            Tag = tag;
        }

        public Item AddItem(string caption, int? key = null, IModel? tag = null)
        {
            return new Item(this)
            {
                Caption = caption,
                Key = key,
                Tag = tag
            };
        }
        public class Item
        {
            public ContextMenu ContextMenu { get; set; }
            public Item? Parent { get; set; }
            public string? Caption { get; set; }
            public int? Key { get; set; }
            public IModel? Tag { get; set; }

            public Item(ContextMenu contextMenu)
            {
                ContextMenu = contextMenu;
                ContextMenu.Items.Add(this);
            }

            public Item AddItem(string caption, int? key = null, IModel? tag = null)
            {
                return new Item(ContextMenu)
                {
                    Parent = this,
                    Caption = caption,
                    Key = key,
                    Tag = tag
                };
            }

        }

        public class ClickEventArgs
        {
            public Object? Tag { get; set; }
            public int? Key { get; set; }

            public ClickEventArgs(Object? tag = null, int? key = null)
            {
                Tag = tag;
                Key = key;
            }

        }
    }

}
