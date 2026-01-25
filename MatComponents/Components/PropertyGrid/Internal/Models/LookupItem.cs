using System;
using System.Collections.Generic;
using System.Text;

namespace MatComponents.Components.PropertyGrid.Internal.Models
{
    public class LookupItem<T>
    {
        public T Value { get; set; } = default!;
        public string Label { get; set; } = string.Empty;

        public LookupItem() { }

        public LookupItem(T value, string label)
        {
            Value = value;
            Label = label;
        }
    }

}
