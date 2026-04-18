using WebProjectAPIs.Database;
using WebProjectAPIs.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using WebProjectAPIs.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebProjectAPIs.Services;

public class AuthServices(DB _dB, IConfiguration configuration)
{
    public async Task RegisterAsync(RegisterUserDto registerUser)
    {
        // Check for duplicate username
        var usernameExists = await _dB.Users.AnyAsync(m => m.Name == registerUser.Name);

        if (usernameExists)
        {
            throw new Exception("Username already exists");
        }

        if (string.IsNullOrEmpty(registerUser.Password))
        {
            throw new Exception("Password must not be null");
        }

        User user = new User
        {
            Name = registerUser.Name,
            Email = registerUser.Email,
            Password = registerUser.Password,
            Role = registerUser.Role
        };

        await _dB.Users.AddAsync(user);
        await _dB.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(LoginUserDto loginUser)
    {
        var user = await _dB.Users.FirstOrDefaultAsync(m => m.Name == loginUser.Name);

        if (user == null || user.Password != loginUser.Password)
        {
            throw new Exception("Invalid username or password");
        }

        string token = CreateToken(user);

        return token;
    }

    private string CreateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSetting:SecretKey")!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
            new Claim(ClaimTypes.Role, user.Role ?? string.Empty)
        };

        var tokenDescriptor = new JwtSecurityToken(
            signingCredentials: creds,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            issuer: configuration.GetValue<string>("AppSetting:Issuer"),
            audience: configuration.GetValue<string>("AppSetting:Audience")
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
