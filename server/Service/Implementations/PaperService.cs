﻿using DataAccess;
using DataAccess.Models;
using Service.Interfaces;
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

        public async Task<IEnumerable<Paper>> GetAllPapers()
        {
            // Retrieves all papers from the database
            return await _context.Papers.ToListAsync();
        }

        public async Task<Paper?> GetPaperById(int id)
        {
            // Retrieves a specific paper by ID
            return await _context.Papers.FindAsync(id);
        }

        public async Task<Paper?> GetPaperByName(string name)
        {
            // Retrieves a specific paper by its name
            return await _context.Papers
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task CreatePaper(Paper paper)
        {
            // Adds a new paper to the database
            await _context.Papers.AddAsync(paper);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaper(Paper paper)
        {
            // Updates an existing paper in the database
            _context.Papers.Update(paper);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaper(int id)
        {
            // Deletes a paper from the database by ID
            var paper = await _context.Papers.FindAsync(id);
            if (paper != null)
            {
                _context.Papers.Remove(paper);
                await _context.SaveChangesAsync();
            }
        }
    }
}