using System;
using DataAccess.Models;

namespace Service.DTOs.Read
{
    public class OrderDto
    {
        public int Id { get; set; }  // Unique identifier for the order
        public DateTime OrderDate { get; set; }  // Date the order was placed
        public DateOnly? DeliveryDate { get; set; }  // Optional delivery date
        public string Status { get; set; } = null!;  // Status of the order
        public double TotalAmount { get; set; }  // Total amount for the order
        public int? CustomerId { get; set; }  // Customer ID associated with the order

        // Method to map an Order entity to an OrderDto
        public static OrderDto FromOrder(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                CustomerId = order.CustomerId
            };
        }
    }
}