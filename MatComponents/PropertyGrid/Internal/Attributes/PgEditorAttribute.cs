namespace MatComponents.PropertyGrid.Internal.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PgEditorAttribute : Attribute
    {
        public Type EditorType { get; }

        public PgEditorAttribute(Type editorType)
        {
            EditorType = editorType;
        }
    }
}
