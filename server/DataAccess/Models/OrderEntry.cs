using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("order_entries")]
[Index("OrderId", Name = "IX_order_entries_order_id")]
[Index("ProductId", Name = "IX_order_entries_product_id")]
public partial class OrderEntry
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("product_id")]
    public int? ProductId { get; set; }

    [Column("order_id")]
    public int? OrderId { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderEntries")]
    public virtual Order? Order { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("OrderEntries")]
    public virtual Paper? Product { get; set; }
}
