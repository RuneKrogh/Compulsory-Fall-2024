using DataAccess.Models;

namespace Service.DTOs.Read
{
    public class PaperDto
    {
        public int Id { get; set; }  // Unique identifier for the paper
        public string Name { get; set; } = null!;  // Name of the paper
        public bool Discontinued { get; set; }  // Whether the paper is discontinued
        public int Stock { get; set; }  // Stock level of the paper
        public double Price { get; set; }  // Price of the paper

        public List<PropertyDto> Properties { get; set; } = new();  // List of linked properties

        // Method to map a Paper entity to a PaperDto
        public static PaperDto FromPaper(Paper paper)
        {
            return new PaperDto
            {
                Id = paper.Id,
                Name = paper.Name,
                Discontinued = paper.Discontinued,
                Stock = paper.Stock,
                Price = paper.Price,
                Properties = paper.Properties.Select(p => new PropertyDto 
                {
                    Id = p.Id,
                    PropertyName = p.PropertyName
                }).ToList() // Map linked properties to PropertyDto
            };
        }
    }
}