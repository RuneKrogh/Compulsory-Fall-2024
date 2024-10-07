﻿using DataAccess;
using DataAccess.Models;
using Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Service.Implementations
{
    public class OrderEntryService : IOrderEntryService
    {
        private readonly DunderMifflinContext _context;

        public OrderEntryService(DunderMifflinContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderEntry>> GetAllOrderEntries()
        {
            // Retrieves all order entries from the database
            return await _context.OrderEntries.ToListAsync();
        }

        public async Task<OrderEntry?> GetOrderEntryById(int id)
        {
            // Retrieves a specific order entry by ID
            return await _context.OrderEntries.FindAsync(id);
        }

        public async Task CreateOrderEntry(OrderEntry orderEntry)
        {
            // Adds a new order entry to the database
            await _context.OrderEntries.AddAsync(orderEntry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderEntry(OrderEntry orderEntry)
        {
            // Updates an existing order entry in the database
            _context.OrderEntries.Update(orderEntry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderEntry(int id)
        {
            // Deletes an order entry from the database by ID
            var orderEntry = await _context.OrderEntries.FindAsync(id);
            if (orderEntry != null)
            {
                _context.OrderEntries.Remove(orderEntry);
                await _context.SaveChangesAsync();
            }
        }
    }
}