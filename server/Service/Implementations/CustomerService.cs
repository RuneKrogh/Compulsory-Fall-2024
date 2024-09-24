using DataAccess.Models;
using DataAccess.Repositories;
using Service.Interfaces;

namespace Service.Implementations;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _customerRepository.GetAllCustomersAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _customerRepository.GetCustomerByIdAsync(id);
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        await _customerRepository.AddCustomerAsync(customer);
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        await _customerRepository.UpdateCustomerAsync(customer);
    }

    public async Task DeleteCustomerAsync(int id)
    {
        await _customerRepository.DeleteCustomerAsync(id);
    }
}
