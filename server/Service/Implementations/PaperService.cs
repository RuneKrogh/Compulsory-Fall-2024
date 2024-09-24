using DataAccess.Models;
using DataAccess.Repositories;
using Service.Interfaces;

namespace Service.Implementations;

public class PaperService : IPaperService
{
    private readonly IPaperRepository _paperRepository;

    public PaperService(IPaperRepository paperRepository)
    {
        _paperRepository = paperRepository;
    }

    public async Task<IEnumerable<Paper>> GetAllPapersAsync()
    {
        return await _paperRepository.GetAllPapersAsync();
    }

    public async Task<Paper?> GetPaperByIdAsync(int id)
    {
        return await _paperRepository.GetPaperByIdAsync(id);
    }

    public async Task<Paper?> GetPaperByNameAsync(string name)
    {
        return await _paperRepository.GetPaperByNameAsync(name);
    }

    public async Task AddPaperAsync(Paper paper)
    {
        await _paperRepository.AddPaperAsync(paper);
    }

    public async Task UpdatePaperAsync(Paper paper)
    {
        await _paperRepository.UpdatePaperAsync(paper);
    }

    public async Task DeletePaperAsync(int id)
    {
        await _paperRepository.DeletePaperAsync(id);
    }
}
