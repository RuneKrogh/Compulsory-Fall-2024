using DataAccess;
using DataAccess.Models;
using Service.Interfaces;
using Service.DTOs.Create;
using Service.DTOs.Read;
using Microsoft.EntityFrameworkCore;

namespace Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly DunderMifflinContext _context;

        public OrderService(DunderMifflinContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            // Retrieves all orders from the database and maps them to DTOs
            return await _context.Orders
                .OrderBy(order => order.Id)
                .Select(order => OrderDto.FromOrder(order)) // Use the FromOrder method for mapping
                .ToListAsync();
        }

        public async Task<OrderDto?> GetOrderById(int id)
        {
            // Retrieves a specific order by ID and maps it to DTO
            var order = await _context.Orders.FindAsync(id);
            return order == null ? null : OrderDto.FromOrder(order); // Use the FromOrder method
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerId(int customerId)
        {
            // Retrieves orders for a specific customer and maps them to DTOs
            return await _context.Orders
                .Where(order => order.CustomerId == customerId)
                .Select(order => OrderDto.FromOrder(order)) // Use the FromOrder method for mapping
                .ToListAsync();
        }

        public async Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto)
        {
            // Creates a new order based on the DTO
            var order = new Order
            {
                OrderDate = createOrderDto.OrderDate,
                DeliveryDate = createOrderDto.DeliveryDate,
                Status = createOrderDto.Status,
                TotalAmount = createOrderDto.TotalAmount,
                CustomerId = createOrderDto.CustomerId
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Return the created order as a DTO using FromOrder method
            return OrderDto.FromOrder(order);
        }

        public async Task UpdateOrder(OrderDto orderDto)
        {
            // Updates an existing order in the database
            var order = await _context.Orders.FindAsync(orderDto.Id);
            if (order == null) return; // Handle case appropriately

            order.OrderDate = orderDto.OrderDate;
            order.DeliveryDate = orderDto.DeliveryDate;
            order.Status = orderDto.Status;
            order.TotalAmount = orderDto.TotalAmount;
            order.CustomerId = orderDto.CustomerId;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id)
        {
            // Deletes an order from the database by ID
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
