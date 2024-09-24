using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("properties")]
public partial class Property
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("property_name")]
    [StringLength(255)]
    public string PropertyName { get; set; } = null!;

    [ForeignKey("PropertyId")]
    [InverseProperty("Properties")]
    public virtual ICollection<Paper> Papers { get; set; } = new List<Paper>();
}
