using DataAccess.Models;
using DataAccess.Repositories;
using Service.Interfaces;

namespace Service.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllOrdersAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _orderRepository.GetOrderByIdAsync(id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
    }

    public async Task AddOrderAsync(Order order)
    {
        await _orderRepository.AddOrderAsync(order);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        await _orderRepository.UpdateOrderAsync(order);
    }

    public async Task DeleteOrderAsync(int id)
    {
        await _orderRepository.DeleteOrderAsync(id);
    }
}
