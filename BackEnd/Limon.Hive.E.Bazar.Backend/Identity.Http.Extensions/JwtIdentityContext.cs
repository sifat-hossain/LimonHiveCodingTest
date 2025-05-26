using Identity.Http.Extensions.Token;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Identity.Http.Extensions
{
    public class JwtIdentityContext : IIdentityContext
    {
        private readonly IAuthorizationTokenProvider _authorizationTokenProvider;
        private readonly IOptions<IdentityContextOptions> _options;

        public JwtIdentityContext(IAuthorizationTokenProvider authorizationTokenProvider,
            IOptions<IdentityContextOptions> options)
        {
            _authorizationTokenProvider = authorizationTokenProvider;
            _options = options;

            GetTokenClaims();
        }

        public string UserName { get; private set; }
        public string AuthorizationHeader { get; private set; }
        public string AuthorizationScheme { get; private set; }

        private void GetTokenClaims()
        {
            (bool successful, IAuthorizationToken token) = _authorizationTokenProvider.TryGet();

            if (!successful)
            {
                return;
            }

            if (!(GetPrincipal(token) is ClaimsPrincipal principal))
            {
                return;
            }

            AuthorizationHeader = token.Value;
            AuthorizationScheme = token.Type;

            UserName = principal.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;

        }

        private IPrincipal GetPrincipal(IAuthorizationToken token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(token.Value);
                if (jwtSecurityToken == null)
                {
                    return null;
                }

                var param = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecurityKey))
                };

                return handler.ValidateToken(token.Value, param, out SecurityToken securityToken);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}