using Cash.Components;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using System.Globalization;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;

namespace Cash.Models
{
    public class PropertyGridModel
    {
        public List<Item> Items { get; set; } = new();
        public CultureInfo Culture = new CultureInfo("es-ES");
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
            Zip,
            Object,
            IModel,
            IModelList,
            IdNomList,
            BaseTipusQuota
        }

        public void AddItem<T>(string caption, T value, Formats? format = Formats.String)
        {
            var item = new Item<T>(caption, value, (Formats)format!);
            AddItem(item);
        }

        public void AddString(string caption, String? value, int maxLength = 0)
        {
            Item item;
            if (value == null)
                item = new Item<string>(caption, null, Formats.String);
            else
                item = new Item<string>(caption, ((string)value).ToString(), Formats.String);
            item.MaxLength = maxLength;
            AddItem(item);
        }

        public void AddBool(string caption, bool? value = false)
        {
            var item = new Item<bool>(caption, value ?? false, Formats.Bool);
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
                item = new Item<string>(caption, ((decimal)value).ToString(pattern, Culture), Formats.Decimal);
                item.Decimals = decimals;
            }
            AddItem(item);
        }

        public void AddPercent(string caption, decimal? value, int decimals = 2)
        {
            Item item;
            if (value == null)
                item = new Item<string>(caption, null, Formats.Decimal);
            else
            {
                var pattern = $"0.{new string('0', decimals)} %";
                item = new Item<string>(caption, ((decimal)value).ToString(pattern, Culture), Formats.Decimal);
                item.Decimals = decimals;
            }
            AddItem(item);
        }


        public Item AddEur(string caption, decimal? value)
        {
            Item item;
            if (value == null)
                item = new Item<string>(caption, null, Formats.Eur);
            else
                item = new Item<string>(caption, ((decimal)value).ToString("0.00", Culture), Formats.Eur);
            AddItem(item);
            return item;
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

        public void AddBaseQuota(string caption, BaseQuotaModel? value)
        {
            Item item = new Item<BaseQuotaModel>(caption, value, Formats.BaseTipusQuota);
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
            if (values is List<GuidNom>)
                guidnoms = (List<GuidNom>?)values;
            else
                guidnoms = values?.Select(x => new GuidNom(x.Guid, x.ToString())).ToList() ?? [];

            var value = guidnoms?.FirstOrDefault(x => x.Guid == guid);
            Item item = new Item<GuidNom>(caption, value, guidnoms ?? new(), Formats.Guidnom);
            item.AddNewRequest = addNewRequest;
            AddItem(item);
        }

        //        retval.AddObject("Subcompte", contacts?.FirstOrDefault(x => x.Guid == value.Contact), contacts?.Select(x => (object)x).ToArray(), AddNewContactHandler);

        //public void AddContact(string caption, Guid? guid, List<ContactModel>? values, Action? addNewRequest = null)
        //{
        //    Object? obj = values?.FirstOrDefault(x => x.Guid == guid);
        //    Object[]? array = values?.Select(x => (object)x).ToArray();
        //    List<Object> objs = array == null ? new List<object>() : new List<object>(array);
        //    Item item = new Item<object>(caption, obj, objs, Formats.Object);
        //    item.AddNewRequest = addNewRequest;
        //    AddItem(item);
        //}

        public void AddObject(string caption, object? value, object[]? values, Action? addNewRequest = null)
        {
            Item item = new Item<object>(caption, value, values == null ? new List<object>() : new List<object>(values), Formats.Object);
            item.AddNewRequest = addNewRequest;
            AddItem(item);
        }

        public void AddGuidnom(string caption, GuidNom? value, List<GuidNom>? values, Action? addNewRequest = null)
        {
            Item item = new Item<GuidNom>(caption, value, values ?? new(), Formats.Guidnom);
            AddItem(item);
        }
        public void AddModel(string caption, Guid? guid, IModel[]? values, Action? addNewRequest = null)
        {
            var value = values?.FirstOrDefault(x => x.Guid == guid);
            Item item = new Item<IModel>(caption, value, values?.ToList() ?? new(), Formats.IModel);
            item.AddNewRequest = addNewRequest;
            AddItem(item);
        }
        public void AddModel<T>(string caption, Guid? guid, List<T>? values, Action? addNewRequest = null) where T : IModel
        {
            var iModels = values?.Select(x => (IModel)x);
            var iModel = iModels?.FirstOrDefault(x => x.Guid == guid);
            Item item = new Item<IModel>(caption, iModel, iModels?.ToList() ?? new(), Formats.IModel);
            item.AddNewRequest = addNewRequest;
            AddItem(item);
        }
        public void AddModelList(string caption, List<IModel>? availableValues, List<IModel>? selectedValues, Action? addNewRequest = null)
        {
            Item item = new Item<List<IModel>>(caption, selectedValues, Formats.IModelList);
            item.AvailableValues = availableValues;
            item.SelectedValues = selectedValues; // es redundant amb Value
            item.AddNewRequest = addNewRequest;
            AddItem(item);
        }
        public void AddIdNoms(string caption, List<IdNom>? availableValues, List<IdNom>? selectedValues, Action? addNewRequest = null)
        {
            Item item = new Item<List<IdNom>>(caption, selectedValues, Formats.IdNomList);
            item.AvailableIdNoms = availableValues;
            item.SelectedIdNoms = selectedValues; // es redundant amb Value
            item.AddNewRequest = addNewRequest;
            AddItem(item);
        }

        public void AddContacts(string caption, List<ContactModel>? availableContacts, List<ContactModel>? selectedContacts, Action? addNewRequest = null)
        {
            var availableValues = availableContacts?.Select(x => (IModel)x).ToList();
            var selectedValues = selectedContacts?.Select(x => (IModel)x).ToList();
            AddModelList(caption, availableValues, selectedValues, addNewRequest);
        }
        public void AddModels<T>(string caption, List<T>? availableModels, List<T>? selectedModels, Action? addNewRequest = null) where T : IModel
        {
            var availableValues = availableModels?.Select(x => (IModel)x).ToList();
            var selectedValues = selectedModels?.Select(x => (IModel)x).ToList();
            AddModelList(caption, availableValues, selectedValues, addNewRequest);
        }

        public void AddEnum<T>(string caption, T? value = default) where T : struct, Enum
        {
            var values = new List<IdNom>();
            foreach (int v in Enum.GetValues(typeof(T)))
                values.Add(new IdNom { Id = v, Nom = Enum.GetName(typeof(T), v) });
            var idNomValue = values.FirstOrDefault(x => x.Id == Convert.ToInt32(value));
            Item item = new Item<IdNom>(caption, idNomValue, values, Formats.IdNom);
            AddItem(item);
        }

        public void AddEnums<T>(string caption, List<T>? selectedEnums) where T : struct, Enum
        {
            var availableValues = new List<IdNom>();
            foreach (int v in Enum.GetValues(typeof(T)))
                availableValues.Add(new IdNom { Id = v, Nom = Enum.GetName(typeof(T), v) });

            var selectedInts = selectedEnums?.Select(x => Convert.ToInt32(x)).ToList();
            var selectedValues = availableValues.Where(x => selectedInts?.Contains(x.Id) ?? false).ToList();

            AddIdNoms(caption, availableValues, selectedValues);


            //var availableValues = availableModels?.Select(x => (IModel)x).ToList();
            //var selectedValues = selectedModels?.Select(x => (IModel)x).ToList();
            //AddModelList(caption, availableValues, selectedValues, addNewRequest);
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

        public object? GetObject(int idx) => ((Item<object>)Items.First(x => x.Field == idx)!).Value;
        public GuidNom? GetGuidNom(int idx) => ((Item<GuidNom>)Items.First(x => x.Field == idx)!).Value;
        public Guid? GetGuid(int idx) => ((Item<GuidNom>?)Items.FirstOrDefault(x => x.Field == idx))?.Value?.Guid;
        public Guid? GetModelGuid(int idx) => ((Item<IModel>?)Items.FirstOrDefault(x => x.Field == idx))?.Value?.Guid;
        public IModel? GetModel(int idx) => ((Item<IModel>?)Items.FirstOrDefault(x => x.Field == idx))?.Value;
        public T? GetModel<T>(int idx)
        {
            var item = (Item<IModel>)Items.First(x => x.Field == idx)!;
            var model = item.Value;
            if (model is T)
                return (T)model;
            else
                return default;
        }
        public List<T> GetModels<T>(int idx)
        {
            var item = Items.First(x => x.Field == idx);
            var models = item.SelectedValues?.Select(x => (T)x).ToList() ?? new();
            return models;
        }

        public List<T> GetEnumList<T>(int idx) where T : Enum
        {
            var item = Items.First(x => x.Field == idx);
            var ints = item.SelectedIdNoms?.Select(x => x.Id).ToList();
            return ints?.Select(id => (T)Enum.ToObject(typeof(T), id))?.ToList() ?? new();
        }
        public List<ContactModel>? GetContacts(int idx) => Items.First(x => x.Field == idx)!.SelectedValues?.Select(x => (ContactModel)x).ToList();
        public int? IdNom(int idx) => ((Item<IdNom>)Items.First(x => x.Field == idx)!).Value?.Id;

        public string? GetNumericDigits(int idx)
        {
            var value = Value<string>(idx);
            var retval = string.IsNullOrEmpty(value) ? null : new String(value.Where(Char.IsDigit).ToArray());
            return retval;
        }

        public BaseQuotaModel? GetBaseQuota(int idx)
        {
            var item = (Item<BaseQuotaModel>)Items.First(x => x.Field == idx)!;
            return item.Value;
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

        public int? GetEnum(int idx)
        {
            int? retval = null;
            var item = (Item<IdNom>)Items.First(x => x.Field == idx)!;
            if (item.Value != null)
                retval = ((IdNom)item.Value).Id;
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
            if (!string.IsNullOrEmpty(item.Value) && decimal.TryParse(item.Value, NumberStyles.Float, Culture, out tmp))
                retval = tmp;
            return retval;
        }
        public decimal? GetDecimal(int idx)
        {
            decimal? retval = null;
            decimal tmp;
            var item = (Item<string>)Items.First(x => x.Field == idx)!;
            if (!string.IsNullOrEmpty(item.Value) && decimal.TryParse(item.Value, NumberStyles.Float, Culture, out tmp))
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
            public bool ReadOnly { get; set; }
            public string? WarningMessage { get; set; }

            public Action? AddNewRequest { get; set; }

            public List<IModel>? SelectedValues { get; set; }
            public List<IModel>? AvailableValues { get; set; }
            public List<IdNom>? SelectedIdNoms { get; set; }
            public List<IdNom>? AvailableIdNoms { get; set; }

            public int MaxLength { get; set; }

            public Item(string caption, List<IModel>? availableValues, List<IModel>? selectedValues)

            {
                //new Item(caption, Formats.IModelList);
                Caption = caption;
                Format = Formats.IModelList;
                AvailableValues = availableValues ?? new();
                SelectedValues = selectedValues ?? new();
            }

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

            //public int? MaxLength { get; set; }

            public Item(string caption, T? value, Formats format) : base(caption, format)
            {
                Value = value;
            }
            public Item(string caption, T? value, List<T> values, Formats format) : base(caption, format)
            {
                Value = value;
                Values = values ?? new();
            }

            public bool IsOverLength()
            {
                var retval = MaxLength > 0 && !string.IsNullOrEmpty(Value?.ToString()) && Value!.ToString()!.Length > MaxLength;
                return retval;
            }
        }


    }
}
