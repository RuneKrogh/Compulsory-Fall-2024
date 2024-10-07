using DataAccess.Models;

namespace Service.DTOs.Read
{
    public class CustomerDto
    {
        public int Id { get; set; }  // Unique identifier for the customer
        public string Name { get; set; } = null!;  // Name of the customer
        public string? Address { get; set; }  // Optional address
        public string? Phone { get; set; }  // Optional phone number
        public string? Email { get; set; }  // Optional email

        // Method to map a Customer entity to a CustomerDto
        public static CustomerDto FromCustomer(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Phone = customer.Phone,
                Email = customer.Email
            };
        }
    }
}