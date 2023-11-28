using Xpand.Api.Models;

namespace Xpand.Api.Dto;

public class PlanetDto
{
    public string Name { get; set; } = null!;
    public IFormFile? Image { get; set; }
    public string Description { get; set; } = "No description yet :/";
    public PlanetStatus Status { get; set; } = PlanetStatus.TODO;
    public int RobotsCount { get; set; } = 0;
    public int? TeamId { get; set; }
}
