using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Input.Paper;

public class UpdatePaperDto
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    public bool Discontinued { get; set; }

    public int Stock { get; set; }

    public double Price { get; set; }

    // Include a list of property IDs to update the associated properties of a Paper
    public List<int> PropertyIds { get; set; } = new List<int>();
}