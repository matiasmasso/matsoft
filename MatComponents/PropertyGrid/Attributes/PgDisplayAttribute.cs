namespace MatComponents.PropertyGrid.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PgDisplayAttribute : Attribute
    {
        public string Label { get; }
        public int Order { get; set; } = 0;

        public PgDisplayAttribute(string label)
        {
            Label = label;
        }
    }
}
