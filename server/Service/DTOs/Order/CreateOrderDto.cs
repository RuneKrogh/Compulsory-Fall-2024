using Service.DTOs.OrderEntry;

namespace Service.DTOs.Order;

public class CreateOrderDto
{
    public DateOnly? DeliveryDate { get; set; }

    public string Status { get; set; } = "pending";

    public double TotalAmount { get; set; }

    public int? CustomerId { get; set; }

    public List<CreateOrderEntryDto> OrderEntries { get; set; } = new List<CreateOrderEntryDto>();
}