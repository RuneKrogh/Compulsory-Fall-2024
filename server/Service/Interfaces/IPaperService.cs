using DataAccess.Models;

namespace Service.Interfaces;

public interface IPaperService
{
    Task<IEnumerable<Paper>> GetAllPapersAsync();
    Task<Paper?> GetPaperByIdAsync(int id);
    Task<Paper?> GetPaperByNameAsync(string name);
    Task AddPaperAsync(Paper paper);
    Task UpdatePaperAsync(Paper paper);
    Task DeletePaperAsync(int id);
}
