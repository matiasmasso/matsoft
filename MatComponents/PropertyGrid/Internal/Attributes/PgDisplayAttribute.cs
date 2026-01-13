namespace MatComponents.PropertyGrid.Internal.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PgDisplayAttribute : Attribute
    {
        public string Label { get; set; } = "";
        public int Order { get; set; }

        public PgDisplayAttribute() { }

        public PgDisplayAttribute(string label)
        {
            Label = label;
        }
    }

}
