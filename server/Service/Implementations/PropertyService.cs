using DataAccess;
using DataAccess.Models;
using Service.Interfaces;
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

        public async Task<IEnumerable<Property>> GetAllProperties()
        {
            // Retrieves all properties from the database
            return await _context.Properties.ToListAsync();
        }

        public async Task<Property?> GetPropertyById(int id)
        {
            // Retrieves a specific property by ID
            return await _context.Properties.FindAsync(id);
        }

        public async Task CreateProperty(Property property)
        {
            // Adds a new property to the database
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProperty(Property property)
        {
            // Updates an existing property in the database
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProperty(int id)
        {
            // Deletes a property from the database by ID
            var property = await _context.Properties.FindAsync(id);
            if (property != null)
            {
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
            }
        }
    }
}