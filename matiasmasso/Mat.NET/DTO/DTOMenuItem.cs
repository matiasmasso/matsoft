using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOMenuItem
    {
        public Guid Guid { get; set; }
        public string Caption { get; set; }
        public string Action { get; set; }
        public bool Selected { get; set; }
        public List<DTOMenuItem> Children { get; set; }

        public DTOMenuItem(string caption, string action = "") : base()
        {
            Guid = Guid.NewGuid();
            Caption = caption;
            Action = action;
            Children = new List<DTOMenuItem>();
        }

        public DTOMenuItem AddChild(string caption, string action = "")
        {
            DTOMenuItem retval = new DTOMenuItem(caption, action);
            Children.Add(retval);
            return retval;
        }

        public static DTOMenuItem Separator()
        {
            DTOMenuItem retval = new DTOMenuItem("-", "");
            return retval;
        }
        public bool HasChildren()
        {
            return Children.Count > 0;
        }
    }
}
