using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace T_Shop.Client.MVC.Extensions
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.TryGetValue("AuthToken", out var token))
            {
                //context.Request.Headers.Add("Authorization", $"Bearer {token}");
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken != null)
                {
                    var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);
                    var claimObjects = claims.Select(c => new Claim(c.Key, c.Value));
                    var claimsIdentity = new ClaimsIdentity(claimObjects, JwtBearerDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(claimsIdentity);
                    context.User = principal;
                }
            }
            await _next(context);
        }
    }
}
