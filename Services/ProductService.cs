using ProductApi.Data;
using ProductApi.Models;
using ProductApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Services;

public class ProductService
{

    private async Task Validate(ProductCreateDTO dto, int? productId = null)
    {
        if (dto.Stock < 0)
            throw new Exception("Estoque não pode ser negativo");

        if (string.IsNullOrWhiteSpace(dto.SKU))
            throw new Exception("SKU não pode ser vazio");

        var category = await _context.Categories.FindAsync(dto.CategoryId);
        if (category == null)
            throw new Exception("Categoria inválida");

        if (category.Name == "Eletrônicos" && dto.Price < 50)
            throw new Exception("Preço mínimo para eletrônicos é R$50");

        var exists = await _context.Products
            .AnyAsync(p => p.SKU == dto.SKU && p.Id != productId);

        if (exists)
            throw new Exception("SKU já existe");
    }

    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetAll(int page, int pageSize, string? search)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search));
        }

        var total = await query.CountAsync();

        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(p => p.Category)
            .ToListAsync();

        return new { total, data };
    }

    public async Task<Product?> GetById(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> Create(ProductCreateDTO dto)
    {
        await Validate(dto);

        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            SKU = dto.SKU,
            CategoryId = dto.CategoryId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product> Update(int id, ProductCreateDTO dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            throw new Exception("Produto não encontrado");

        // 👇 AQUI entra o validate
        await Validate(dto, id);

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.SKU = dto.SKU;
        product.CategoryId = dto.CategoryId;

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            throw new Exception("Produto não encontrado");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}