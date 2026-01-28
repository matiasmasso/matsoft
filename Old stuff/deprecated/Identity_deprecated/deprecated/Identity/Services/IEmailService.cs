namespace Identity.Services
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(string email, string verificationLink);
    }

}
