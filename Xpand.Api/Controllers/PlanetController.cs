using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xpand.Api.Data;
using Xpand.Api.Dto;
using Xpand.Api.Models;
using Xpand.Api.Services;

namespace Xpand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanetsController : ControllerBase
{
    private readonly XpandDbContext _context;
    private readonly FileService _fileService;

    public PlanetsController(XpandDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Planet>>> GetPlanets()
    {
        var planets = await _context.Planets.ToListAsync();
        return Ok(planets);
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
    public async Task<ActionResult<Planet>> PostPlanet([FromForm] PlanetDto planetDto)
    {
        try
        {
            var planet = CreatePlanetFromDto(planetDto);
            await _fileService.SaveFileAsync(planetDto.Image, planet.ImageName);
            _context.Planets.Add(planet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlanet), new { id = planet.Id }, planet);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error creating planet: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlanet(int id, [FromForm] PlanetDto planetDto)
    {
        try
        {
            var planet = await UpdatePlanetFromDtoAsync(id, planetDto);
            await _context.SaveChangesAsync();
            return Ok(planet);
        }
        catch (DbUpdateConcurrencyException) when (!PlanetExists(id))
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest($"Error updating planet: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlanet(int id)
    {
        try
        {
            var planet = await _context.Planets.FindAsync(id);

            if (planet == null)
            {
                return NotFound();
            }

            _context.Planets.Remove(planet);

            await _context.SaveChangesAsync();

            _fileService.DeleteFile(planet.ImageName);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Error deleting planet: {ex.Message}");
        }
    }

    private bool PlanetExists(int id)
    {
        return _context.Planets.Any(e => e.Id == id);
    }

    private Planet CreatePlanetFromDto(PlanetDto planetDto)
    {
        return new Planet
        {
            Name = planetDto.Name,
            ImageName = Guid.NewGuid().ToString() + ".png",
            Description = planetDto.Description,
            RobotsCount = planetDto.RobotsCount,
            Status = planetDto.Status,
            TeamId = planetDto.TeamId
        };
    }

    private async Task<Planet> UpdatePlanetFromDtoAsync(int id, PlanetDto planetDto)
    {
        var planet = await _context.Planets.FindAsync(id);

        if (planet == null)
        {
            throw new Exception("Planet not found");
        }

        if (planetDto.Image != null)
        {
            var newImageName = Guid.NewGuid().ToString() + ".png";
            _fileService.DeleteFile(planet.ImageName);
            await _fileService.SaveFileAsync(planetDto.Image, newImageName);
            planet.ImageName = newImageName;
        }

        planet.Name = planetDto.Name;
        planet.Description = planetDto.Description;
        planet.RobotsCount = planetDto.RobotsCount;
        planet.Status = planetDto.Status;
        planet.TeamId = planetDto.TeamId;

        return planet;
    }
}
