using CatalogApi.Models;
using CatalogApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController(ICategoryRepository repository, ILogger<CategoriesController> logger)
    : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger = logger;
    private readonly ICategoryRepository _repository = repository;

    [HttpGet("{id:int:min(1)}/products")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoryProduct(int id)
    {
        var categories = await _repository.GetCategoryAndProduct(id);
        if (!categories.Any()) return NotFound(categories);
        return Ok(categories);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        var categories = await _repository.GetCategories();
        if (categories.Any()) return NotFound(categories);
        return Ok(categories);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var category = await _repository.GetCategory(id);
        if (category is null)
        {
            _logger.LogWarning($"Categoria de ID = {id} não encontrada.");
            return NotFound("Categoria não encontrada");
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Category category)
    {
        if (category is null)
        {
            _logger.LogWarning("Dados inválidos");
            return BadRequest("Dados Inválidos");
        }

        var categoryCreated = await _repository.Create(category);
        return new CreatedAtRouteResult("GetCategory", new { id = categoryCreated.Id }, categoryCreated);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult<Category>?> Put(int id, Category category)
    {
        if (await _repository.GetCategory(id) is null) return NotFound("Categoria não encontrada");
        if (id != category.Id) return BadRequest("Dados inválidos");
        await _repository.Update(category);
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Category>> Delete(int id)
    {
        var category = await _repository.Delete(id);
        if (!category) return NotFound("Categoria não encontrada");
        return Ok(category);
    }
}