using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

public class AdminAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IAdminRepository _adminRepository;
    public AdminAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IAdminRepository adminRepository) : base(options, logger, encoder, clock)
    {
        _adminRepository = adminRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var token = Request.Headers["Authorization"].ToString().Split(" ").Last();
        if (token != null)
        {
            var admin = await _adminRepository.getAdminByToken(token);
            if (admin == null)
            {
                Response.StatusCode = 401;
                return AuthenticateResult.Fail("توکن به اشتباه وارد شده است");
            }
            Request.Headers["admin"] = JsonConvert.SerializeObject(admin);
            var claims = new[] { new Claim("token", token) };
            var identity = new ClaimsIdentity(claims, "token");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
        }
        Response.StatusCode = 401;
        return AuthenticateResult.Fail("توکن به اشتباه وارد شده است");
    }
}