using DataAccess;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Create;
using Service.DTOs.Read;
using Service.Interfaces;
using Service.Validation.OrderValidation;

namespace Service.Implementations;

public class OrderService : IOrderService
{
    private readonly DunderMifflinContext _context;
    private readonly CreateOrderValidation _createOrderValidation;

    public OrderService(DunderMifflinContext context, CreateOrderValidation validation)
    {
        _createOrderValidation = validation;
        _context = context;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrders()
    {
        // Retrieves all orders from the database and maps them to DTOs
        return await _context.Orders
            .Include(order => order.OrderEntries) // Include order entries
            .OrderBy(order => order.Id)
            .Select(order => OrderDto.FromOrder(order)) // Use the FromOrder method for mapping
            .ToListAsync();
    }

    public async Task<OrderDto?> GetOrderById(int id)
    {
        // Retrieves a specific order by ID and maps it to DTO
        var order = await _context.Orders
            .Include(o => o.OrderEntries) // Include order entries
            .FirstOrDefaultAsync(o => o.Id == id); // Use FirstOrDefault for more safety
        return order == null ? null : OrderDto.FromOrder(order); // Use the FromOrder method
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerId(int customerId)
    {
        // Retrieves orders for a specific customer and maps them to DTOs
        return await _context.Orders
            .Include(order => order.OrderEntries) // Include order entries
            .Where(order => order.CustomerId == customerId)
            .Select(order => OrderDto.FromOrder(order)) // Use the FromOrder method for mapping
            .ToListAsync();
    }

    public async Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto)
    {
        await _createOrderValidation.ValidateAndThrowAsync(createOrderDto);

        var order = new Order
        {
            OrderDate = createOrderDto.OrderDate,
            DeliveryDate = createOrderDto.DeliveryDate,
            Status = createOrderDto.Status,
            TotalAmount = createOrderDto.TotalAmount,
            CustomerId = createOrderDto.CustomerId
        };

        // Include the order entries if provided
        foreach (var entry in createOrderDto.OrderEntries)
        {
            var orderEntry = new OrderEntry
            {
                ProductId = entry.ProductId,
                Quantity = entry.Quantity,
                Order = order // Link back to the order
            };
            order.OrderEntries.Add(orderEntry); // Add entry to the order
        }

        // Save the order with entries to the database
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        return OrderDto.FromOrder(order);
    }

    public async Task UpdateOrder(OrderDto orderDto)
    {
        // Updates an existing order in the database
        var order = await _context.Orders
            .Include(o => o.OrderEntries) // Include order entries for updates
            .FirstOrDefaultAsync(o => o.Id == orderDto.Id);

        if (order == null) return; // Handle case appropriately

        order.OrderDate = orderDto.OrderDate;
        order.DeliveryDate = orderDto.DeliveryDate;
        order.Status = orderDto.Status;
        order.TotalAmount = orderDto.TotalAmount;
        order.CustomerId = orderDto.CustomerId;

        // Optionally update order entries here if needed
        // For now, we assume the entries are managed externally.

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