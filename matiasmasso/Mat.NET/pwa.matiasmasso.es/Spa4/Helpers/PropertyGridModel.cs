using DTO;

namespace Spa4.Helpers
{
    public class PropertyGridModel
    {
        public enum Cods:int
        {
            Text,
            Boolean,
            Dropdown,
            Tel,
            Email
        }
        public List<Item> Items { get; set; } = new();


        public Item AddString(int field, string? value = null, string? labelEsp = "", string? labelCat = null, string? labelEng = null, string? labelPor = null)
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Id = value;
            item.DisplayText = value;
            item.Label = new DTO.LangTextDTO(labelEsp, labelCat, labelEng, labelPor);
            item.Cod = Cods.Text;
            Items.Add(item);
            return item;
        }


        public Item AddEnum(int field, object? value = null, string? id = "", string? displayText = "", string? labelEsp = "", string? labelCat = null, string? labelEng = null, string? labelPor = null)
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Id = id;
            item.DisplayText = displayText;
            item.Label = new DTO.LangTextDTO(labelEsp, labelCat, labelEng, labelPor);
            item.Cod = Cods.Dropdown;
            Items.Add(item);
            return item;
        }

        public Item AddBool(int field, bool? value = false, string? labelEsp = "", string? labelCat = null, string? labelEng = null, string? labelPor = null)
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Id = (bool)value! ? "True":"False";
            item.DisplayText = item.Id;
            item.Label = new DTO.LangTextDTO(labelEsp, labelCat, labelEng, labelPor);
            item.Cod = Cods.Boolean;
            Items.Add(item);
            return item;
        }

        public Item AddItem(string esp, string cat, string eng, string por, string? value = null, object? tag = null, Cods? cod = null)
        {
            var item = new Item();
            item.Field = Items.Count;
            item.Label = new DTO.LangTextDTO(esp, cat, eng, por);
            item.Value = value;
            item.Cod = cod;
            //item.Tag = tag ?? value;
            Items.Add(item);
            return item;
        }

        public class Item
        {
            public int Field { get; set; }
            public Object? Value { get; set; } //original object to edit
            public string? Id { get; set; } // key string to match on an enumerable, maybe a Guid, a Id
            public string? DisplayText { get; set; } // on enumerable
            public DTO.LangTextDTO? Label { get; set; }
            public Cods? Cod { get; set; }
        }

    }
}
