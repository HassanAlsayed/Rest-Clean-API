using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HR.LeaveManagmant.Domain.Entities.Account;

public class UserInfo
{

    [JsonIgnore]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
