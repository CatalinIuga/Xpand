using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Xpand.Api.Data;
using Xpand.Api.Dto;
using Xpand.Api.Services;

namespace Xpand.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly XpandDbContext _context;
        private readonly AuthService _authService;

        public AuthController(
            XpandDbContext context,
            IConfiguration configuration,
            AuthService authService
        )
        {
            _context = context;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> SignIn([FromBody] LoginDto login)
        {
            try
            {
                var tokenString = await _authService.GetTokenAsync(login);
                Response.Headers.Add("Authorization", $"Bearer {tokenString}");

                return Ok(new TokenDto { Token = tokenString });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("info")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetInfo()
        {
            var captainId = int.Parse(User.Identity!.Name!);
            var captain = await _context.Captains.FindAsync(captainId);

            if (captain == null)
            {
                return NotFound(new { message = "Captain not found." });
            }

            return Ok(new { Captain = captain });
        }

        [HttpPost("signout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Revoke()
        {
            // Technically, we should add the token to a blacklist
            // but for the sake of simplicity, we just remove it from the response.
            // The client should remove the token from its local storage.
            Response.Headers.Remove("Authorization");
            return Ok(new { message = "Signed out." });
        }
    }
}
