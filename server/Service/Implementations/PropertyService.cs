using DataAccess;
using DataAccess.Models;
using Service.Interfaces;
using Service.DTOs.Read;
using Service.DTOs.Create;
using Microsoft.EntityFrameworkCore;

namespace Service.Implementations
{
    public class PropertyService : IPropertyService
    {
        private readonly DunderMifflinContext _context;

        public PropertyService(DunderMifflinContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PropertyDto>> GetAllProperties()
        {
            // Returns all properties as DTOs
            return await _context.Properties
                .OrderBy(property => property.Id)
                .Select(property => new PropertyDto
                {
                    Id = property.Id,
                    PropertyName = property.PropertyName
                })
                .ToListAsync();
        }

        public async Task<PropertyDto?> GetPropertyById(int id)
        {
            // Returns a property DTO by ID
            var property = await _context.Properties.FindAsync(id);
            return property != null ? new PropertyDto
            {
                Id = property.Id,
                PropertyName = property.PropertyName
            } : null;
        }

        public async Task<PropertyDto> CreateProperty(CreatePropertyDto createPropertyDto)
        {
            // Creates a new property from DTO
            var property = new Property
            {
                PropertyName = createPropertyDto.PropertyName
            };

            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            // Return the created property as a DTO
            return PropertyDto.FromProperty(property); // Assuming you want to return the same DTO back
        }

        public async Task UpdateProperty(PropertyDto propertyDto)
        {
            // Updates an existing property using DTO
            var property = await _context.Properties.FindAsync(propertyDto.Id);
            if (property != null)
            {
                property.PropertyName = propertyDto.PropertyName;

                _context.Properties.Update(property);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProperty(int id)
        {
            // Deletes a property by ID
            var property = await _context.Properties.FindAsync(id);
            if (property != null)
            {
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
            }
        }
    }
}
