using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagmant.Domain.Entities.Account;

public class LoginInfo
{
    [Required,EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password{ get; set; }
}
