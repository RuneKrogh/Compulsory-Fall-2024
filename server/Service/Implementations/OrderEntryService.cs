using DataAccess;
using DataAccess.Models;
using FluentValidation;
using Service.Interfaces;
using Service.DTOs.Create;
using Service.DTOs.Read;
using Microsoft.EntityFrameworkCore;
using Service.Validation.OrderEntryValidation;

namespace Service.Implementations
{
    public class OrderEntryService : IOrderEntryService
    {
        private readonly DunderMifflinContext _context;
        private readonly CreateOrderEntryValidation _createOrderEntryValidation; // Add this line

        public OrderEntryService(DunderMifflinContext context, CreateOrderEntryValidation createOrderEntryValidation) // Update constructor
        {
            _context = context;
            _createOrderEntryValidation = createOrderEntryValidation; // Initialize the validator
        }

        public async Task<IEnumerable<OrderEntryDto>> GetAllOrderEntries()
        {
            // Retrieves all order entries from the database and maps them to DTOs
            return await _context.OrderEntries
                .Select(orderEntry => OrderEntryDto.FromOrderEntry(orderEntry)) // Use the DTO mapping method
                .ToListAsync();
        }

        public async Task<OrderEntryDto?> GetOrderEntryById(int id)
        {
            // Retrieves a specific order entry by ID and maps it to a DTO
            var orderEntry = await _context.OrderEntries.FindAsync(id);
            return orderEntry != null ? OrderEntryDto.FromOrderEntry(orderEntry) : null; // Use the DTO mapping method
        }

        public async Task<OrderEntryDto> CreateOrderEntry(CreateOrderEntryDto createOrderEntryDto)
        {
            
            await _createOrderEntryValidation.ValidateAndThrowAsync(createOrderEntryDto);
            
            // Creates a new OrderEntry based on the DTO
            var orderEntry = new OrderEntry
            {
                Quantity = createOrderEntryDto.Quantity,
                ProductId = createOrderEntryDto.ProductId,
                OrderId = createOrderEntryDto.OrderId
            };

            await _context.OrderEntries.AddAsync(orderEntry);
            await _context.SaveChangesAsync();

            // Return the created OrderEntry as a DTO
            return OrderEntryDto.FromOrderEntry(orderEntry); // Use the DTO mapping method
        }

        public async Task UpdateOrderEntry(OrderEntryDto orderEntryDto)
        {
            // Updates an existing order entry in the database
            var orderEntry = await _context.OrderEntries.FindAsync(orderEntryDto.Id);
            if (orderEntry == null) return; // Handle case appropriately

            orderEntry.Quantity = orderEntryDto.Quantity;
            orderEntry.ProductId = orderEntryDto.ProductId;
            orderEntry.OrderId = orderEntryDto.OrderId;

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
