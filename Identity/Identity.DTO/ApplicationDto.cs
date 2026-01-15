namespace Identity.DTO;

    public class ApplicationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ClientId { get; set; }
        public bool IsActive { get; set; }
    }
