using System.ComponentModel.DataAnnotations;

namespace CleanArchitechture.Core.Types;

public class LoginPayload
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}