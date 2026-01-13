

namespace DTO
{
    public class StringLocalizerModel
    {
        public string StringKey { get; set; } = string.Empty;
        public List<Item> Items { get; set; } = new();
        public bool IsNew { get; set; }

        public StringLocalizerModel() { }
        public StringLocalizerModel(string key)
        {
            StringKey = key;
        }

        public string Tradueix(LangDTO lang)
        {
            var retval = Items.FirstOrDefault(x => x.LangTag == lang.Tag())?.Value;
            if (retval == null)
            {
                retval = Items.FirstOrDefault(x => x.LangTag == "ESP")?.Value;
                if (retval == null) retval = StringKey;
            }
            return retval;
        }

        public string GetValue(LangDTO.Ids langId) => Items.FirstOrDefault(x => x.LangTag == langId.ToString())?.Value ?? "";
        public void AddItem(string langTag, string value)
        {
            Items.Add(new Item
            {
                LangTag = langTag,
                Value = value
            });
        }

        public bool Matches(string? searchTerm)
        {
            return searchTerm == null
                || StringKey.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
                || Items.Any(x => x.Value.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
        }
        public override string ToString() => string.Format("{0}: {1},{2},{3},{4}"
                , StringKey
                , Items.FirstOrDefault(x => x.LangTag == "ESP")?.Value
                , Items.FirstOrDefault(x => x.LangTag == "CAT")?.Value
                , Items.FirstOrDefault(x => x.LangTag == "ENG")?.Value
                , Items.FirstOrDefault(x => x.LangTag == "POR")?.Value);

        public class Item
        {
            public string LangTag { get; set; }
            public string Value { get; set; }
            public Item(string? langtag = null, string? value = null)
            {
                LangTag = langtag ?? LangDTO.Esp().Tag();
                Value = value ?? "";
            }

            public override string ToString() => string.Format("{0}: {1}", LangTag, Value);
        }

        public static List<StringLocalizerModel> FromExcel(DTO.Excel.Sheet sheet)
        {
            var retval = new List<StringLocalizerModel>();
            if (sheet.Rows.Count > 0)
            {
                var row = sheet.Rows.First();
                var keyCol = row.Cells.FirstOrDefault(x => x.Content == "StringKey");
                if (keyCol == null) throw new Exception("falta la columna StringKey");

                var espCol = row.Cells.FirstOrDefault(x => x.Content == "Esp");
                var catCol = row.Cells.FirstOrDefault(x => x.Content == "Cat");
                var engCol = row.Cells.FirstOrDefault(x => x.Content == "Eng");
                var porCol = row.Cells.FirstOrDefault(x => x.Content == "Por");
                var keyIdx = row.Cells.IndexOf(keyCol);
                var espIdx = espCol == null ? -1 : row.Cells.IndexOf(espCol);
                var catIdx = catCol == null ? -1 : row.Cells.IndexOf(catCol);
                var engIdx = engCol == null ? -1 : row.Cells.IndexOf(engCol);
                var porIdx = porCol == null ? -1 : row.Cells.IndexOf(porCol);

                for (var i = 1; i < sheet.Rows.Count; i++)
                {
                    row = sheet.Rows[i];
                    if (HasValue(row, keyIdx))
                    {
                        var value = new StringLocalizerModel(row.Cells[keyIdx].Content!.ToString()!);
                        if (HasValue(row, espIdx))
                            value.AddItem("ESP", row.Cells[espIdx].Content!.ToString()!);
                        if (HasValue(row, catIdx))
                            value.AddItem("CAT", row.Cells[catIdx].Content!.ToString()!);
                        if (HasValue(row, engIdx))
                            value.AddItem("ENG", row.Cells[engIdx].Content!.ToString()!);
                        if (HasValue(row, porIdx))
                            value.AddItem("POR", row.Cells[porIdx].Content!.ToString()!);
                        retval.Add(value);
                    }
                }
            }
            return retval;
        }

        private static bool HasValue(DTO.Excel.Row row, int cellIdx) => row.Cells.Count > cellIdx && !string.IsNullOrEmpty(row.Cells[cellIdx].Content?.ToString());

    }
}
