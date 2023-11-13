namespace CleanArchitechture.Core.Types;
public class JwtSettings
{
    public static string Key { get; set; }
    public static string Issuer { get; set; }
    public static string Audience { get; set; }
    public static double DurationInMinutes { get; set; }
}