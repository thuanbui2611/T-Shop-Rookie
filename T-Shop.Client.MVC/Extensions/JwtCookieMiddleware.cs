using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using T_Shop.Shared.DTOs.User.ResponseModels;

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
                context.Request.Headers.Add("Authorization", $"Bearer {token}");

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken != null)
                {
                    var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                    var userId = claims.GetValueOrDefault("UserId");
                    var user = new UserResponseModel
                    {
                        Id = !userId.IsNullOrEmpty() ? new Guid(userId) : Guid.Empty,
                        Full_name = claims.GetValueOrDefault("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"),
                        Email = claims.GetValueOrDefault("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"),
                        Avatar = claims.GetValueOrDefault("Avatar"),
                        Role = claims.GetValueOrDefault("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    };

                    var claimObjects = claims.Select(c => new Claim(c.Key, c.Value));

                    var claimsIdentity = new ClaimsIdentity(claimObjects, JwtBearerDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(claimsIdentity);

                    // Set the user's identity on the HttpContext
                    context.User = principal;
                    context.Items["CurrentUser"] = user;
                };
            }
            await _next(context);
        }
    }
}
