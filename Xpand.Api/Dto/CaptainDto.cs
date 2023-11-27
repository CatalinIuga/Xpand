using System.ComponentModel.DataAnnotations;

namespace Xpand.Api.Dto;

public class CaptainDto
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(3)]
    public string Password { get; set; } = null!;
}
