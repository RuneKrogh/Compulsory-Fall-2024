using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using Service.DTOs.Create;
using Service.DTOs.Read;
using Service.Implementations;
using Service.Interfaces;
using DataAccess;
using Service.Validation; // Ensure you're using the right namespace for CreateCustomerValidation
using Xunit;

namespace UnitTests
{
    public class CustomerTests : IDisposable
    {
        private readonly DunderMifflinContext _context;
        private readonly ICustomerService _customerService;

        public CustomerTests()
        {
            // Create a unique in-memory database for each test instance
            var options = new DbContextOptionsBuilder<DunderMifflinContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DunderMifflinContext(options);
            var createCustomerValidation = new CreateCustomerValidation();
            var updateCustomerValidation = new UpdateCustomerValidation();
            _customerService = new CustomerService(_context, createCustomerValidation, updateCustomerValidation);
        }

        public void Dispose()
        {
            // Cleanup in-memory database after each test
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CreateCustomer_ValidData_ShouldCreateCustomer()
        {
            // Arrange
            var createCustomerDto = new CreateCustomerDto
            {
                Name = "John Doe",
                Address = "123 Main St",
                Phone = "+4512345678",
                Email = "john.doe@example.com"
            };

            // Act
            var customerDto = await _customerService.CreateCustomer(createCustomerDto);

            // Assert
            Assert.NotNull(customerDto);
            Assert.Equal("John Doe", customerDto.Name);
            Assert.Equal("123 Main St", customerDto.Address);
            Assert.Equal("+4512345678", customerDto.Phone);
            Assert.Equal("john.doe@example.com", customerDto.Email);
        }

        [Fact]
        public async Task CreateCustomer_InvalidData_ShouldThrowValidationException()
        {
            // Arrange
            var createCustomerDto = new CreateCustomerDto
            {
                Name = "", // Invalid name (too short)
                Address = "123 Main St",
                Phone = "+4512345678",
                Email = "john.doe@example.com"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _customerService.CreateCustomer(createCustomerDto));
        }

        [Fact]
        public async Task GetAllCustomers_ShouldReturnAllCustomers()
        {
            // Arrange
            await _customerService.CreateCustomer(new CreateCustomerDto
            {
                Name = "John Doe",
                Address = "123 Main St",
                Phone = "+4512345678",
                Email = "john.doe@example.com"
            });
            await _customerService.CreateCustomer(new CreateCustomerDto
            {
                Name = "Jane Doe",
                Address = "456 Elm St",
                Phone = "+4598765432",
                Email = "jane.doe@example.com"
            });

            // Act
            var customers = await _customerService.GetAllCustomers();

            // Assert
            Assert.Equal(2, customers.Count());
        }

        [Fact]
        public async Task GetCustomerById_ExistingId_ShouldReturnCustomer()
        {
            // Arrange
            var customerDto = await _customerService.CreateCustomer(new CreateCustomerDto
            {
                Name = "John Doe",
                Address = "123 Main St",
                Phone = "+4512345678",
                Email = "john.doe@example.com"
            });

            // Act
            var customer = await _customerService.GetCustomerById(customerDto.Id);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(customerDto.Id, customer.Id);
        }

        [Fact]
        public async Task UpdateCustomer_ValidData_ShouldUpdateCustomer()
        {
            // Arrange
            var customerDto = await _customerService.CreateCustomer(new CreateCustomerDto
            {
                Name = "John Doe",
                Address = "123 Main St",
                Phone = "+4512345678",
                Email = "john.doe@example.com"
            });

            var updatedCustomerDto = new CustomerDto
            {
                Id = customerDto.Id,
                Name = "John Smith",
                Address = "789 Oak St",
                Phone = "+4598765432",
                Email = "john.smith@example.com"
            };

            // Act
            await _customerService.UpdateCustomer(updatedCustomerDto);
            var updatedCustomer = await _customerService.GetCustomerById(customerDto.Id);

            // Assert
            Assert.Equal("John Smith", updatedCustomer.Name);
            Assert.Equal("789 Oak St", updatedCustomer.Address);
            Assert.Equal("+4598765432", updatedCustomer.Phone);
            Assert.Equal("john.smith@example.com", updatedCustomer.Email);
        }

        [Fact]
        public async Task DeleteCustomer_ExistingId_ShouldRemoveCustomer()
        {
            // Arrange
            var customerDto = await _customerService.CreateCustomer(new CreateCustomerDto
            {
                Name = "John Doe",
                Address = "123 Main St",
                Phone = "+4512345678",
                Email = "john.doe@example.com"
            });

            // Act
            await _customerService.DeleteCustomer(customerDto.Id);
            var deletedCustomer = await _customerService.GetCustomerById(customerDto.Id);

            // Assert
            Assert.Null(deletedCustomer);
        }

        [Fact]
        public async Task DeleteCustomer_NonExistingId_ShouldNotThrowException()
        {
            // Act
            await _customerService.DeleteCustomer(999); // Non-existing ID

            // Assert
            var customers = await _customerService.GetAllCustomers();
            Assert.Empty(customers); // Should not throw exception and customers list should be empty
        }
        
        [Fact]
        public async Task UpdateCustomer_InvalidData_ShouldThrowValidationException()
        {
            // Arrange
            // Create a valid customer first
            var customerDto = await _customerService.CreateCustomer(new CreateCustomerDto
            {
                Name = "John Doe",
                Address = "123 Main St",
                Phone = "+4512345678",
                Email = "john.doe@example.com"
            });

            // Create an updated customer DTO with invalid data
            var updatedCustomerDto = new CustomerDto
            {
                Id = customerDto.Id,
                Name = "", // Invalid name (too short)
                Address = "789 Oak St",
                Phone = "+4598765432",
                Email = "john.smith@example.com"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _customerService.UpdateCustomer(updatedCustomerDto));
        }
    }
}
