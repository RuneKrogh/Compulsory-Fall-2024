using Service.DTOs.Create;
using Service.DTOs.Read;

namespace Service.Interfaces;

public interface IOrderEntryService
{
    Task<IEnumerable<OrderEntryDto>> GetAllOrderEntries(); // Return DTOs instead of entities
    Task<OrderEntryDto?> GetOrderEntryById(int id); // Return DTOs instead of entities
    Task<OrderEntryDto> CreateOrderEntry(CreateOrderEntryDto createOrderEntryDto); // Use DTO for creation
    Task UpdateOrderEntry(OrderEntryDto orderEntryDto); // Use DTO for updates
    Task DeleteOrderEntry(int id);
}