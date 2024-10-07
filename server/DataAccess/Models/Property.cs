using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

[Table("properties")]
public class Property
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("property_name")]
    [StringLength(255)]
    public string PropertyName { get; set; } = null!;

    [ForeignKey("PropertyId")]
    [InverseProperty("Properties")]
    public virtual ICollection<Paper> Papers { get; set; } = new List<Paper>();
}