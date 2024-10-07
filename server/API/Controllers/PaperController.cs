﻿using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using DataAccess.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaperController : ControllerBase
    {
        private readonly IPaperService _paperService;

        public PaperController(IPaperService paperService)
        {
            _paperService = paperService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paper>>> GetAllPapers()
        {
            var papers = await _paperService.GetAllPapers();
            return Ok(papers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paper>> GetPaperById(int id)
        {
            var paper = await _paperService.GetPaperById(id);
            if (paper == null) return NotFound();
            return Ok(paper);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Paper>> GetPaperByName(string name)
        {
            var paper = await _paperService.GetPaperByName(name);
            if (paper == null) return NotFound();
            return Ok(paper);
        }

        [HttpPost]
        public async Task<ActionResult<Paper>> AddPaper(Paper paper)
        {
            await _paperService.CreatePaper(paper);
            return CreatedAtAction(nameof(GetPaperById), new { id = paper.Id }, paper);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaper(int id, Paper paper)
        {
            if (id != paper.Id) return BadRequest();
            await _paperService.UpdatePaper(paper);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaper(int id)
        {
            await _paperService.DeletePaper(id);
            return NoContent();
        }
    }
}