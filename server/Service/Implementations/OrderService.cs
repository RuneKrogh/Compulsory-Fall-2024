using DataAccess;
using DataAccess.Models;
using Service.Interfaces;
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

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            // Retrieves all orders from the database
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            // Retrieves a specific order by ID
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            // Retrieves orders for a specific customer
            return await _context.Orders
                .Where(order => order.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            // Adds a new order to the database
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            // Updates an existing order in the database
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
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