using Microsoft.AspNetCore.Mvc;
using ProductApi.Services;
using ProductApi.DTOs;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ProductService service, ILogger<ProductController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10, string? search = null)
    {
        _logger.LogInformation("Buscando produtos - page: {Page}, search: {Search}", page, search);

        var result = await _service.GetAll(page, pageSize, search);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Buscando produto por ID: {Id}", id);

        var product = await _service.GetById(id);
        if (product == null)
        {
            _logger.LogWarning("Produto não encontrado: ID {Id}", id);
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDTO dto)
    {
        try
        {
            var result = await _service.Create(dto);

            _logger.LogInformation("Produto criado: {Name} - SKU: {SKU}", dto.Name, dto.SKU);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar produto: {Name}", dto.Name);

            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductCreateDTO dto)
    {
        try
        {
            var result = await _service.Update(id, dto);

            _logger.LogInformation("Produto atualizado: ID {Id}", id);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar produto ID {Id}", id);

            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.Delete(id);

            _logger.LogWarning("Produto deletado: ID {Id}", id);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar produto ID {Id}", id);

            return BadRequest(new { message = ex.Message });
        }
    }
}