using DataAccess.Models;

namespace Service.Interfaces;

public interface IPaperService
{
    Task<IEnumerable<Paper>> GetAllPapers();
    Task<Paper?> GetPaperById(int id);
    Task<Paper?> GetPaperByName(string name);
    Task CreatePaper(Paper paper);
    Task UpdatePaper(Paper paper);
    Task DeletePaper(int id);
}
