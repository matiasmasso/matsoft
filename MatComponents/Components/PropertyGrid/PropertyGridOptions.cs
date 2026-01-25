using System;
using System.Collections.Generic;
using System.Text;

namespace MatComponents.Components.PropertyGrid
{
    public class PropertyGridOptions
    {
        //This is just an example — you can expand it as your component grows.
        public bool ShowCategories { get; set; } = true;
        public bool ExpandAllCategories { get; set; } = false;
        public bool ExpandNestedObjects { get; set; } = false;
        public bool ShowDescriptions { get; set; } = true;
        public bool ShowReadOnlyProperties { get; set; } = true;
        public bool ShowNullValues { get; set; } = false;

        public bool SortCategories { get; set; } = true;
        public bool SortProperties { get; set; } = true;

        public Func<string, string>? LabelFormatter { get; set; }

        public Dictionary<Type, Type> CustomEditors { get; set; } = new();
    }

}
