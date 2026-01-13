using Cash.Components;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Cash.Models
{
    public class PropertyGridModel
    {
        public List<Item> Items { get; set; } = new();
        private CultureInfo culture = new CultureInfo("es-ES");
        public enum Formats
        {
            String,
            TextArea,
            Password,
            Bool,
            Integer,
            Decimal,
            N2,
            Eur,
            DateOnly,
            DateTime,
            Guidnom,
            IdNom,
            EmpId,
            Docfile,
            Zip
        }

        public void AddItem<T>(string caption, T value, Formats? format = Formats.String)
        {
            var item = new Item<T>(caption, value, (Formats)format!);
            AddItem(item);
        }

        public void AddString(string caption, string? value, int maxLength = 0)
        {
            var item = new Item<string>(caption, value ?? "", Formats.String);
            item.MaxLength = maxLength;
            AddItem(item);
        }

        public void AddBool(string caption, bool? value = false)
        {
            var item = new Item<bool>(caption, (bool)value!, Formats.Bool);
            AddItem(item);
        }

        public void AddInt(string caption, int? value)
        {
            Item item;
            if (value == null)
                item = new Item<string>(caption, null, Formats.Integer);
            else
                item = new Item<string>(caption, ((int)value).ToString(), Formats.Integer);
            AddItem(item);
        }

        public void AddDecimal(string caption, decimal? value, int decimals = 2)
        {
            Item item;
            if (value == null)
                item = new Item<string>(caption, null, Formats.Decimal);
            else
            {
                var pattern = $"0.{new string('0', decimals)}";
                item = new Item<string>(caption, ((decimal)value).ToString(pattern, culture), Formats.Eur);
                item.Decimals = decimals;
            }
            AddItem(item);
        }


        public void AddEur(string caption, decimal? value)
        {
            Item item;
            if (value == null)
                item = new Item<string>(caption, null, Formats.Eur);
            else
                item = new Item<string>(caption, ((decimal)value).ToString("0.00", culture), Formats.Eur);
            AddItem(item);
        }

        public void AddDateOnly(string caption, DateOnly? value)
        {
            Item item;
            if (value == null)
                item = new Item<string>(caption, null, Formats.DateOnly);
            else
            {
                var s = ((DateOnly)value).ToString("yyyy-MM-dd");
                item = new Item<string>(caption, s, Formats.DateOnly);
            }
            AddItem(item);
        }

        public void AddDateTime(string caption, DateTime? value)
        {
            Item item;
            if (value == null)
                item = new Item<string>(caption, null, Formats.DateTime);
            else
            {
                var s = ((DateTime)value).ToString("yyyy-MM-dd");
                item = new Item<string>(caption, s, Formats.DateTime);
            }
            AddItem(item);
        }

        public void AddEmp(EmpModel.EmpIds? value, string caption = "Empresa")
        {
            Item item;
            if (value == null)
                item = new Item<EmpModel.EmpIds>(caption, DTO.EmpModel.EmpIds.MMC, Formats.EmpId);
            else
            {
                item = new Item<EmpModel.EmpIds>(caption, (EmpModel.EmpIds)value, Formats.EmpId);
            }
            AddItem(item);
        }

        public void AddDocfile(DocfileModel? value, string caption = "Fitxer")
        {
            Item item = new Item<DocfileModel>(caption, value, Formats.Docfile);
            AddItem(item);
        }

        public void AddGuidnom(string caption, Guid? guid, IEnumerable<IModel>? values, Action? addNewRequest = null)
        {
            List<GuidNom>? guidnoms;
            if(values is List<GuidNom>)
                guidnoms = (List<GuidNom>?)values;
            else
                guidnoms= values?.Select(x => new GuidNom(x.Guid, x.ToString())).ToList() ?? [];

            var value = guidnoms?.FirstOrDefault(x => x.Guid == guid);
            Item item = new Item<GuidNom>(caption, value, guidnoms ?? new(), Formats.Guidnom);
            item.AddNewRequest = addNewRequest;
            AddItem(item);
        }

        public void AddGuidnom(string caption, GuidNom? value, List<GuidNom>? values, Action? addNewRequest = null)
        {
            Item item = new Item<GuidNom>(caption, value, values ?? new(), Formats.Guidnom);
            AddItem(item);
        }

        public void AddEnum<T>(string caption, T? value = default ) where T : struct, Enum
        {
            var values = new List<IdNom>();
            foreach (int v in Enum.GetValues(typeof(T)))
                values.Add(new IdNom { Id = v, Nom = Enum.GetName(typeof(T), v) });
            var idNomValue = values.FirstOrDefault(x=>x.Id == Convert.ToInt32(value));
            Item item = new Item<IdNom>(caption, idNomValue, values, Formats.IdNom);
            AddItem(item);
        }

        public void AddIdNom(string caption, int? idx = 0, IEnumerable<string>? strings = null)
        {
            var stringList = strings?.ToList() ?? new();
            var values = new List<IdNom>();
            for (int i = 0; i < stringList.Count; i++)
            {
                values.Add(new IdNom { Id = i, Nom = stringList[i] });
            }
            var value = idx == null ? null : values[(int)idx];
            Item item = new Item<IdNom>(caption, value, values, Formats.IdNom);
            AddItem(item);
        }


        public void AddTextArea(string caption, string? value)
        {
            var item = new Item<string>(caption, value ?? "", Formats.TextArea);
            AddItem(item);
        }

        public void AddPassword(string? value)
        {
            var item = new Item<string>("Password", value ?? "", Formats.Password);
            AddItem(item);
        }

        private void AddItem(Item item)
        {
            item.Field = Items.Count;
            Items.Add(item);
        }

        public GuidNom? GetGuidNom(int idx) => ((Item<GuidNom>)Items.First(x => x.Field == idx)!).Value;
        public Guid? GetGuid(int idx) => ((Item<GuidNom>)Items.First(x => x.Field == idx)!).Value?.Guid;
        public int? IdNom(int idx) => ((Item<IdNom>)Items.First(x => x.Field == idx)!).Value?.Id;

        public string? GetNumericDigits(int idx)
        {
            var value = Value<string>(idx);
            var retval = string.IsNullOrEmpty(value) ? null : new String(value.Where(Char.IsDigit).ToArray());
            return retval;
        }

        public DateOnly? GetDateOnly(int idx)
        {
            DateOnly? retval = null;
            var item = (Item<string>)Items.First(x => x.Field == idx)!;
            if (!string.IsNullOrEmpty(item.Value))
                retval = DateOnly.Parse(item.Value);
            return retval;
        }

        public DateTime? GetDateTime(int idx)
        {
            DateTime? retval = null;
            var item = (Item<string>)Items.First(x => x.Field == idx)!;
            if (!string.IsNullOrEmpty(item.Value))
                retval = DateTime.Parse(item.Value);
            return retval;
        }

        public int? GetInt(int idx)
        {
            int? retval = null;
            var item = (Item<string>)Items.First(x => x.Field == idx)!;
            if (!string.IsNullOrEmpty(item.Value))
                retval = int.Parse(item.Value);
            return retval;
        }

        public int? GetId(int idx)
        {
            int? retval = null;
            var item = (Item<IdNom>)Items.First(x => x.Field == idx)!;
            if (item.Value != null)
                retval = ((IdNom)item.Value).Id;
            return retval;
        }
        public decimal? GetEur(int idx)
        {
            decimal? retval = null;
            decimal tmp;
            var item = (Item<string>)Items.First(x => x.Field == idx)!;
            if (!string.IsNullOrEmpty(item.Value) && decimal.TryParse(item.Value, NumberStyles.Float, culture, out tmp))
                retval = tmp;
            return retval;
        }
        public decimal? GetDecimal(int idx)
        {
            decimal? retval = null;
            decimal tmp;
            var item = (Item<string>)Items.First(x => x.Field == idx)!;
            if (!string.IsNullOrEmpty(item.Value) && decimal.TryParse(item.Value, NumberStyles.Float, culture, out tmp))
                retval = tmp;
            return retval;
        }
        public EmpModel.EmpIds GetEmp(int idx)
        {
            var item = (Item<EmpModel.EmpIds>)Items.First(x => x.Field == idx)!;
            EmpModel.EmpIds retval = item.Value;
            return retval;
        }

        public DocfileModel? GetDocfile(int idx)
        {
            var item = (Item<DocfileModel>)Items.First(x => x.Field == idx)!;
            DocfileModel? retval = item.Value;
            return retval;
        }

        public string? GetString(int idx)
        {
            var item = (Item<string>)Items.First(x => x.Field == idx)!;
            var retval = item.Value;
            return retval;
        }

        public bool GetBool(int idx)
        {
            var item = (Item<bool>)Items.First(x => x.Field == idx)!;
            var retval = item.Value;
            return retval;
        }

        public T? Value<T>(int idx)
        {
            var item = (Item<T>)Items.First(x => x.Field == idx);
            return item.Value;
            //if (item.Value == null)
            //    return default;
            //else
            //    return item.Value;
        }

        public Item GetItem(int idx) => Items.First(x => x.Field == idx);

        public class Item
        {
            public int Field { get; set; }
            public string Caption { get; set; }
            public Formats Format { get; set; }
            public int Decimals { get; set; } = 2;
            public bool IsDirty { get; set; }
            public string? WarningMessage { get; set; }

            public Action? AddNewRequest { get; set; }

            public Item(string caption, Formats format)
            {
                Caption = caption;
                Format = format;
            }
        }


        public class Item<T> : Item
        {
            public T? Value { get; set; }
            public List<T> Values { get; set; } = new();

            public int MaxLength { get; set; }

            public Item(string caption, T? value, Formats format) : base(caption, format)
            {
                Value = value;
            }
            public Item(string caption, T? value, List<T> values, Formats format) : base(caption, format)
            {
                Value = value;
                Values = values ?? new();
            }

            public bool IsOverLength() => MaxLength > 0 && !string.IsNullOrEmpty(Value?.ToString()) && Value!.ToString()!.Length > MaxLength;
        }


    }
}
