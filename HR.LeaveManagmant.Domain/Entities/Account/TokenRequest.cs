using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagmant.Domain.Entities.Account;

public class TokenRequest
{
    [Required]
    public string Token { get; set; }
    [Required]
    public string Refreshtoken { get; set; }
}
