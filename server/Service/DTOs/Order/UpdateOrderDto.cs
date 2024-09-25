namespace Service.DTOs.Order;

public class UpdateOrderDto
{
    public DateOnly? DeliveryDate { get; set; }

    public string Status { get; set; } = "pending";
    
}