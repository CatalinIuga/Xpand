using System.Text.Json.Serialization;

namespace Xpand.Api.Models;

public class Planet
{
    public int Id { get; set; }
    public string Name { get; set; } = "Luke Skywalker";
    public string ImageName { get; set; } = "default.png";
    public string Description { get; set; } = "No description yet :/";
    public PlanetStatus Status { get; set; } = PlanetStatus.TODO;

    // Can be changed by a captain or automatically by the team's robot count.
    public int RobotsCount { get; set; } = 0;

    // The team realationship will get initialized when a captain
    // modifies the planet status.
    public int? TeamId { get; set; }

    [JsonIgnore]
    public Team? Team { get; set; } = null;
}

public enum PlanetStatus
{
    OK,
    NotOK,
    TODO,
    EnRoute
}
