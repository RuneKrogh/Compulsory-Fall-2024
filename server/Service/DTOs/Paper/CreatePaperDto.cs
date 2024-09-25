using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Input.Paper;

public class CreatePaperDto
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    public bool Discontinued { get; set; } = false;

    public int Stock { get; set; } = 0;

    public double Price { get; set; }

    // Include a list of property IDs to associate properties when creating a Paper
    public List<int> PropertyIds { get; set; } = new List<int>();
}