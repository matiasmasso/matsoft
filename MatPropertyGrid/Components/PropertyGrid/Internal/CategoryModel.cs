using PropertyGridTest.Components.PropertyGrid.Metadata;

namespace PropertyGridTest.Components.PropertyGrid.Internal
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public bool Expanded { get; set; } = true;
        public List<PgPropertyMetadata> Properties { get; set; } = new();
    }
}
