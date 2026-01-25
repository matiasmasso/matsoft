using MatComponents.Components.PropertyGrid.Internal.Metadata;

namespace MatComponents.Components.PropertyGrid.Internal;


    public class CategoryModel
    {
        public string Name { get; set; }
        public bool Expanded { get; set; } = true;
        public List<PgPropertyMetadata> Properties { get; set; } = new();
    }

