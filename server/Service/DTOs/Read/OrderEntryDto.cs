using DataAccess.Models;

namespace Service.DTOs.Read
{
    public class OrderEntryDto
    {
        public int Id { get; set; }  // Unique identifier for the order entry
        public int Quantity { get; set; }  // Quantity of the product
        public int ProductId { get; set; }  // Associated product ID
        public int OrderId { get; set; }  // Associated order ID
        
        
        // Method to map OrderEntry to OrderEntryDto
        public static OrderEntryDto FromOrderEntry(OrderEntry orderEntry)
        {
            return new OrderEntryDto
            {
                Id = orderEntry.Id,
                Quantity = orderEntry.Quantity,
                ProductId = orderEntry.ProductId ?? 0, // Handle nullable
                OrderId = orderEntry.OrderId ?? 0 // Handle nullable
            };
        }
    }
}