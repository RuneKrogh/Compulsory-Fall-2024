using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("paper")]
[Index("Name", Name = "unique_product_name", IsUnique = true)]
public partial class Paper
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("discontinued")]
    public bool Discontinued { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [Column("price")]
    public double Price { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<OrderEntry> OrderEntries { get; set; } = new List<OrderEntry>();

    [ForeignKey("PaperId")]
    [InverseProperty("Papers")]
    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
