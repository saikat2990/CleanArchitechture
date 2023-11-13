using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Exceptions;
using CleanArchitechture.Core.Interfaces.Services;
using CleanArchitechture.Core.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitechture.Services;

public class AuthService:IAuthService
{
    private readonly UserManager<AppUser> _user;
    private readonly SignInManager<AppUser> _signIn;

    public AuthService(UserManager<AppUser> user, SignInManager<AppUser> signIn)
    {
        _user = user;
        _signIn = signIn;
    }

    public async Task<LoginResult> LoginAsync(LoginPayload request)
    {
        AppUser user = await _user.FindByNameAsync(request.UserName);
        if (user is null)
        {
            throw new CustomException($"Authentication failed.Incorrect UserName");
        }

        var result = await _signIn.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            throw new CustomException($"Authentication failed. Incorrect Password");
        }

        var response = await GetAuthResponse(user);
        return response ;
    }

    private async Task<LoginResult> GetAuthResponse(AppUser user)
    {
        JwtSecurityToken jwtSecurityToken = await GenerateJwtToken(user);

        LoginResult response = new LoginResult();
        response.Id = user.Id;
        response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        response.EmailAddress = user.Email;
        var rolesList = await _user.GetRolesAsync(user).ConfigureAwait(false);
        response.Roles = rolesList.ToList();
        return response;
    }

    private async Task<JwtSecurityToken> GenerateJwtToken(AppUser user)
    {
        var userClaims = await _user.GetClaimsAsync(user);
        var roles = await _user.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: JwtSettings.Issuer,
            audience: JwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(JwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}