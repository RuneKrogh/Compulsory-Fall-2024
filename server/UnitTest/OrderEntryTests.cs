using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Create;
using Service.DTOs.Read;
using Service.Implementations;
using Service.Validation.OrderEntryValidation;

namespace UnitTests;

public class OrderEntryServiceTests : IDisposable
{
    private readonly DunderMifflinContext _context;
    private readonly CreateOrderEntryValidation _createOrderEntryValidation;
    private readonly OrderEntryService _orderEntryService;

    public OrderEntryServiceTests()
    {
        // Create a unique in-memory database for each test instance
        var options = new DbContextOptionsBuilder<DunderMifflinContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DunderMifflinContext(options);
        _createOrderEntryValidation = new CreateOrderEntryValidation();
        _orderEntryService = new OrderEntryService(_context, _createOrderEntryValidation);
    }

    public void Dispose()
    {
        // Cleanup in-memory database after each test
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task CreateOrderEntry_ValidData_ShouldCreateOrderEntry()
    {
        // Arrange
        var createOrderEntryDto = new CreateOrderEntryDto
        {
            Quantity = 10,
            ProductId = 1,
            OrderId = 1
        };

        // Act
        var orderEntryDto = await _orderEntryService.CreateOrderEntry(createOrderEntryDto);

        // Assert
        Assert.NotNull(orderEntryDto);
        Assert.Equal(10, orderEntryDto.Quantity);
        Assert.Equal(1, orderEntryDto.ProductId);
        Assert.Equal(1, orderEntryDto.OrderId);
    }

    [Fact]
    public async Task CreateOrderEntry_InvalidQuantity_ShouldThrowValidationException()
    {
        // Arrange
        var createOrderEntryDto = new CreateOrderEntryDto
        {
            Quantity = 0, // Invalid quantity
            ProductId = 1,
            OrderId = 1
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(
            async () => await _orderEntryService.CreateOrderEntry(createOrderEntryDto)
        );
    }

    [Fact]
    public async Task CreateOrderEntry_InvalidProductId_ShouldThrowValidationException()
    {
        // Arrange
        var createOrderEntryDto = new CreateOrderEntryDto
        {
            Quantity = 10,
            ProductId = 0, // Invalid Product ID
            OrderId = 1
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(
            async () => await _orderEntryService.CreateOrderEntry(createOrderEntryDto)
        );
    }

    [Fact]
    public async Task CreateOrderEntry_InvalidOrderId_ShouldThrowValidationException()
    {
        // Arrange
        var createOrderEntryDto = new CreateOrderEntryDto
        {
            Quantity = 10,
            ProductId = 1,
            OrderId = 0 // Invalid Order ID
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(
            async () => await _orderEntryService.CreateOrderEntry(createOrderEntryDto)
        );
    }

    [Fact]
    public async Task GetAllOrderEntries_ShouldReturnAllOrderEntries()
    {
        // Arrange
        await _orderEntryService.CreateOrderEntry(new CreateOrderEntryDto
        {
            Quantity = 10,
            ProductId = 1,
            OrderId = 1
        });
        await _orderEntryService.CreateOrderEntry(new CreateOrderEntryDto
        {
            Quantity = 5,
            ProductId = 2,
            OrderId = 1
        });

        // Act
        var orderEntries = await _orderEntryService.GetAllOrderEntries();

        // Assert
        Assert.Equal(2, orderEntries.Count());
    }

    [Fact]
    public async Task GetOrderEntryById_ExistingId_ShouldReturnOrderEntry()
    {
        // Arrange
        var orderEntryDto = await _orderEntryService.CreateOrderEntry(new CreateOrderEntryDto
        {
            Quantity = 10,
            ProductId = 1,
            OrderId = 1
        });

        // Act
        var orderEntry = await _orderEntryService.GetOrderEntryById(orderEntryDto.Id);

        // Assert
        Assert.NotNull(orderEntry);
        Assert.Equal(orderEntryDto.Id, orderEntry.Id);
    }

    [Fact]
    public async Task UpdateOrderEntry_ValidData_ShouldUpdateOrderEntry()
    {
        // Arrange
        var orderEntryDto = await _orderEntryService.CreateOrderEntry(new CreateOrderEntryDto
        {
            Quantity = 10,
            ProductId = 1,
            OrderId = 1
        });

        var updatedOrderEntryDto = new OrderEntryDto
        {
            Id = orderEntryDto.Id,
            Quantity = 15,
            ProductId = 1,
            OrderId = 1
        };

        // Act
        await _orderEntryService.UpdateOrderEntry(updatedOrderEntryDto);
        var updatedOrderEntry = await _orderEntryService.GetOrderEntryById(orderEntryDto.Id);

        // Assert
        Assert.Equal(15, updatedOrderEntry.Quantity);
    }

    [Fact]
    public async Task DeleteOrderEntry_ExistingId_ShouldRemoveOrderEntry()
    {
        // Arrange
        var orderEntryDto = await _orderEntryService.CreateOrderEntry(new CreateOrderEntryDto
        {
            Quantity = 10,
            ProductId = 1,
            OrderId = 1
        });

        // Act
        await _orderEntryService.DeleteOrderEntry(orderEntryDto.Id);
        var deletedOrderEntry = await _orderEntryService.GetOrderEntryById(orderEntryDto.Id);

        // Assert
        Assert.Null(deletedOrderEntry);
    }

    [Fact]
    public async Task DeleteOrderEntry_NonExistingId_ShouldNotThrowException()
    {
        // Act
        await _orderEntryService.DeleteOrderEntry(999); // Non-existing ID

        // Assert
        var orderEntries = await _orderEntryService.GetAllOrderEntries();
        Assert.Empty(orderEntries); // Should not throw exception and order entries list should be empty
    }
}