using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Application.Common.Interface;
using T_Shop.Infrastructure.Persistence.IdentityModels;
using T_Shop.Shared.DTOs.User.RequestModels;

namespace T_Shop.Infrastructure.SharedServices.Authentication;
public class AccountManager : IAccountManager
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private ApplicationUser? _user;

    public AccountManager(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }


    public async Task<IdentityResult> RegisterUser(UserCreationResquestModel registerUser)
    {
        var user = await _userManager.FindByEmailAsync(registerUser.Email);
        if (user != null)
        {
            throw new ConflictException("Email existed, please choose other email");
        }
        if (!registerUser.Password.Equals(registerUser.ConfirmPassword))
        {
            throw new BadRequestException("Password not match.");
        }
        ApplicationUser newUser = new ApplicationUser()
        {
            FullName = registerUser.Full_name,
            UserName = registerUser.Username,
            Email = registerUser.Email,
            DateOfBirth = registerUser.Date_of_birth,
            Gender = registerUser.Gender,
            PhoneNumber = registerUser.PhoneNumber,
            Address = registerUser.Address,
            Avatar = registerUser.Avatar,
            CreatedAt = DateTime.UtcNow,
            IsLocked = false,
        };
        var result = await _userManager.CreateAsync(newUser, registerUser.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, "User");
        }
        return result;
    }

    public async Task<bool> ValidateUser(UserAuthenRequestModel userForAuth)
    {
        _user = await _userManager.FindByEmailAsync(userForAuth.Email);
        var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
        if (!result)
        {
            throw new BadRequestException("Authentication failed. Please check your email or password.");
        }
        return result;
    }

    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["secret"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, _user.UserName),
            new Claim(ClaimTypes.Email, _user.Email),
            new Claim(ClaimTypes.Name, _user.FullName)
            };
        if (!_user.Avatar.IsNullOrEmpty())
        {
            claims.Add(new Claim("Avatar", _user.Avatar));
        }
        else
        {
            claims.Add(new Claim("Avatar", string.Empty));
        }

        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddHours(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

}

