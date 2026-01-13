namespace Cash.Models
{
    public class NavigationArgs
    {
        public object? Value { get; set; }
        public Codis Codi { get; set; }

        public enum Codis
        {
            ImportNorma43,
            ImportMedia
        }

    }
}
