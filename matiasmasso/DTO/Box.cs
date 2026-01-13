using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Box:BaseGuid
    {
        public string Caption { get; set; } = string.Empty;
        public string? Url { get; set; } 
        public string? ImageUrl { get; set; }
        public Guid? Parent { get; set; }
        public Modes Mode { get; set; } = Modes.Navigation;

        public enum Modes : int
        {
            Navigation,
            Toggle,
            Action
        }


        public Box() : base() { }
        public Box(Guid guid) : base(guid) { }


        public static Box Factory(string caption, string url = "", string imageUrl="", Guid? parent = null)
        {
            var retval = new Box();
            retval.Caption = caption;
            retval.Url = url;
            retval.ImageUrl = imageUrl;
            retval.Parent = parent;
            return retval;
        }

        public bool HasChildren(List<Box> items)
        {
            return Children(items).Count() > 0;
        }

        public List<Box> Children(List<Box> items)
        {
            return items.Where(x => x.Parent.Equals(Guid)).ToList();
        }

        public override string ToString()
        {
            return Caption;
        }



    }
}
