using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xpand.Api.Data;

namespace Xpand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly XpandDbContext _context;

    public TeamsController(XpandDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetTeams()
    {
        return Ok(await _context.Teams.ToListAsync());
    }

    [HttpGet("{id}/members")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetTeamCaptain(int id)
    {
        var team = _context.Teams.FirstOrDefault(t => t.Id == id);
        if (team == null)
        {
            return NotFound();
        }

        var captain = await _context.Captains.FirstOrDefaultAsync(r => r.Id == team.CaptainId);
        if (captain == null)
        {
            return NotFound();
        }

        var robots = _context.Robots.Where(r => r.TeamId == team.Id);

        return Ok(new { Captain = captain, Robots = robots });
    }
}
