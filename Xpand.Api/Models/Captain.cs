using System.Text.Json.Serialization;

namespace Xpand.Api.Models;

public class Captain
{
    public int Id { get; set; }
    public string Name { get; set; } = "Luke Skywalker";
    public string Password { get; set; } = "No password yet :/";

    public int? TeamId { get; set; }

    [JsonIgnore]
    public Team? Team { get; set; } = null;
}
