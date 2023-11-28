using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xpand.Api.Data;
using Xpand.Api.Dto;
using Xpand.Api.Models;

namespace Xpand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanetsController : ControllerBase
{
    private readonly XpandDbContext _context;

    public PlanetsController(XpandDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Planet>>> GetPlanets()
    {
        return await _context.Planets.ToListAsync();
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Planet>> GetPlanet(int id)
    {
        var planet = await _context.Planets.FindAsync(id);

        if (planet == null)
        {
            return NotFound();
        }

        return planet;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Planet>> PostPlanet([FromForm] PlanetDto planet)
    {
        // Saving the new image if it exists
        var planetImage = planet.Image;
        var planetImageName = Guid.NewGuid().ToString() + ".png";
        if (planetImage is null || planetImage.Length == 0)
            planetImageName = "default.png";
        else if (planetImage.Length > 0)
            await using (
                var stream = new FileStream($"./Uploads/{planetImageName}", FileMode.Create)
            )
                await planetImage.CopyToAsync(stream);
        else
            return BadRequest("Invalid file type");

        var pln = new Planet
        {
            Name = planet.Name,
            ImageName = planetImageName,
            Description = planet.Description,
            RobotsCount = planet.RobotsCount,
            Status = planet.Status,
            TeamId = planet.TeamId
        };

        // Reset the planet if the status is TODO
        // Probably a mistake from a captain's part
        if (planet.Status == PlanetStatus.TODO)
        {
            planet.TeamId = null;
            planet.Description = "No description yet :/";
            planet.RobotsCount = 0;
        }

        _context.Planets.Add(pln);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPlanet", new { id = pln.Id }, pln);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlanet(int id, [FromForm] PlanetDto planetDto)
    {
        var planet = await _context.Planets.FindAsync(id);

        if (planet is null)
            return NotFound();

        // Saving the new image if it exists
        var planetImage = planetDto.Image;
        var planetImageName = Guid.NewGuid().ToString() + ".png";
        if (planetImage is null || planetImage.Length == 0)
            planetImageName = planet.ImageName;
        else if (planetImage.Length > 0)
        {
            // Delete the old imagee
            if (planet.ImageName != "default.png")
                System.IO.File.Delete($"./Uploads/{planet.ImageName}");

            // Save the new image
            await using var stream = new FileStream(
                $"./Uploads/{planetImageName}",
                FileMode.Create
            );
            await planetImage.CopyToAsync(stream);
        }
        else
            return BadRequest("Invalid file type");

        // Dto to model
        planet.Name = planetDto.Name;
        planet.ImageName = planetImageName;
        planet.Description = planetDto.Description;
        planet.Status = planetDto.Status;

        // Could definitely swap this for the count of robots in the team
        planet.RobotsCount = planetDto.RobotsCount;

        // Create the link between the planet and the team if specified
        if (planetDto.TeamId is not null)
            planet.TeamId = planetDto.TeamId.Value;
        else
            planet.TeamId = null;

        // Reset the planet if the status is TODO
        // Probably a mistake from a captain's part
        if (planetDto.Status == PlanetStatus.TODO)
        {
            planet.TeamId = null;
            planet.Description = "No description yet :/";
            planet.RobotsCount = 0;
        }

        // Save changes to database
        _context.Entry(planet).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!PlanetExists(id))
        {
            return NotFound();
        }

        return Ok(planet);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlanet(int id)
    {
        var planet = await _context.Planets.FindAsync(id);
        if (planet == null)
        {
            return NotFound();
        }

        _context.Planets.Remove(planet);

        await _context.SaveChangesAsync();

        if (planet.ImageName != "default.png")
            System.IO.File.Delete($"./Uploads/{planet.ImageName}");

        return NoContent();
    }

    private bool PlanetExists(int id)
    {
        return _context.Planets.Any(e => e.Id == id);
    }
}
