using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Create;
using Service.DTOs.Read;
using Service.Implementations;
using Service.Validation.PropertyValidation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class PropertyServiceTests : IDisposable
    {
        private readonly DunderMifflinContext _context;
        private readonly PropertyService _propertyService;
        private readonly CreatePropertyValidation _createPropertyValidation;
        private readonly UpdatePropertyValidation _updatePropertyValidation;

        public PropertyServiceTests()
        {
            var options = new DbContextOptionsBuilder<DunderMifflinContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DunderMifflinContext(options);
            _createPropertyValidation = new CreatePropertyValidation();
            _updatePropertyValidation = new UpdatePropertyValidation();
            _propertyService = new PropertyService(_context, _createPropertyValidation, _updatePropertyValidation);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CreateProperty_ValidData_ShouldCreateProperty()
        {
            // Arrange
            var createPropertyDto = new CreatePropertyDto
            {
                PropertyName = "New Property"
            };

            // Act
            var propertyDto = await _propertyService.CreateProperty(createPropertyDto);

            // Assert
            Assert.NotNull(propertyDto);
            Assert.Equal("New Property", propertyDto.PropertyName);
            Assert.True(propertyDto.Id > 0); // Ensure that an ID is assigned
        }

        [Fact]
        public async Task CreateProperty_InvalidData_ShouldThrowValidationException()
        {
            // Arrange
            var createPropertyDto = new CreatePropertyDto
            {
                PropertyName = "" // Invalid property name (too short)
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _propertyService.CreateProperty(createPropertyDto));
        }

        [Fact]
        public async Task GetAllProperties_ShouldReturnAllProperties()
        {
            // Arrange
            await _propertyService.CreateProperty(new CreatePropertyDto { PropertyName = "Property 1" });
            await _propertyService.CreateProperty(new CreatePropertyDto { PropertyName = "Property 2" });

            // Act
            var properties = await _propertyService.GetAllProperties();

            // Assert
            Assert.Equal(2, properties.Count());
        }

        [Fact]
        public async Task GetPropertyById_ExistingId_ShouldReturnProperty()
        {
            // Arrange
            var propertyDto = await _propertyService.CreateProperty(new CreatePropertyDto { PropertyName = "Property 1" });

            // Act
            var property = await _propertyService.GetPropertyById(propertyDto.Id);

            // Assert
            Assert.NotNull(property);
            Assert.Equal(propertyDto.Id, property.Id);
            Assert.Equal(propertyDto.PropertyName, property.PropertyName);
        }

        [Fact]
        public async Task UpdateProperty_ValidData_ShouldUpdateProperty()
        {
            // Arrange
            var propertyDto = await _propertyService.CreateProperty(new CreatePropertyDto { PropertyName = "Old Property" });
            var updatedPropertyDto = new PropertyDto
            {
                Id = propertyDto.Id,
                PropertyName = "Updated Property"
            };

            // Act
            await _propertyService.UpdateProperty(updatedPropertyDto);
            var updatedProperty = await _propertyService.GetPropertyById(propertyDto.Id);

            // Assert
            Assert.Equal("Updated Property", updatedProperty.PropertyName);
        }

        [Fact]
        public async Task UpdateProperty_InvalidData_ShouldThrowValidationException()
        {
            // Arrange
            var propertyDto = await _propertyService.CreateProperty(new CreatePropertyDto { PropertyName = "Valid Property" });
            var updatedPropertyDto = new PropertyDto
            {
                Id = propertyDto.Id,
                PropertyName = "" // Invalid property name (too short)
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _propertyService.UpdateProperty(updatedPropertyDto));
        }

        [Fact]
        public async Task DeleteProperty_ExistingId_ShouldRemoveProperty()
        {
            // Arrange
            var propertyDto = await _propertyService.CreateProperty(new CreatePropertyDto { PropertyName = "Property to Delete" });

            // Act
            await _propertyService.DeleteProperty(propertyDto.Id);
            var deletedProperty = await _propertyService.GetPropertyById(propertyDto.Id);

            // Assert
            Assert.Null(deletedProperty);
        }

        [Fact]
        public async Task DeleteProperty_NonExistingId_ShouldNotThrowException()
        {
            // Act
            await _propertyService.DeleteProperty(999); // Non-existing ID

            // Assert
            var properties = await _propertyService.GetAllProperties();
            Assert.Empty(properties); // Should not throw exception and properties list should be empty
        }
    }
}
