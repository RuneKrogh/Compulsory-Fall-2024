namespace Service.DTOs.OrderEntry;

public class UpdateOrderEntryDto
{
    public int Quantity { get; set; }

    public int? ProductId { get; set; }
}