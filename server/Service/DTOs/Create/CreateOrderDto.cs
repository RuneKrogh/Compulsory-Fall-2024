using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Create
{
    public class CreateOrderDto
    {
        [Required]
        public DateTime OrderDate { get; set; }

        public DateOnly? DeliveryDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = null!;

        [Required]
        public double TotalAmount { get; set; }

        public int? CustomerId { get; set; } // Optional, to link to a customer

        // Add this property to accept order entries
        [Required]
        public List<CreateOrderEntryDto> OrderEntries { get; set; } = new List<CreateOrderEntryDto>();
    }
}