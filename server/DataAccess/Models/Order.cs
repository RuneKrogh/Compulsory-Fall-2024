using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("orders")]
[Index("CustomerId", Name = "IX_orders_customer_id")]
public partial class Order
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("order_date")]
    public DateTime OrderDate { get; set; }

    [Column("delivery_date")]
    public DateOnly? DeliveryDate { get; set; }

    [Column("status")]
    [StringLength(50)]
    public string Status { get; set; } = null!;

    [Column("total_amount")]
    public double TotalAmount { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer? Customer { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderEntry> OrderEntries { get; set; } = new List<OrderEntry>();
}
