using Service.DTOs.Paper;
using Service.DTOs.Property;

namespace Service.DTOs.PaperProperty;

public class PaperPropertyDto
{
    public PaperDto Paper { get; set; } = null!;

    public PropertyDto Property { get; set; } = null!;
}