using Service.DTOs.Property;

namespace Service.DTOs.Paper;

public class PaperDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Discontinued { get; set; }

    public int Stock { get; set; }

    public double Price { get; set; }

    public List<PropertyDto> Properties { get; set; } = new List<PropertyDto>();
}