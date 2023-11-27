using System.Text.Json.Serialization;

namespace Xpand.Api.Models;

public class Robot
{
    public int Id { get; set; }
    public string Name { get; set; } = "R2D2";

    public int? TeamId { get; set; }

    [JsonIgnore]
    public Team? Team { get; set; } = null!;
}
