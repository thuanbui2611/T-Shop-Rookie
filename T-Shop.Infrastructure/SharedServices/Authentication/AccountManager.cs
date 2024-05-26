using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence.IdentityModels;
using T_Shop.Infrastructure.SharedServices.CloudinaryService;
using T_Shop.Shared.DTOs.User.RequestModels;
namespace T_Shop.Infrastructure.SharedServices.Authentication;
public class AccountManager : IAccountManager
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IUnitOfWork _unitOfWork;
    private ApplicationUser? _user;

    public AccountManager(UserManager<ApplicationUser> userManager, IConfiguration configuration, ICloudinaryService cloudinaryService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _configuration = configuration;
        _cloudinaryService = cloudinaryService;
        _unitOfWork = unitOfWork;
    }


    public async Task<IdentityResult> RegisterUser(UserCreationResquestModel registerUser)
    {
        var user = await _userManager.FindByEmailAsync(registerUser.Email);
        if (user != null)
        {
            if (user.UserName.ToLower().Equals(registerUser.Username.ToLower()))
            {
                throw new ConflictException("Username existed, please choose other email");

            }
            else
            {
                throw new ConflictException("Email existed, please choose other email");
            }

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
            Avatar = null,
            CreatedAt = DateTime.UtcNow,
            IsLocked = false,
        };
        // Add image
        if (registerUser.Avatar.Length > 0)
        {
            var uploadResponse = await _cloudinaryService.AddImageAsync(registerUser.Avatar);
            newUser.Avatar = uploadResponse.PublicID;
        }

        // Add user
        var result = await _userManager.CreateAsync(newUser, registerUser.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, "User");
        }
        else
        {
            throw new Exception(result.Errors.First().Description);
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
        if (_user.Avatar is not null)
        {
            claims.Add(new Claim("Avatar", _user.Avatar));
        }
        else
        {
            claims.Add(new Claim("Avatar", string.Empty));
        }
        claims.Add(new Claim("UserId", _user.Id.ToString()));

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

