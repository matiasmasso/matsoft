namespace Identity.DTO;

public class ActivateUserRequest
    {
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
    }
