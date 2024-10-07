using DataAccess.Models;

namespace Service.DTOs.Read;

public class PropertyDto
{
    public int Id { get; set; } // Unique identifier for the property
    public string PropertyName { get; set; } = null!; // Name of the property

    // Method to map a Property entity to a PropertyDto
    public static PropertyDto FromProperty(Property property)
    {
        return new PropertyDto
        {
            Id = property.Id,
            PropertyName = property.PropertyName
        };
    }
}