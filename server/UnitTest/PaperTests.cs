using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Create;
using Service.DTOs.Read;
using Service.Implementations;
using Service.Validation.PaperValidation;

namespace UnitTests;

public class PaperServiceTests : IDisposable
{
    private readonly DunderMifflinContext _context;
    private readonly CreatePaperValidation _createPaperValidation;
    private readonly PaperService _paperService;
    private readonly UpdatePaperValidation _updatePaperValidation;

    public PaperServiceTests()
    {
        var options = new DbContextOptionsBuilder<DunderMifflinContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DunderMifflinContext(options);
        _createPaperValidation = new CreatePaperValidation();
        _updatePaperValidation = new UpdatePaperValidation();
        _paperService = new PaperService(_context, _createPaperValidation, _updatePaperValidation);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task CreatePaper_ValidData_ShouldCreatePaper()
    {
        // Arrange
        var createPaperDto = new CreatePaperDto
        {
            Name = "A4 Paper",
            Discontinued = false,
            Stock = 100,
            Price = 10.50
        };

        // Act
        var paperDto = await _paperService.CreatePaper(createPaperDto);

        // Assert
        Assert.NotNull(paperDto);
        Assert.Equal("A4 Paper", paperDto.Name);
        Assert.Equal(100, paperDto.Stock);
        Assert.Equal(10.50, paperDto.Price);
    }

    [Fact]
    public async Task CreatePaper_InvalidData_ShouldThrowValidationException()
    {
        // Arrange
        var createPaperDto = new CreatePaperDto
        {
            Name = "", // Invalid name
            Discontinued = false,
            Stock = 100,
            Price = 10.50
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await _paperService.CreatePaper(createPaperDto));
    }

    [Fact]
    public async Task GetAllPapers_ShouldReturnAllPapers()
    {
        // Arrange
        await _paperService.CreatePaper(new CreatePaperDto
        {
            Name = "A4 Paper",
            Discontinued = false,
            Stock = 100,
            Price = 10.50
        });
        await _paperService.CreatePaper(new CreatePaperDto
        {
            Name = "A5 Paper",
            Discontinued = true,
            Stock = 50,
            Price = 5.00
        });

        // Act
        var papers = await _paperService.GetAllPapers();

        // Assert
        Assert.Equal(2, papers.Count());
    }

    [Fact]
    public async Task GetPaperById_ExistingId_ShouldReturnPaper()
    {
        // Arrange
        var createdPaper = await _paperService.CreatePaper(new CreatePaperDto
        {
            Name = "A4 Paper",
            Discontinued = false,
            Stock = 100,
            Price = 10.50
        });

        // Act
        var paper = await _paperService.GetPaperById(createdPaper.Id);

        // Assert
        Assert.NotNull(paper);
        Assert.Equal(createdPaper.Id, paper.Id);
    }

    [Fact]
    public async Task UpdatePaper_ValidData_ShouldUpdatePaper()
    {
        // Arrange
        var createdPaper = await _paperService.CreatePaper(new CreatePaperDto
        {
            Name = "A4 Paper",
            Discontinued = false,
            Stock = 100,
            Price = 10.50
        });

        var updatedPaperDto = new PaperDto
        {
            Id = createdPaper.Id,
            Name = "A4 Premium Paper",
            Discontinued = false,
            Stock = 150,
            Price = 12.00
        };

        // Act
        await _paperService.UpdatePaper(updatedPaperDto);
        var updatedPaper = await _paperService.GetPaperById(createdPaper.Id);

        // Assert
        Assert.Equal("A4 Premium Paper", updatedPaper.Name);
        Assert.Equal(150, updatedPaper.Stock);
        Assert.Equal(12.00, updatedPaper.Price);
    }

    [Fact]
    public async Task UpdatePaper_InvalidData_ShouldThrowValidationException()
    {
        // Arrange
        var createdPaper = await _paperService.CreatePaper(new CreatePaperDto
        {
            Name = "A4 Paper",
            Discontinued = false,
            Stock = 100,
            Price = 10.50
        });

        var updatedPaperDto = new PaperDto
        {
            Id = createdPaper.Id,
            Name = "", // Invalid name
            Discontinued = false,
            Stock = 150,
            Price = 12.00
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await _paperService.UpdatePaper(updatedPaperDto));
    }

    [Fact]
    public async Task DeletePaper_ExistingId_ShouldRemovePaper()
    {
        // Arrange
        var createdPaper = await _paperService.CreatePaper(new CreatePaperDto
        {
            Name = "A4 Paper",
            Discontinued = false,
            Stock = 100,
            Price = 10.50
        });

        // Act
        await _paperService.DeletePaper(createdPaper.Id);
        var deletedPaper = await _paperService.GetPaperById(createdPaper.Id);

        // Assert
        Assert.Null(deletedPaper);
    }

    [Fact]
    public async Task DeletePaper_NonExistingId_ShouldNotThrowException()
    {
        // Act
        await _paperService.DeletePaper(999); // Non-existing ID

        // Assert
        var papers = await _paperService.GetAllPapers();
        Assert.Empty(papers); // Should not throw exception and papers list should be empty
    }
}