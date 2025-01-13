using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace UnclewoodCleanArchitecture.Infrastructure.Authentication;

public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly AuthenticationOptions _authenticationOptions;

    public JwtBearerOptionsSetup(IOptions<AuthenticationOptions> authenticationOptions)
    {
        _authenticationOptions = authenticationOptions.Value;
    }

    public void Configure(JwtBearerOptions options)
    {
        options.Audience = _authenticationOptions.Audience;
        options.MetadataAddress = _authenticationOptions.MetaDataUrl;
        options.RequireHttpsMetadata = _authenticationOptions.RequireHttpsMetaData;
        options.TokenValidationParameters.ValidIssuer = _authenticationOptions.ValidIssuer;
        options.UseSecurityTokenValidators = true;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }
}