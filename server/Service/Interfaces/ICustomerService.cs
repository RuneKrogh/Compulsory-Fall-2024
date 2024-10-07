using DataAccess.Models;
using Service.DTOs.Create;
using Service.DTOs.Read;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        // Read: Retrieves all customers as DTOs
        Task<IEnumerable<CustomerDto>> GetAllCustomers();
        
        // Read: Retrieves a single customer by their ID as a DTO
        Task<CustomerDto?> GetCustomerById(int id);
        
        // Create: Adds a new customer using the provided DTO
        Task<CustomerDto> CreateCustomer(CreateCustomerDto createCustomerDto);
        
        // Update: Updates an existing customer using the provided DTO
        Task UpdateCustomer(CustomerDto customerDto);
        
        // Delete: Deletes a customer by their ID
        Task DeleteCustomer(int id);
    }
}