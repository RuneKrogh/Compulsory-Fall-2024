﻿using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Create;
using Service.DTOs.Read;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderEntryController : ControllerBase
{
    private readonly IOrderEntryService _orderEntryService;

    public OrderEntryController(IOrderEntryService orderEntryService)
    {
        _orderEntryService = orderEntryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderEntryDto>>> GetAllOrderEntries()
    {
        var orderEntries = await _orderEntryService.GetAllOrderEntries();
        return Ok(orderEntries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderEntryDto>> GetOrderEntryById(int id)
    {
        var orderEntry = await _orderEntryService.GetOrderEntryById(id);
        if (orderEntry == null) return NotFound();
        return Ok(orderEntry);
    }

    [HttpPost]
    public async Task<ActionResult<OrderEntryDto>> CreateOrderEntry(CreateOrderEntryDto createOrderEntryDto)
    {
        var orderEntry = await _orderEntryService.CreateOrderEntry(createOrderEntryDto);
        return CreatedAtAction(nameof(GetOrderEntryById), new { id = orderEntry.Id }, orderEntry);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderEntry(int id, OrderEntryDto orderEntryDto)
    {
        if (id != orderEntryDto.Id) return BadRequest();
        await _orderEntryService.UpdateOrderEntry(orderEntryDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderEntry(int id)
    {
        await _orderEntryService.DeleteOrderEntry(id);
        return NoContent();
    }
}