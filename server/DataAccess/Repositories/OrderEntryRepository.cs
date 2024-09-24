using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface IOrderEntryRepository
    {
        Task<IEnumerable<OrderEntry>> GetAllOrderEntriesAsync();
        Task<OrderEntry?> GetOrderEntryByIdAsync(int id);
        Task AddOrderEntryAsync(OrderEntry orderEntry);
        Task UpdateOrderEntryAsync(OrderEntry orderEntry);
        Task DeleteOrderEntryAsync(int id);
    }

    public class OrderEntryRepository : IOrderEntryRepository
    {
        private readonly DunderMifflinContext _context;

        public OrderEntryRepository(DunderMifflinContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderEntry>> GetAllOrderEntriesAsync()
        {
            return await _context.OrderEntries.ToListAsync();
        }

        public async Task<OrderEntry?> GetOrderEntryByIdAsync(int id)
        {
            return await _context.OrderEntries.FindAsync(id);
        }

        public async Task AddOrderEntryAsync(OrderEntry orderEntry)
        {
            await _context.OrderEntries.AddAsync(orderEntry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderEntryAsync(OrderEntry orderEntry)
        {
            _context.OrderEntries.Update(orderEntry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderEntryAsync(int id)
        {
            var orderEntry = await GetOrderEntryByIdAsync(id);
            if (orderEntry != null)
            {
                _context.OrderEntries.Remove(orderEntry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
