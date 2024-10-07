using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Create
{
    public class CreateOrderEntryDto
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; } // Product being ordered

        [Required]
        public int OrderId { get; set; } // Associated order ID
    }
}