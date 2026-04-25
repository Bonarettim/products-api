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


    public async Task<List<Category>> GetAll(int page, int pageSize)
    {
        return await _context.Categories
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
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