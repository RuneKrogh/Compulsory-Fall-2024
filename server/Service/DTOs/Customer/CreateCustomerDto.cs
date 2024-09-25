using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Input;

public class CreateCustomerDto
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    [StringLength(255)]
    public string? Email { get; set; }
}