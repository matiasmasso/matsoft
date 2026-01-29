public sealed class UserApp
{
    public Guid UserId { get; set; }
    public Guid AppId { get; set; }

    public User User { get; set; } = default!;
    public App App { get; set; } = default!;

}