using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Xpand.Api.Data;
using Xpand.Api.Dto;

namespace Xpand.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly XpandDbContext _context;

    public AuthController(XpandDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("signin")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetToken([FromBody] LoginDto login)
    {
        var captain = await _context.Captains.FirstOrDefaultAsync(u => u.Name == login.Name);

        if (captain == null)
        {
            return Unauthorized(new { message = "Invalid name." });
        }

        if (captain.Password != login.Password)
        {
            return Unauthorized(new { message = "Invalid password." });
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        // Secret key for signing the token
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

        // Sending the token back to the user.
        Response.Headers.Add("Authorization", $"Bearer {tokenString}");

        return Ok(new TokenDto { Token = tokenString });
    }

    [HttpGet("info")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMe()
    {
        // Get the captain id from the token.
        // The token is validated by the [Authorize] attribute
        // so the id is always valid.
        var captainId = int.Parse(User.Identity!.Name!);
        var captain = await _context.Captains.FindAsync(captainId);

        if (captain == null)
        {
            return NotFound();
        }

        return Ok(new { Captain = captain });
    }

    [HttpGet("signout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult DeleteToken()
    {
        Response.Headers.Remove("Authorization");
        return Ok(new { message = "Signed out." });
    }
}
