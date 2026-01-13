using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ContextMenuModel
    {
        public Object? Tag { get; set; }
        public List<Item> Items = new();

        public ContextMenuModel(Object? tag = null)
        {
            Tag = tag;
        }

        public Item AddItem(string caption, int? key = null, Object? tag = null)
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
            public ContextMenuModel ContextMenu { get; set; }
            public Item? Parent { get; set; }
            public string? Caption { get; set; }
            public int? Key { get; set; }
            public Object? Tag { get; set; }
            public bool Disabled { get; set; }

            public Item(ContextMenuModel contextMenu)
            {
                ContextMenu = contextMenu;
                ContextMenu.Items.Add(this);
            }

            public Item AddItem(string caption, int? key = null, Object? tag = null)
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
            public Object? Target { get; set; }
            public Object? Tag { get; set; }
            public int? Key { get; set; }

            public ClickEventArgs(Object? tag = null, int? key = null, object? target = null)
            {
                Tag = tag;
                Key = key;
                Target = target;
            }

        }

    }
}
