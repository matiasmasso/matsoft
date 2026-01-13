using MatComponents.PropertyGrid;
using MatComponents.PropertyGrid.Internal.Attributes;
using MatComponents.PropertyGrid.Internal.Metadata;
using System.Reflection;
using System.Xml.Linq;

namespace MatComponents.PropertyGrid.Internal;

public static class ReflectionHelpers
{
    public static List<PgPropertyMetadata> GetMetadata(object model)
    {
        if (model == null)
            return new();

        return model
            .GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite)
            .Where(p => !p.IsDefined(typeof(PgIgnoreAttribute)))
            .Select(p => new PgPropertyMetadata(p))
            .OrderBy(p => p.Order)
            .ToList();
    }
}