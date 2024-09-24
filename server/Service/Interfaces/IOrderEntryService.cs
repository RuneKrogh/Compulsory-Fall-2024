using DataAccess.Models;

namespace Service.Interfaces;

public interface IOrderEntryService
{
    Task<IEnumerable<OrderEntry>> GetAllOrderEntriesAsync();
    Task<OrderEntry?> GetOrderEntryByIdAsync(int id);
    Task AddOrderEntryAsync(OrderEntry orderEntry);
    Task UpdateOrderEntryAsync(OrderEntry orderEntry);
    Task DeleteOrderEntryAsync(int id);
}
