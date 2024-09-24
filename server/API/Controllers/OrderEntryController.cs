using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using DataAccess.Models;

namespace API.Controllers
{
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
        public async Task<ActionResult<IEnumerable<OrderEntry>>> GetAllOrderEntries()
        {
            var orderEntries = await _orderEntryService.GetAllOrderEntriesAsync();
            return Ok(orderEntries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderEntry>> GetOrderEntryById(int id)
        {
            var orderEntry = await _orderEntryService.GetOrderEntryByIdAsync(id);
            if (orderEntry == null) return NotFound();
            return Ok(orderEntry);
        }

        [HttpPost]
        public async Task<ActionResult<OrderEntry>> AddOrderEntry(OrderEntry orderEntry)
        {
            await _orderEntryService.AddOrderEntryAsync(orderEntry);
            return CreatedAtAction(nameof(GetOrderEntryById), new { id = orderEntry.Id }, orderEntry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderEntry(int id, OrderEntry orderEntry)
        {
            if (id != orderEntry.Id) return BadRequest();
            await _orderEntryService.UpdateOrderEntryAsync(orderEntry);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderEntry(int id)
        {
            await _orderEntryService.DeleteOrderEntryAsync(id);
            return NoContent();
        }
    }
}