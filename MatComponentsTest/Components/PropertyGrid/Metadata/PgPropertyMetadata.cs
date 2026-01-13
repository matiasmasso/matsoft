using Microsoft.AspNetCore.Components.Forms;
using PropertyGridTest.Components.PropertyGrid.Attributes;
using System.Reflection;

namespace PropertyGridTest.Components.PropertyGrid.Metadata
{
    public class PgPropertyMetadata
    {
        public PropertyInfo Property { get; }
        public string Label { get; }
        public int Order { get; }
        public string Category { get; }
        public Type CustomEditor { get; }
                public FieldIdentifier Field { get; set; }


        public bool IsComplexType =>
    !Property.PropertyType.IsPrimitive &&
    Property.PropertyType != typeof(string) &&
    !Property.PropertyType.IsEnum &&
    Property.PropertyType != typeof(DateTime) &&
    !Property.PropertyType.IsValueType;

        public PgPropertyMetadata(PropertyInfo prop)
        {
            Property = prop;

            Label = prop.GetCustomAttribute<PgDisplayAttribute>()?.Label ?? prop.Name;
            Order = prop.GetCustomAttribute<PgDisplayAttribute>()?.Order ?? 0;
            Category = prop.GetCustomAttribute<PgCategoryAttribute>()?.Category ?? "General";
            CustomEditor = prop.GetCustomAttribute<PgEditorAttribute>()?.EditorType;
        }
    }
}
