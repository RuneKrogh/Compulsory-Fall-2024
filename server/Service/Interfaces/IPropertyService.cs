using DataAccess.Models;
using Service.DTOs.Read;
using Service.DTOs.Create;

namespace Service.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyDto>> GetAllProperties();          // Returns all properties as DTOs
        Task<PropertyDto?> GetPropertyById(int id);                // Returns a property DTO by ID
        Task<PropertyDto> CreateProperty(CreatePropertyDto createPropertyDto);  // Creates a new property from DTO
        Task UpdateProperty(PropertyDto propertyDto);               // Updates an existing property using DTO
        Task DeleteProperty(int id);                                 // Deletes a property by ID
    }
}