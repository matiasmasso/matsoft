[AttributeUsage(AttributeTargets.Property)]
public class PgOptionsAttribute : Attribute
{
    public string[] Items { get; }

    public PgOptionsAttribute(params string[] items)
    {
        Items = items;
    }
}