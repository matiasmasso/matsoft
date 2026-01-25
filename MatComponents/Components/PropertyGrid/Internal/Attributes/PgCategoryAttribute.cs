namespace MatComponents.Components.PropertyGrid.Internal.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PgCategoryAttribute : Attribute
    {
        public string Category { get; }

        public PgCategoryAttribute(string category)
        {
            Category = category;
        }
    }
}
