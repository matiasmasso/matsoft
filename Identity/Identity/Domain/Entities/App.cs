namespace Identity.Domain.Entities
{
    public class App
    {
        public Guid AppId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<AppRole> AppRoles { get; set; } = new List<AppRole>();
    }

}
