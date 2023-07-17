using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserRepository _userRepository;
    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserRepository userRepository) : base(options, logger, encoder, clock)
    {
        _userRepository = userRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var token = Request.Headers["Authorization"].ToString().Split(" ").Last();
        if (token != null)
        {
            var user = await _userRepository.FindUserByToken(token);
            if (user == null)
            {
                Response.StatusCode = 401;
                return AuthenticateResult.Fail("توکن به اشتباه وارد شده است");
            }
            Request.Headers["user"] = JsonConvert.SerializeObject(user);
            var claims = new[] { new Claim("token", token) };
            var identity = new ClaimsIdentity(claims, "token");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
        }
        Response.StatusCode = 401;
        return AuthenticateResult.Fail("توکن به اشتباه وارد شده است");
    }
}