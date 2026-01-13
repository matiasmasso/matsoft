using MatComponents.PropertyGrid.Internal.Attributes;
using Microsoft.AspNetCore.Components.Forms;
using System.Reflection;

namespace MatComponents.PropertyGrid.Internal.Metadata;

public class PgPropertyMetadata
{
    public PropertyInfo Property { get; }
    public string PropertyName => Property.Name;
    public Type PropertyType => Property.PropertyType;

    public string Label { get; }
    public int Order { get; }
    public string Category { get; }
    public Type? CustomEditor { get; }

    public FieldIdentifier Field { get; set; }

    public IEnumerable<object>? LookupValues { get; set; }
    public Func<string, Task<IEnumerable<object>>>? SearchFunc { get; set; }

    public bool IsComplexType =>
        !PropertyType.IsPrimitive &&
        PropertyType != typeof(string) &&
        !PropertyType.IsEnum &&
        PropertyType != typeof(DateTime) &&
        !PropertyType.IsValueType;

    public PgPropertyMetadata(PropertyInfo prop)
    {
        Property = prop;

        Label = prop.GetCustomAttribute<PgDisplayAttribute>()?.Label ?? prop.Name;
        Order = prop.GetCustomAttribute<PgDisplayAttribute>()?.Order ?? 0;
        Category = prop.GetCustomAttribute<PgCategoryAttribute>()?.Category ?? "General";
        CustomEditor = prop.GetCustomAttribute<PgEditorAttribute>()?.EditorType;
    }
}