using DataAccess.Models;

namespace Service.Interfaces;

public interface IPropertyService
{
    Task<IEnumerable<Property>> GetAllProperties();
    Task<Property?> GetPropertyById(int id);
    Task CreateProperty(Property property);
    Task UpdateProperty(Property property);
    Task DeleteProperty(int id);
}
