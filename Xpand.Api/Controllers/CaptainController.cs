using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xpand.Api.Data;
using Xpand.Api.Dto;
using Xpand.Api.Models;

namespace Xpand.Api.Controllers
{
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
            var captains = await _context.Captains.ToListAsync();
            return Ok(captains);
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
            var captain = CreateCaptainFromDto(captainDto);

            _context.Captains.Add(captain);

            var team = new Team
            {
                Captain = captain,
                CaptainId = captain.Id,
                Name = $"{captain.Name}'s Team"
            };

            _context.Teams.Add(team);

            var shuttle = new Shuttle
            {
                Name = $"{captain.Name}'s Shuttle",
                Team = team,
                TeamId = team.Id
            };

            _context.Shuttles.Add(shuttle);

            team.Shuttle = shuttle;
            team.ShuttleId = shuttle.Id;

            var robots = new[]
            {
                new Robot
                {
                    Name = "R2-D2",
                    Team = team,
                    TeamId = team.Id
                },
                new Robot
                {
                    Name = "C-3PO",
                    Team = team,
                    TeamId = team.Id
                }
            };

            _context.Robots.AddRange(robots);

            team.Robots = robots;

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCaptain), new { id = captain.Id }, captain);
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

            UpdateCaptainFromDto(captain, captainDto);

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

        private Captain CreateCaptainFromDto(CaptainDto captainDto)
        {
            return new Captain { Name = captainDto.Name, Password = captainDto.Password };
        }

        private void UpdateCaptainFromDto(Captain captain, CaptainDto captainDto)
        {
            captain.Name = captainDto.Name;
            captain.Password = captainDto.Password;
        }
    }
}
