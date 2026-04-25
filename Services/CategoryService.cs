using CategoryApi.DTOs;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

public class CategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<object> GetAll(int page, int pageSize, string? search)
    {
        var query = _context.Categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search));
        }

        var total = await query.CountAsync();

        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new { total, data };
    }
    public async Task<Category?> GetById(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category> Create(CategoryCreateDTO dto)
{
    var category = new Category
    {
        Name = dto.Name
    };

    _context.Categories.Add(category);
    await _context.SaveChangesAsync();

    return category;
}

    public async Task<Category> Update(int id, CategoryCreateDTO dto)
{
    var category = await _context.Categories.FindAsync(id);

    if (category == null)
        throw new Exception("Categoria não encontrada");

    category.Name = dto.Name;

    await _context.SaveChangesAsync();

    return category;
}

    public async Task Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            throw new Exception("Categoria não encontrada");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}