namespace Spa4.Shared
{
    public class Option
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public object? Tag { get; set; }

        public Option(string id = "", string nom = "", object? tag=null)
        {
            Id = id;
            Nom = nom;
            Tag = tag;
        }

        public override string ToString() => Nom;
    }
}
