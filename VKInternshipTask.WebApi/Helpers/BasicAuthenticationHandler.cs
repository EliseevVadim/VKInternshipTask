using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using VKInternshipTask.Application.Features.Users.Queries.GetAuthorizedUser;

namespace VKInternshipTask.WebApi.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IMediator _mediator;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IMediator mediator)
        : base(options, logger, encoder, clock)
        {
            _mediator = mediator;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            if (authHeader.Scheme != "Basic")
                return AuthenticateResult.Fail("Invalid Authorization Scheme");

            byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? string.Empty);
            string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            string login = credentials[0];
            string password = credentials[1];

            var user = await _mediator.Send(new GetAuthorizedUserQuery()
            {
                Login = login,
                Password = password
            });

            if (user == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            Claim[] claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
