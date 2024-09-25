using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Property;

public class UpdatePropertyDto
{
    [Required]
    [StringLength(255)]
    public string PropertyName { get; set; } = null!;
}