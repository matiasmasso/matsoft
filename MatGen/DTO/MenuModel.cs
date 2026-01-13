using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MenuModel
    {
        public List<Item> Items { get; set; } = new();

        public enum Modes
        {
            Navigate,
            Zoom,
            Edit,
            Clear,
            Create,
            Parent,
            CustomAction
        }

        public static MenuModel Factory(IModel? value = null)
        {
            var retval = new MenuModel();
            //if (value != null) retval.AddItem("zoom", value.PropertyPageUrl());
            if (value != null) retval.AddItem("clear", Modes.Clear);
            retval.AddItem("edit", Modes.Edit);
            //retval.AddItem("add", value?.CreatePageUrl());
            return retval;
        }
        public Item AddItem(string caption, string? url = null)
        {
            var retval = new Item(caption, url);
            Items.Add(retval);
            return retval;
        }
        public Item AddItem(string caption, Action action)
        {
            var retval = new Item(caption, action);
            Items.Add(retval);
            return retval;
        }

        public Item AddItem(string caption, Modes mode)
        {
            var retval = new Item(caption, mode);
            Items.Add(retval);
            return retval;
        }


        public class Item
        {
            public string Caption { get; set; }
            public string? Url { get; set; }
            public bool IsBusy { get; set; }

            public Modes Mode { get; set; }
            public List<Item> Items { get; set; } = new();



            public Action? Action { get; set; }
            public Item(string? caption, string? url)
            {
                Caption = caption ?? "?";
                Url = url;
                Mode = url == null ? Modes.Parent : Modes.Navigate;
            }
            public Item(string? caption, Action action)
            {
                Caption = caption ?? "?";
                Action = action;
                Mode = Modes.CustomAction;
            }

            public Item(string? caption, Modes mode)
            {
                Caption = caption ?? "?";
                Mode = mode;
            }

            public Item AddItem(string? caption, string? url = null)
            {
                var retval = new Item(caption, url);
                Items.Add(retval);
                return retval;
            }
            public Item AddItem(string? caption, Action action)
            {
                var retval = new Item(caption ?? "?", action);
                Items.Add(retval);
                return retval;
            }

            public Item AddItem(string? caption, Modes mode)
            {
                var retval = new Item(caption, mode);
                Items.Add(retval);
                return retval;
            }



        }
    }
}
