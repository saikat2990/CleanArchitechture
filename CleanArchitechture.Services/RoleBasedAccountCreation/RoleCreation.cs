using CleanArchitechture.Core.DBEntities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitechture.Services.RoleBasedAccountCreation;
public class RoleCreation
{
    private readonly UserManager<AppUser> _userManager;
    public RoleCreation(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task UserRoleCreate(AppUser AppUser, string role)
    {
        switch (role)
        {
            case "User":
                await new UserRoleCreate(_userManager).AccountRoleAdd(AppUser);
                break;
            default:
                break;
        }
        
    }
}