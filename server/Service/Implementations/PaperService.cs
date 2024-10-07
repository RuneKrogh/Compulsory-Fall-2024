using DataAccess;
using DataAccess.Models;
using Service.Interfaces;
using Service.DTOs.Read;
using Service.DTOs.Create;
using Microsoft.EntityFrameworkCore;

namespace Service.Implementations
{
    public class PaperService : IPaperService
    {
        private readonly DunderMifflinContext _context;

        public PaperService(DunderMifflinContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaperDto>> GetAllPapers()
        {
            // Returns all papers as DTOs
            return await _context.Papers
                .OrderBy(paper => paper.Id)
                .Select(paper => new PaperDto
                {
                    Id = paper.Id,
                    Name = paper.Name,
                    Discontinued = paper.Discontinued,
                    Stock = paper.Stock,
                    Price = paper.Price
                })
                .ToListAsync();
        }

        public async Task<PaperDto?> GetPaperById(int id)
        {
            // Returns a paper DTO by ID
            var paper = await _context.Papers.FindAsync(id);
            return paper != null ? new PaperDto
            {
                Id = paper.Id,
                Name = paper.Name,
                Discontinued = paper.Discontinued,
                Stock = paper.Stock,
                Price = paper.Price
            } : null;
        }

        public async Task<PaperDto?> GetPaperByName(string name)
        {
            // Returns a paper DTO by name
            var paper = await _context.Papers.FirstOrDefaultAsync(p => p.Name == name);
            return paper != null ? new PaperDto
            {
                Id = paper.Id,
                Name = paper.Name,
                Discontinued = paper.Discontinued,
                Stock = paper.Stock,
                Price = paper.Price
            } : null;
        }

        public async Task<PaperDto> CreatePaper(CreatePaperDto createPaperDto)
        {
            // Creates a new paper from DTO
            var paper = new Paper
            {
                Name = createPaperDto.Name,
                Discontinued = createPaperDto.Discontinued,
                Stock = createPaperDto.Stock,
                Price = createPaperDto.Price
            };

            await _context.Papers.AddAsync(paper);
            await _context.SaveChangesAsync();

            // Return the created paper as a DTO
            return PaperDto.FromPaper(paper);
        }

        public async Task UpdatePaper(PaperDto paperDto)
        {
            // Updates an existing paper using DTO
            var paper = await _context.Papers.FindAsync(paperDto.Id);
            if (paper != null)
            {
                paper.Name = paperDto.Name;
                paper.Discontinued = paperDto.Discontinued;
                paper.Stock = paperDto.Stock;
                paper.Price = paperDto.Price;

                _context.Papers.Update(paper);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePaper(int id)
        {
            // Deletes a paper by ID
            var paper = await _context.Papers.FindAsync(id);
            if (paper != null)
            {
                _context.Papers.Remove(paper);
                await _context.SaveChangesAsync();
            }
        }
    }
}
