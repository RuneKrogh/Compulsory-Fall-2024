using Service.DTOs.Paper;

namespace Service.DTOs.OrderEntry;

public class OrderEntryDto
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public PaperDto? Product { get; set; }
}