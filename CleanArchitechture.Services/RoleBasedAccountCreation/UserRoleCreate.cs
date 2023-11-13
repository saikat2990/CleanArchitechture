using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitechture.Services.RoleBasedAccountCreation;
public class UserRoleCreate : BaseRoleCreate
{
    public UserRoleCreate(UserManager<AppUser> userManager) : base(userManager)
    {
    }

    public override async Task AccountRoleAdd(AppUser account)
    {
        await _userManager.AddToRoleAsync(account, Roles.User.ToString());
    }
}