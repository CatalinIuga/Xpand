using System.Text.Json.Serialization;

namespace Xpand.Api.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CaptainId { get; set; }

    // public int? ShuttleId { get; set; }

    [JsonIgnore]
    public Captain Captain { get; set; } = null!;

    // [JsonIgnore]
    // public Shuttle? Shuttle { get; set; } = null;

    [JsonIgnore]
    public ICollection<Robot> Robots { get; set; } = new List<Robot>();

    [JsonIgnore]
    public ICollection<Planet> Planets { get; set; } = new List<Planet>();
}
