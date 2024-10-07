﻿using DataAccess;
using DataAccess.Models;
using Service.Interfaces;
using Service.DTOs.Create;
using Service.DTOs.Read;
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

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            // Retrieves all customers from the database and maps them to DTOs
            return await _context.Customers
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Phone = c.Phone,
                    Email = c.Email
                })
                .OrderBy(c => c.Id) // Sort by Id
                .ToListAsync();
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            // Retrieves a customer by their ID and maps it to DTO
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            return CustomerDto.FromCustomer(customer); // Use the mapping method from DTO
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            // Creates a new Customer object from the DTO
            var customer = new Customer
            {
                Name = createCustomerDto.Name,
                Address = createCustomerDto.Address,
                Phone = createCustomerDto.Phone,
                Email = createCustomerDto.Email
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            // Return the created customer as a DTO
            return CustomerDto.FromCustomer(customer);
        }

        public async Task UpdateCustomerAsync(CustomerDto customerDto)
        {
            // Updates an existing customer in the database using the DTO
            var customer = await _context.Customers.FindAsync(customerDto.Id);
            if (customer == null) return; // or handle the case appropriately

            customer.Name = customerDto.Name;
            customer.Address = customerDto.Address;
            customer.Phone = customerDto.Phone;
            customer.Email = customerDto.Email;

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
