using MatComponents.PropertyGrid.Internal.Metadata;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MatComponents.PropertyGrid;

public static class ReflectionHelpers
{
    public static List<PgPropertyMetadata> GetMetadata(object instance)
    {
        var type = instance.GetType();
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var list = new List<PgPropertyMetadata>();

        foreach (var prop in props)
        {
            // Skip indexers or non-readable properties
            if (prop.GetIndexParameters().Length > 0)
                continue;

            if (!prop.CanRead)
                continue;

            // Construct metadata with the PropertyInfo-based constructor
            var meta = new PgPropertyMetadata(prop);

            list.Add(meta);
        }

        return list;
    }

    private static string GetLabel(PropertyInfo prop)
    {
        var display = prop.GetCustomAttribute<DisplayAttribute>();
        if (display != null && !string.IsNullOrWhiteSpace(display.Name))
            return display.Name;

        var displayName = prop.GetCustomAttribute<DisplayNameAttribute>();
        if (displayName != null)
            return displayName.DisplayName;

        return prop.Name;
    }

    private static string GetCategory(PropertyInfo prop)
    {
        var cat = prop.GetCustomAttribute<CategoryAttribute>();
        if (cat != null)
            return cat.Category;

        return "General";
    }

    private static int GetOrder(PropertyInfo prop)
    {
        var display = prop.GetCustomAttribute<DisplayAttribute>();
        if (display != null && display.GetOrder().HasValue)
            return display.GetOrder().Value;

        return 0;
    }
}
