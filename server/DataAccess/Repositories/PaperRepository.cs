using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface IPaperRepository
    {
        Task<IEnumerable<Paper>> GetAllPapersAsync();
        Task<Paper?> GetPaperByIdAsync(int id);
        Task<Paper?> GetPaperByNameAsync(string name);
        Task AddPaperAsync(Paper paper);
        Task UpdatePaperAsync(Paper paper);
        Task DeletePaperAsync(int id);
    }

    public class PaperRepository : IPaperRepository
    {
        private readonly DunderMifflinContext _context;

        public PaperRepository(DunderMifflinContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paper>> GetAllPapersAsync()
        {
            return await _context.Papers.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Paper?> GetPaperByIdAsync(int id)
        {
            return await _context.Papers.FindAsync(id);
        }

        public async Task<Paper?> GetPaperByNameAsync(string name)
        {
            return await _context.Papers.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task AddPaperAsync(Paper paper)
        {
            await _context.Papers.AddAsync(paper);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaperAsync(Paper paper)
        {
            _context.Papers.Update(paper);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaperAsync(int id)
        {
            var paper = await GetPaperByIdAsync(id);
            if (paper != null)
            {
                _context.Papers.Remove(paper);
                await _context.SaveChangesAsync();
            }
        }
    }
}