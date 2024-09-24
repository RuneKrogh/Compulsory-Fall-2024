using DataAccess.Models;
using DataAccess.Repositories;
using Service.Interfaces;

namespace Service.Implementations;
    
public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }
    public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        return await _propertyRepository.GetAllPropertiesAsync();
    }
    public async Task<Property?> GetPropertyByIdAsync(int id)
    {
        return await _propertyRepository.GetPropertyByIdAsync(id);
    }
    public async Task AddPropertyAsync(Property property)
    {
        await _propertyRepository.AddPropertyAsync(property);
    }
    public async Task UpdatePropertyAsync(Property property)
    {
        await _propertyRepository.UpdatePropertyAsync(property);
    }
    public async Task DeletePropertyAsync(int id)
    {
        await _propertyRepository.DeletePropertyAsync(id);
    }
}