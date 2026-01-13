using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using DTO;
using System.Collections.Generic;

namespace Web
{
    public class PropertyGridModel
    {
        public IModel? Src { get; set; }
        public List<Item> Items { get; set; } = new();
        public enum Cods : int
        {
            Text,
            Boolean,
            Dropdown,
            Tel,
            Email,
            LangText,
            Fch,
            GuidNom,
            Password,
            Eur,
            Multiline,
            Sex,
            FchLocation,
            Docfile,
            Url
        }


        public PropertyGridModel(IModel? src)
        {
            Src = src;
        }

        public Item AddUrl(int field, string? value = null, string? label = "Url")
        {
            var retval = AddString(field, value, label);
            retval.Cod = Cods.Url;
            return retval;
        }
        public Item AddString(int field, string? value = null, string? label = null, int? maxLength = null)
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Id = value;
            item.DisplayText = value ?? "";
            item.Label = label ?? "";
            item.Cod = Cods.Text;
            item.MaxLength = maxLength;
            Items.Add(item);
            return item;
        }
        public Item AddMultiline(int field, string? value = null, string? label = null)
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Id = value;
            item.DisplayText = value ?? "";
            item.Label = label ?? "";
            item.Cod = Cods.Multiline;
            Items.Add(item);
            return item;
        }
        public Item AddEmail(int field, UserModel? value = null, string? label = "Email")
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Label = label;
            item.Cod = Cods.Email;
            Items.Add(item);
            return item;
        }

        public Item AddPassword(int field, string? value = null, string? label = null, int? maxLength = null)
        {
            var item = AddString(field, value, label);
            item.Cod = Cods.Password;
            item.MaxLength = maxLength;
            return item;
        }
        public Item AddInt(int field, int? value = null, string? label = null)
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Id = value == null ? null : String.Format("{0}", value);
            item.DisplayText = item.Id;
            item.Label = label ?? "";
            item.Cod = Cods.Text;
            Items.Add(item);
            return item;
        }

        public Item AddDecimal(int field, decimal? value = null, string? label = null)
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Id = value == null ? null : String.Format("{0:N2}", value);
            item.DisplayText = item.Id;
            item.Label = label ?? "";
            item.Cod = Cods.Text;
            Items.Add(item);
            return item;
        }
        public Item AddPercent(int field, decimal? value = null, string? label = null)
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Id = value == null ? null : String.Format("{0} %", value);
            item.DisplayText = item.Id;
            item.Label = label ?? "";
            item.Cod = Cods.Text;
            Items.Add(item);
            return item;
        }

        public Item AddDropdown<T>(int field, List<T> values, Guid? value, string? label = null) where T : IModel
        {
            var item = new Item();
            item.Field = field;
            item.Values = values.ConvertAll(x => (IModel)x);
            if(value != null)
                item.Value = item.Values.FirstOrDefault(x => x.Guid == value);
            item.Label = label ?? "";
            item.Cod = Cods.Dropdown;
            Items.Add(item);
            return item;
        }
        public Item AddDropdown<T>(int field, List<T> values, T? value = default, string? label = null) where T : IModel
        {
            var item = new Item();
            item.Field = field;
            item.Value = (object?)value;
            item.Values = values.ConvertAll(x => (IModel)x);
            item.Label = label ?? "";
            item.Cod = Cods.Dropdown;
            Items.Add(item);
            return item;
        }

        public Item AddSex(int field, PersonModel.Sexs? value = PersonModel.Sexs.NotSet, string? label = null)
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Label = label ?? "Sexe";
            item.Cod = Cods.Sex;
            Items.Add(item);
            return item;
        }

        public Item AddFchLocation(int field, List<LocationModel>? locations, FchLocationModel? value, string? label = null)
        {
            var item = new Item();
            item.Field = field;
            item.Values = locations?.Select(x => (IModel)x).ToList() ?? new();
            item.Value = value;
            item.Label = label ?? "?";
            item.Cod = Cods.FchLocation;
            Items.Add(item);
            return item;
        }

        public Item AddDocfile(int field, DocfileModel? value, string caption = "Fitxer")
        {
            Item item = new Item();
            item.Field = field;
            item.Cod = Cods.Docfile;
            item.Value = value;
            item.Label = caption;
            Items.Add(item);
            return item;
        }

        public void LoadDropdownValues<T>(int field, List<T> values)
        {
            var item = Items.First(x => x.Field == field);
            item.Values = values.ConvertAll(x => (IModel)x);
        }

        public Item AddLookup<T>(int field, T? value = default, string? label = null) where T : IModel
        {
            var item = new Item();
            item.Field = field;
            item.Value = (object?)value;
            item.Label = label ?? "";
            item.Cod = Cods.Dropdown;
            Items.Add(item);
            return item;
        }

        public Item AddFch(int field, DateTime? value = null, string label = "Date")
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            //item.Id = value;
            item.DisplayText = string.Format("{0:dd/MM/yy}", value);
            item.Label = label;
            item.Cod = Cods.Fch;
            Items.Add(item);
            return item;
        }

        public Item AddDateTime(int field, DateTime? value = null, string label = "Date")
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            //item.Id = value;
            item.DisplayText = string.Format("{0:dd/MM/yy HH:mm}", value);
            item.Label = label;
            item.Cod = Cods.Fch;
            Items.Add(item);
            return item;
        }


        public Item AddGuidNom(int field, GuidNom? value = null, string? label = null)
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Id = value?.Guid.ToString() ?? "";
            item.DisplayText = value?.Nom ?? "";
            item.Label = label;
            item.Cod = Cods.GuidNom;
            Items.Add(item);
            return item;
        }

        public Item AddSex(int field, PersonModel.Sexs value, string label = "Sexe")
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            //item.Id = value?.ToString() ?? "";
            //item.DisplayText = ((IModel?)(item.Value))?.ToString() ?? "?";
            item.Label = label;
            item.Cod = Cods.Sex;
            Items.Add(item);
            return item;
        }

        public Item AddEnum(int field, Enum e, int? value = null, string? label = null)
        {
            var item = new Item();
            item.Field = field;
            item.Values = EnumItemModel.Factory(e);
            item.Value = item.Values.FirstOrDefault(x => ((EnumItemModel)x).Value == (int?)value);
            item.Id = value?.ToString() ?? "";
            item.DisplayText = ((IModel?)(item.Value))?.ToString() ?? "?";
            item.Label = label;
            item.Cod = Cods.Dropdown;
            Items.Add(item);
            return item;
        }


        public Item AddBool(int field, bool? value = false, string? label = null)
        {
            var item = new Item();
            item.Field = field;
            item.Value = value;
            item.Id = (bool)value! ? "True" : "False";
            item.DisplayText = item.Id;
            item.Label = label;
            item.Cod = Cods.Boolean;
            Items.Add(item);
            return item;
        }


        public string? GetString(int field) => Items.FirstOrDefault(x => x.Field == (int)field)?.Value?.ToString();

        //public LangTextModel GetLangText(LangTextModel src, int fieldEsp, int fieldCat, int fieldEng, int fieldPor)
        //{
        //    var retval = src;
        //    retval.Esp = GetString(fieldEsp);
        //    retval.Cat = GetString(fieldCat);
        //    retval.Eng = GetString(fieldEng);
        //    retval.Por = GetString(fieldPor);
        //    return retval;
        //}

        public int? GetInt(int field)
        {
            int? retval = null;
            var item = Items.FirstOrDefault(x => x.Field == (int)field);
            if (item != null)
            {
                if (item.Value is int)
                    retval = (int)item.Value;
                else
                    retval = Convert.ToInt32((string?)item.Value);
            }
            return retval;
        }
        public decimal? GetDecimal(int field)
        {
            decimal? retval = null;
            var item = Items.FirstOrDefault(x => x.Field == (int)field);
            if (item != null)
            {
                if (item.Value is int)
                    retval = (int)item.Value;
                else
                    retval = Convert.ToDecimal((string?)item.Value);
            }
            return retval;
        }
        //public T? GetValue<T>(int field) => (T?)Items.FirstOrDefault(x => x.Field == (int)field)?.Value;

        public int GetEnum(int field)
        {
            var enumItem = GetValue<EnumItemModel>(field);
            return enumItem != null ? enumItem.Value : default;
        }
        public T? GetValue<T>(int field)
        {
            T? retval = default(T);
            Item? item = Items.FirstOrDefault(x => x.Field == (int)field);
            if (item != null)
            {
                retval = (T?)item.Value;
            }
            return retval;
        }



        public PersonModel.Sexs GetSex(int field)
        {
            return GetValue<PersonModel.Sexs>(field);
        }

        public FchLocationModel? GetFchLocation(int field) => GetValue<FchLocationModel>(field);



        public Guid? GetModelGuid(int field)
        {
            var valueModel = GetValue<IModel>(field);
            return valueModel == null ? null : valueModel.Guid;
        }

        public bool GetBool(int field) => GetValue<bool?>(field) ?? false;
        public void SetBool(int field, bool value)
        {
            Item? item = Items.FirstOrDefault(x => x.Field == (int)field);
            if (item != null)
            {
                item.Value = value;
                item.Id = (bool)value! ? "True" : "False";
                item.DisplayText = item.Id;
            }
        }

        public class Item
        {
            public int Field { get; set; }
            public Object? Value { get; set; } //original object to edit
            public List<IModel>? Values { get; set; } // for dropdown selection
            public string? Id { get; set; } // key string to match on an enumerable, maybe a Guid, a Id
            public string? DisplayText { get; set; } // on enumerable
            public string? Label { get; set; }
            public Cods? Cod { get; set; }
            public int? MaxLength { get; set; }
            public bool IsEditing { get; set; }

            public override string ToString()=> Label ?? "label?";

            public void SetValues<T>(List<T>? values)
            {
                Values = values == null
                    ? new List<IModel>()
                    : values.Where(x => x != null).Select(x => (IModel)x!).ToList();
            }
        }

    }
}
