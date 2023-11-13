

using Microsoft.AspNetCore.Identity;

namespace CleanArchitechture.Core.DBEntities;

public class AppUser: IdentityUser
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}