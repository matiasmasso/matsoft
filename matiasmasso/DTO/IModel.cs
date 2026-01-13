namespace DTO
{
    public interface IModel
    {
        public Guid Guid { get; set; }
        //public bool IsNew { get; set; }
        //public string Caption();

        //public string PropertyPageUrl();

        public bool Matches(string? searchTerm);
    }
}