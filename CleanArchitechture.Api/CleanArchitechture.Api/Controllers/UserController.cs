using CleanArchitechture.Api.Controllers.Base;
using CleanArchitechture.Core.Dtos;
using CleanArchitechture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitechture.Api.Controllers;

public class UserController : BaseApiController
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IAccountService _accountService;

    public UserController(ICurrentUserService currentUserService, IAccountService accountService)
    {
        _currentUserService = currentUserService;
        _accountService = accountService;
    }

    [HttpPost("UserCreate")]
    public async Task<ActionResult<object>> Create(UserCreateDto model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        await _accountService.CreateUser(model);
        return OkResult(model);
    }
}