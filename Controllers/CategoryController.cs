using CategoryApi.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(CategoryService service, ILogger<CategoryController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10, string? search = null)
    {
        _logger.LogInformation("Buscando categorias - page: {Page}, search: {Search}", page, search);

        var result = await _service.GetAll(page, pageSize, search);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Buscando categoria por ID: {Id}", id);

        var category = await _service.GetById(id);

        if (category == null)
        {
            _logger.LogWarning("Categoria não encontrada: ID {Id}", id);
            return NotFound(new { message = "Categoria não encontrada" });
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            _logger.LogWarning("Tentativa de criar categoria sem nome");
            return BadRequest(new { message = "Nome é obrigatório" });
        }

        try
        {
            var category = await _service.Create(dto);

            _logger.LogInformation("Categoria criada: {Name} (ID: {Id})", category.Name, category.Id);

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar categoria: {Name}", dto.Name);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryCreateDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            _logger.LogWarning("Tentativa de atualizar categoria com nome vazio - ID {Id}", id);
            return BadRequest(new { message = "Nome é obrigatório" });
        }

        try
        {
            var category = await _service.Update(id, dto);

            _logger.LogInformation("Categoria atualizada: ID {Id}", id);

            return Ok(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar categoria ID {Id}", id);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.Delete(id);

            _logger.LogWarning("Categoria deletada: ID {Id}", id);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar categoria ID {Id}", id);
            return BadRequest(new { message = ex.Message });
        }
    }
}