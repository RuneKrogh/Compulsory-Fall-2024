using Service.DTOs.Create;
using Service.DTOs.Read;

namespace Service.Interfaces;

public interface IPaperService
{
    Task<IEnumerable<PaperDto>> GetAllPapers(); // Returns all papers as DTOs
    Task<PaperDto?> GetPaperById(int id); // Returns a paper DTO by ID
    Task<PaperDto?> GetPaperByName(string name); // Returns a paper entity by name
    Task<PaperDto> CreatePaper(CreatePaperDto createPaperDto); // Creates a new paper from DTO
    Task UpdatePaper(PaperDto paperDto); // Updates an existing paper using DTO
    Task UpdatePaperStock(PaperStockDto paperStockDtoDto); // Updates an existing papers stock
    Task DeletePaper(int id); // Deletes a paper by I
}