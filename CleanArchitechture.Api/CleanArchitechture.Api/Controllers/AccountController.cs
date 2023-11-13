using CleanArchitechture.Api.Controllers.Base;
using CleanArchitechture.Core.Interfaces.Services;
using CleanArchitechture.Core.Types;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitechture.Api.Controllers;

public class AccountController : BaseApiController
{
    private readonly IAuthService _auth;
    public AccountController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult<LoginResult>>> Login(LoginPayload payload)
    {
        var result = await _auth.LoginAsync(payload);
        return OkResult(result);
    }
}