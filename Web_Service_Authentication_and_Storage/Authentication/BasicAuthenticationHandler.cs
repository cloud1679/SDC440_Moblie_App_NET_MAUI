using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Web_Service_Authentication_and_Storage.Authentication;

public sealed class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger, UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!AuthenticationHeaderValue.TryParse(Request.Headers.Authorization, out var header)
            || !string.Equals(header.Scheme, "Basic", StringComparison.OrdinalIgnoreCase)
            || string.IsNullOrEmpty(header.Parameter))
            return Task.FromResult(AuthenticateResult.NoResult());

        try
        {
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(header.Parameter));
            // Fixed credentials are required by this classroom assignment.
            if (credentials != "Burns01:Password1")
                return Task.FromResult(AuthenticateResult.Fail("Invalid user name or password."));

            var identity = new ClaimsIdentity([new Claim(ClaimTypes.Name, "Burns01")], Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(
                new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name)));
        }
        catch (FormatException)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Basic authentication header."));
        }
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        Response.Headers.WWWAuthenticate = "Basic realm=\"Assessment\", charset=\"UTF-8\"";
        return Task.CompletedTask;
    }
}
