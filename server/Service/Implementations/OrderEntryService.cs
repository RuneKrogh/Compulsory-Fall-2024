using DataAccess.Models;
using DataAccess.Repositories;
using Service.Interfaces;

namespace Service.Implementations;

public class OrderEntryService : IOrderEntryService
{
    private readonly IOrderEntryRepository _orderEntryRepository;

    public OrderEntryService(IOrderEntryRepository orderEntryRepository)
    {
        _orderEntryRepository = orderEntryRepository;
    }

    public async Task<IEnumerable<OrderEntry>> GetAllOrderEntriesAsync()
    {
        return await _orderEntryRepository.GetAllOrderEntriesAsync();
    }

    public async Task<OrderEntry?> GetOrderEntryByIdAsync(int id)
    {
        return await _orderEntryRepository.GetOrderEntryByIdAsync(id);
    }

    public async Task AddOrderEntryAsync(OrderEntry orderEntry)
    {
        await _orderEntryRepository.AddOrderEntryAsync(orderEntry);
    }

    public async Task UpdateOrderEntryAsync(OrderEntry orderEntry)
    {
        await _orderEntryRepository.UpdateOrderEntryAsync(orderEntry);
    }

    public async Task DeleteOrderEntryAsync(int id)
    {
        await _orderEntryRepository.DeleteOrderEntryAsync(id);
    }
}
