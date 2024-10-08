﻿using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Create;
using Service.DTOs.Read;
using Service.Interfaces;

namespace API.Controllers;

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
    public async Task<ActionResult<IEnumerable<PaperDto>>> GetAllPapers()
    {
        var papers = await _paperService.GetAllPapers();
        return Ok(papers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaperDto>> GetPaperById(int id)
    {
        var paper = await _paperService.GetPaperById(id);
        if (paper == null) return NotFound();
        return Ok(paper);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<PaperDto>> GetPaperByName(string name)
    {
        var paper = await _paperService.GetPaperByName(name);
        if (paper == null) return NotFound();
        return Ok(paper);
    }

    [HttpPost]
    public async Task<ActionResult<PaperDto>> AddPaper(CreatePaperDto createPaperDto)
    {
        var createdPaper = await _paperService.CreatePaper(createPaperDto);
        return CreatedAtAction(nameof(GetPaperById), new { id = createdPaper.Id }, createdPaper);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePaper(int id, PaperDto paperDto)
    {
        if (id != paperDto.Id) return BadRequest();
        await _paperService.UpdatePaper(paperDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePaper(int id)
    {
        await _paperService.DeletePaper(id);
        return NoContent();
    }

    [HttpPatch("update-stock")]
    public async Task<IActionResult> UpdatePaperStock([FromBody] PaperStockDto paperStockDto)
    {
        // Make sure the DTO is valid
        if (paperStockDto == null || paperStockDto.Stock < 0) return BadRequest("Invalid stock update data.");

        // Call service to update the stock
        await _paperService.UpdatePaperStock(paperStockDto);

        return NoContent(); // Return a 204 status indicating success without a response body
    }
}