
using System.Security.Claims;
using CleanArchitechture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace CleanArchitechture.Services;

public class CurrentUserService: ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)!.Value!;
        UserName = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)!.Value!;
        Email = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)!.Value!;
        Role = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)!.Value! ?? 
            httpContextAccessor.HttpContext?.User.FindFirst("roles")!.Value!;
    }
    public string UserId { get; }
    public string UserName { get; }
    public string Email { get; }
    public string Role { get; }
}