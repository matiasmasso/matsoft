
using MatComponents.PropertyGrid.Internal.Metadata;

namespace MatComponents.PropertyGrid.Internal;

public class ObjectNode
{
    public string Label { get; set; }
    public object Instance { get; set; }
    public bool Expanded { get; set; } = false;
    public List<PgPropertyMetadata> Properties { get; set; } = new();
}
