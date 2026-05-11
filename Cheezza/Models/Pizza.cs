namespace Cheezza.Models;

public class Pizza
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = "/images/default-pizza.jpg";
    public bool IsAvailable { get; set; } = true;
}