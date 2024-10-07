namespace Service.DTOs.Create
{
    public class CreatePaperDto
    {
        public string Name { get; set; } = null!;  // Name of the paper
        public bool Discontinued { get; set; }  // Whether the paper is discontinued
        public int Stock { get; set; }  // Stock level of the paper
        public double Price { get; set; }  // Price of the paper
    }
}