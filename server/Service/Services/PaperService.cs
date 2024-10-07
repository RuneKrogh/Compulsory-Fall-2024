using DataAccess;
using DataAccess.Models;
using FluentValidation;
using Service.Interfaces;
using Service.DTOs.Read;
using Service.DTOs.Create;
using Microsoft.EntityFrameworkCore;
using Service.Validation.PaperValidation;

namespace Service.Implementations
{
    public class PaperService : IPaperService
    {
        private readonly DunderMifflinContext _context;
        private readonly CreatePaperValidation _createPaperValidation;
        private readonly UpdatePaperValidation _updatePaperValidation;

        public PaperService(DunderMifflinContext context, CreatePaperValidation createPaperValidation, UpdatePaperValidation updatePaperValidation)
        {
            _context = context;
            _createPaperValidation = createPaperValidation;
            _updatePaperValidation = updatePaperValidation;
        }

        public async Task<IEnumerable<PaperDto>> GetAllPapers()
        {
            // Returns all papers as DTOs, including linked properties
            return await _context.Papers
                .Include(p => p.Properties) // Include the properties
                .OrderBy(paper => paper.Id)
                .Select(paper => new PaperDto
                {
                    Id = paper.Id,
                    Name = paper.Name,
                    Discontinued = paper.Discontinued,
                    Stock = paper.Stock,
                    Price = paper.Price,
                    Properties = paper.Properties.Select(prop => new PropertyDto
                    {
                        Id = prop.Id,
                        PropertyName = prop.PropertyName
                    }).ToList() // Map linked properties to PropertyDto
                })
                .ToListAsync();
        }

        public async Task<PaperDto?> GetPaperById(int id)
        {
            // Returns a paper DTO by ID, including linked properties
            var paper = await _context.Papers
                .Include(p => p.Properties) // Include the properties
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paper == null)
                return null;

            return new PaperDto
            {
                Id = paper.Id,
                Name = paper.Name,
                Discontinued = paper.Discontinued,
                Stock = paper.Stock,
                Price = paper.Price,
                Properties = paper.Properties.Select(prop => new PropertyDto
                {
                    Id = prop.Id,
                    PropertyName = prop.PropertyName
                }).ToList() // Map linked properties to PropertyDto
            };
        }

        public async Task<PaperDto?> GetPaperByName(string name)
        {
            // Returns a paper DTO by name, including linked properties
            var paper = await _context.Papers
                .Include(p => p.Properties) // Include the properties
                .FirstOrDefaultAsync(p => p.Name == name);

            if (paper == null)
                return null;

            return new PaperDto
            {
                Id = paper.Id,
                Name = paper.Name,
                Discontinued = paper.Discontinued,
                Stock = paper.Stock,
                Price = paper.Price,
                Properties = paper.Properties.Select(prop => new PropertyDto
                {
                    Id = prop.Id,
                    PropertyName = prop.PropertyName
                }).ToList() // Map linked properties to PropertyDto
            };
        }

        public async Task<PaperDto> CreatePaper(CreatePaperDto createPaperDto)
        {
            await _createPaperValidation.ValidateAndThrowAsync(createPaperDto);
            
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
            await _updatePaperValidation.ValidateAndThrowAsync(paperDto);
            
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
