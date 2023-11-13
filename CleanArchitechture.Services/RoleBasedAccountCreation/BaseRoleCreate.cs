using Microsoft.AspNetCore.Identity;
using CleanArchitechture.Core.DBEntities;

namespace CleanArchitechture.Services.RoleBasedAccountCreation;
public abstract class BaseRoleCreate
{
    protected readonly UserManager<AppUser> _userManager;

    public BaseRoleCreate(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public abstract Task AccountRoleAdd(AppUser account);
}