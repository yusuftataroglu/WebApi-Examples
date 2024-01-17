using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace NumberGuessingGameWebApi.Helpers
{
    public class BasicAuth : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public BasicAuth(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, UserManager<IdentityUser> userManager) : base(options, logger, encoder, clock)
        {
            _userManager = userManager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var auth = Request.Headers.ContainsKey("Authorization");
            if (auth)
            {
                var headerValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(headerValue.Parameter);
                var credential = Encoding.UTF8.GetString(bytes);

                var array = credential.Split(':');
                var username = array[0];
                var password = array[1];

                var user = await _userManager.FindByNameAsync(username);
                if (user != null)
                {
                    var validatePassword = await _userManager.CheckPasswordAsync(user, password);
                    if (validatePassword)
                    {
                        var claims = new[] { new Claim(ClaimTypes.Name, username) };
                        var identity = new ClaimsIdentity(claims, Scheme.Name);
                        var principal = new ClaimsPrincipal(identity);
                        var ticket = new AuthenticationTicket(principal, Scheme.Name);
                        return AuthenticateResult.Success(ticket);
                    }
                    else
                    {
                        return AuthenticateResult.Fail("Yetkisiz Giriş!");
                    }
                }
                else
                {
                    return AuthenticateResult.Fail("Böyle bir kullanıcı bulunmamaktadır.");
                }
            }   
            else
            {
                return AuthenticateResult.Fail("Yetkisiz Giriş!");
            }

        }
    }
}
