using CleanArchitechture.Core.Dtos;
using CleanArchitechture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using CleanArchitechture.Core.DBEntities;
using AutoMapper;
using CleanArchitechture.Core.Enums;
using CleanArchitechture.Services.RoleBasedAccountCreation;

namespace CleanArchitechture.Services;

public class AccountService: IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    public AccountService(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task CreateUser(UserCreateDto model)
    {
        var mappedAccount = _mapper.Map<AppUser>(model);
        var result = await _userManager.CreateAsync(mappedAccount,model.Password);
        if (result.Succeeded)
        {
            var getUser = _userManager.Users.FirstOrDefault(x => x.Email.Equals(model.Email));
            var roleManager = new RoleCreation(_userManager);
            if (getUser != null)
                await roleManager.UserRoleCreate(getUser, Roles.User.ToString());
        }
    }
}