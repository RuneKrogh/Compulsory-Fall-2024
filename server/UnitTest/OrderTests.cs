using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Create;
using Service.Implementations;
using Service.Interfaces;
using Service.Validation.OrderValidation;

public class OrderTests : IDisposable
{
    private readonly DunderMifflinContext _context;
    private readonly IOrderService _orderService;

    public OrderTests()
    {
        // Create a unique in-memory database for each test instance
        var options = new DbContextOptionsBuilder<DunderMifflinContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DunderMifflinContext(options);
        var createOrderValidation = new CreateOrderValidation();
        _orderService = new OrderService(_context, createOrderValidation); // Assume you have an OrderService
    }

    public void Dispose()
    {
        // Cleanup in-memory database after each test
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task CreateOrder_ValidData_ShouldCreateOrder()
    {
        // Arrange
        var createOrderDto = new CreateOrderDto
        {
            OrderDate = DateTime.Now,
            DeliveryDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
            Status = "Pending",
            TotalAmount = 100.50,
            CustomerId = 1,
            OrderEntries = new List<CreateOrderEntryDto>
            {
                new() { ProductId = 1, Quantity = 2 },
                new() { ProductId = 2, Quantity = 3 }
            }
        };

        // Act
        var orderDto = await _orderService.CreateOrder(createOrderDto);

        // Assert
        Assert.NotNull(orderDto);
        Assert.Equal(createOrderDto.OrderDate, orderDto.OrderDate);
        Assert.Equal(createOrderDto.DeliveryDate, orderDto.DeliveryDate);
        Assert.Equal(createOrderDto.Status, orderDto.Status);
        Assert.Equal(createOrderDto.TotalAmount, orderDto.TotalAmount);
        Assert.Equal(createOrderDto.CustomerId, orderDto.CustomerId);
        Assert.Equal(createOrderDto.OrderEntries.Count, orderDto.OrderEntries.Count);
    }

    [Fact]
    public async Task CreateOrder_InvalidData_ShouldThrowValidationException()
    {
        // Arrange
        var createOrderDto = new CreateOrderDto
        {
            OrderDate = DateTime.Now.AddDays(1), // Invalid: future date
            DeliveryDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
            Status = "", // Invalid: empty status
            TotalAmount = -50, // Invalid: negative amount
            CustomerId = 1,
            OrderEntries = new List<CreateOrderEntryDto> // Invalid: no valid entries
            {
                new() { ProductId = 0, Quantity = 0 } // Invalid entry
            }
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await _orderService.CreateOrder(createOrderDto));
    }

    [Fact]
    public async Task GetAllOrders_ShouldReturnAllOrders()
    {
        // Arrange
        await _orderService.CreateOrder(new CreateOrderDto
        {
            OrderDate = DateTime.Now,
            DeliveryDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
            Status = "Pending",
            TotalAmount = 100.50,
            CustomerId = 1,
            OrderEntries = new List<CreateOrderEntryDto>
            {
                new() { ProductId = 1, Quantity = 2 }
            }
        });

        await _orderService.CreateOrder(new CreateOrderDto
        {
            OrderDate = DateTime.Now,
            DeliveryDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
            Status = "Completed",
            TotalAmount = 150.00,
            CustomerId = 1,
            OrderEntries = new List<CreateOrderEntryDto>
            {
                new() { ProductId = 2, Quantity = 1 }
            }
        });

        // Act
        var orders = await _orderService.GetAllOrders();

        // Assert
        Assert.Equal(2, orders.Count());
    }

    [Fact]
    public async Task GetOrderById_ExistingId_ShouldReturnOrder()
    {
        // Arrange
        var createOrderDto = new CreateOrderDto
        {
            OrderDate = DateTime.Now,
            DeliveryDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
            Status = "Pending",
            TotalAmount = 100.50,
            CustomerId = 1,
            OrderEntries = new List<CreateOrderEntryDto>
            {
                new() { ProductId = 1, Quantity = 2 }
            }
        };

        var orderDto = await _orderService.CreateOrder(createOrderDto);

        // Act
        var retrievedOrder = await _orderService.GetOrderById(orderDto.Id);

        // Assert
        Assert.NotNull(retrievedOrder);
        Assert.Equal(orderDto.Id, retrievedOrder.Id);
    }

    [Fact]
    public async Task GetOrderById_NonExistingId_ShouldReturnNull()
    {
        // Act
        var order = await _orderService.GetOrderById(999); // Non-existing ID

        // Assert
        Assert.Null(order);
    }
}