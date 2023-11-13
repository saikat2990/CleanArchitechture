using Microsoft.AspNetCore.Identity;

namespace CleanArchitechture.Core.DBEntities;

public class AppRole:IdentityRole
{
    public string Code { get; set; }
}