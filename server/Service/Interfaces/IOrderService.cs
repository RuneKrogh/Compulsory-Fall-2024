using Service.DTOs.Create;
using Service.DTOs.Read;

namespace Service.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrders(); // Retrieve all orders as DTOs
    Task<OrderDto?> GetOrderById(int id); // Retrieve a specific order by ID as DTO

    Task<IEnumerable<OrderDto>>
        GetOrdersByCustomerId(int customerId); // Retrieve orders for a specific customer as DTOs

    Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto); // Create a new order using a DTO
    Task UpdateOrder(OrderDto orderDto); // Update an existing order using a DTO
    Task DeleteOrder(int id); // Delete an order by ID
}