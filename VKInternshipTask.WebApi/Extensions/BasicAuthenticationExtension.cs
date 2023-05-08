using Microsoft.AspNetCore.Authentication;
using VKInternshipTask.WebApi.Helpers;

namespace VKInternshipTask.WebApi.Extensions
{
    public static class BasicAuthenticationExtension
    {
        public static AuthenticationBuilder AddBasicAuthentication(this AuthenticationBuilder builder)
        {
            builder.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
            return builder;
        }
    }
}
