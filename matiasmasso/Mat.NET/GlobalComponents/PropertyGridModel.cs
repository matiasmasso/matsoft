using System.Collections.Generic;
using static GlobalComponents.PropertyGridModel;

namespace GlobalComponents
{
    public class PropertyGridModel
    {
        Object Src { get; set; }
        public List<Item> Items { get; set; } = new();
        public enum Cods : int
        {
            Text,
            Boolean,
            Dropdown,
            Tel,
            Email,
            LangText,
            Iban,
            Address,
            Fch,
            GuidNom,
            Password,
            Eur,
            Multiline
        }


        public PropertyGridModel(Object src)
        {
            Src = src;
        }

        public Item AddItem(int field, Cods cod, object? value = null, string? label = null)
        {
            var item = new Item
            {
                Field = field,
                Cod = cod,
                Value = value,
                Label = label ?? ""
            };
            Items.Add(item);
            return item;
        }
        public Item AddDropdownItem<T>(int field, Cods cod, Dictionary<T, string> values, T? value = default(T), string? label = null)
        {
            var item = new DropdownItem<T>
            {
                Field = field,
                Cod = cod,
                Value = value,
                Values = values,
                Label = label ?? ""
            };
            Items.Add(item);
            return item;
        }


        public U? GetValue<U>(int field) => (U?)Items.FirstOrDefault(x => x.Field == (int)field)?.Value;

        public class Item
        {
            public int Field { get; set; }
            public Cods? Cod { get; set; }
            public object? Value { get; set; } //original object to edit
            public string? Label { get; set; }

            public int? MaxLength { get; set; }

        }
        public class DropdownItem<T>:Item
        {
            public Dictionary<T, string> Values { get; set; } // for dropdown selection
            public new T? Value { get; set; } //original object to edit
        }

    }
}
