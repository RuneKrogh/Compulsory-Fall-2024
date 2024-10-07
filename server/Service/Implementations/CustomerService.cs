using DataAccess;
using DataAccess.Models;
using Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly DunderMifflinContext _context;

        public CustomerService(DunderMifflinContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            // Retrieves all customers from the database
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            // Retrieves a customer by their ID
            return await _context.Customers.FindAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            // Adds a new customer to the database
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            // Updates an existing customer in the database
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            // Deletes a customer from the database
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}