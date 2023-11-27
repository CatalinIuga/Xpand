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
    public async Task<ActionResult<Planet>> PostPlanet([FromForm] PlanetDto planet)
    {
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

        // TODO: if the planet status is not TODO, initialize the team and robotCount,
        // otherwise, reset them
        var pln = new Planet
        {
            Name = planet.Name,
            ImageName = planetImageName,
            Description = planet.Description,
            Status = planet.Status
        };

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

        var planetImage = planetDto.Image;
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

        planet.Name = planetDto.Name;
        // TODO, delete the old image
        planet.ImageName = planetImageName;
        planet.Description = planetDto.Description;
        planet.Status = planetDto.Status;

        _context.Entry(planet).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!PlanetExists(id))
        {
            return NotFound();
        }

        return NoContent();
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

        return NoContent();
    }

    private bool PlanetExists(int id)
    {
        return _context.Planets.Any(e => e.Id == id);
    }
}
