using DataAccess.Models;

namespace Service.Interfaces;

public interface IPropertyService
{
    Task<IEnumerable<Property>> GetAllPropertiesAsync();
    Task<Property?> GetPropertyByIdAsync(int id);
    Task AddPropertyAsync(Property property);
    Task UpdatePropertyAsync(Property property);
    Task DeletePropertyAsync(int id);
}
