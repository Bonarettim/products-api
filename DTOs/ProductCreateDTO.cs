namespace ProductApi.DTOs;

public class ProductCreateDTO
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string SKU { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}