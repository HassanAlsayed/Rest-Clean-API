namespace HR.LeaveManagmant.Domain.Entities.Account;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public Guid JwtId { get; set; }
    public bool IdUsed { get; set; }
    public bool IdRevoked { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}
