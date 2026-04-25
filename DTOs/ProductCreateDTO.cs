using System.ComponentModel.DataAnnotations;

namespace ProductApi.DTOs;

public class ProductCreateDTO
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    
    [Required(ErrorMessage = "SKU é obrigatório")]
    public string ? SKU { get; set; }
    public int CategoryId { get; set; }
}