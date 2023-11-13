using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Enums;
using CleanArchitechture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitechture.Services;

public class DataService:IDataService
{
    private readonly RoleManager<AppRole> _role;
    private readonly UserManager<AppUser> _user;

    public DataService(RoleManager<AppRole> role, UserManager<AppUser> user)
    {
        _role = role;
        _user = user;
    }

    public async Task<bool> SeedData()
    {
        var isSuccess = true;
        
        var isSeedRoleSuccess = await SeedRoles();
        if (!isSeedRoleSuccess) isSuccess = false;

        var isUserSeedSuccess = await SeedUser();
        if (!isUserSeedSuccess) isSuccess = false;

        return isSuccess;
    }

    private async Task<bool> SeedUser()
    {
        var users = new[]
        {
            new AppUser()
            {
                UserName = "System.Admin",
                Email = "System.Admin@gmail.com",
                Name= "System.Admin"
            }
        };

        try
        {
            var existingUsers = await _user.Users.ToListAsync();

            foreach (var user in users)
            {
                var existingUser = existingUsers.FirstOrDefault(x => x.UserName == user.UserName);
                if (existingUser is null)
                {
                    var result = await _user.CreateAsync(user, "123456");
                }
                else
                {
                    var isUpdated = false;
                    if (existingUser.UserName != user.UserName)
                    {
                        existingUser.UserName = user.UserName;
                        isUpdated = true;
                    }
                    if (isUpdated) await _user.UpdateAsync(existingUser);
                }

            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    private async Task<bool> SeedRoles()
    {
        var roles = new AppRole[]
        {
            new AppRole()
            {
                Name = Roles.Admin.ToString(),
                Code = "R001",
            },
            new AppRole()
            {
                Name = Roles.User.ToString(),
                Code = "R002",
            }
        };
        try
        {

            var existingRoles = await _role.Roles.ToListAsync();

            foreach (var role in roles)
            {
                var existingRole = existingRoles.FirstOrDefault(x => x.Code == role.Code);
                if (existingRole is null)
                {
                    await _role.CreateAsync(role);
                }
                else
                {
                    var isUpdated = false;

                    if (role.Name != existingRole.Name)
                    {
                        existingRole.Name = role.Name;
                        isUpdated = true;
                    }

                    if (isUpdated)
                    {
                        await _role.UpdateAsync(existingRole);
                    }
                }
                
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}