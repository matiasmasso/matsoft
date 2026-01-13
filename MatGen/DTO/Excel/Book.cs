namespace DTO.Excel
{
    public class Book
    {
        public string? Filename { get; set; } 
        public List<Sheet> Sheets { get; set; } = new();

        public Book(string? filename = null)
        {
            Filename = filename;
        }

        public Sheet AddSheet(string? name = null)
        {
            if (string.IsNullOrEmpty(name)) name = string.Format("Sheet {0}", Sheets.Count + 1);
            var retval = new Sheet(name);
            Sheets.Add(retval);
            return retval;
        }

        public override string ToString() => String.Format("{DTO.Excel.Book} '{0}'", Filename);
    }
}
