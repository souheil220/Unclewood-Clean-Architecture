namespace UnclewoodCleanArchitecture.Infrastructure.Authentication;

public class AuthenticationOptions
{
    public string Audience { get; set; } = string.Empty;
    public string MetaDataUrl { get; set; } = string.Empty;
    public bool RequireHttpsMetaData { get; set; }
    public string ValidIssuer { get; set; } = string.Empty;
}