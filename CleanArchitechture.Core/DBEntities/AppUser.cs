

using Microsoft.AspNetCore.Identity;

namespace CleanArchitechture.Core.DBEntities;

public class AppUser: IdentityUser
{
    public string TeamName { get; set; }
    public string FullName { get; set; }
}