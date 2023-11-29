using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Xpand.Api.Data;
using Xpand.Api.Dto;

namespace Xpand.Api.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly XpandDbContext _context;

    public AuthService(XpandDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> GetTokenAsync(LoginDto login)
    {
        var captain = await _context.Captains.FirstOrDefaultAsync(u => u.Name == login.Name);

        if (captain == null)
        {
            throw new UnauthorizedAccessException("Invalid name.");
        }

        if (captain.Password != login.Password)
        {
            throw new UnauthorizedAccessException("Invalid password.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // Save the captain id in the token.
            Subject = new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.Name, captain.Id.ToString()) }
            ),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}
