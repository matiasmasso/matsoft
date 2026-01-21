namespace Identity.DTO;

public class ConfirmEmailRequest
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }

