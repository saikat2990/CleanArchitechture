namespace CleanArchitechture.Core.Types;

public class LoginResult
{
    public string Token { get; set; }
    public string Id { get; set; }
    public string EmailAddress { get; set; }
    public List<string> Roles { get; set; }
}