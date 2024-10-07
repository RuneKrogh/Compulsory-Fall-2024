using DataAccess.Models;

namespace Service.Interfaces;

public interface IOrderEntryService
{
    Task<IEnumerable<OrderEntry>> GetAllOrderEntries();
    Task<OrderEntry?> GetOrderEntryById(int id);
    Task CreateOrderEntry(OrderEntry orderEntry);
    Task UpdateOrderEntry(OrderEntry orderEntry);
    Task DeleteOrderEntry(int id);
}
