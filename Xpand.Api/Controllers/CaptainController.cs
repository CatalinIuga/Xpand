using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xpand.Api.Data;
using Xpand.Api.Dto;
using Xpand.Api.Models;

namespace Xpand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaptainsController : ControllerBase
{
    private readonly XpandDbContext _context;

    public CaptainsController(XpandDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Captain>>> GetCaptains()
    {
        return await _context.Captains.ToListAsync();
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Captain>> GetCaptain(int id)
    {
        var captain = await _context.Captains.FindAsync(id);

        if (captain == null)
            return NotFound();

        return Ok(captain);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Captain>> PostCaptain(CaptainDto captainDto)
    {
        var captain = new Captain { Name = captainDto.Name, Password = captainDto.Password };

        var team = new Team { Name = captain.Name + "'s team", Captain = captain };

        captain.Team = team;
        _context.Teams.Add(team);
        _context.Captains.Add(captain);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCaptain", new { id = captain.Id }, captain);
    }

    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutCaptain(int id, CaptainDto captainDto)
    {
        var captain = await _context.Captains.FindAsync(id);

        if (captain == null)
            return NotFound();

        captain.Name = captainDto.Name;
        captain.Password = captainDto.Password;

        _context.Entry(captain).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CaptainExists(id))
                return NotFound();
            throw;
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCaptain(int id)
    {
        var captain = await _context.Captains.FindAsync(id);

        if (captain == null)
            return NotFound();

        _context.Captains.Remove(captain);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CaptainExists(int id)
    {
        return _context.Captains.Any(e => e.Id == id);
    }
}
