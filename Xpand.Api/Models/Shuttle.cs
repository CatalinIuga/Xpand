namespace Xpand.Api.Models;

public class Shuttle
{
    public int Id { get; set; }
    public string Name { get; set; } = "Lambda T-4a";

    public int TeamId { get; set; }
    public Team? Team { get; set; } = null;
}
