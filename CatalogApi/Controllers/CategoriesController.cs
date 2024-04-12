using CatalogApi.Models;
using CatalogApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(IUnitOfWork unitOfWork, ILogger<CategoriesController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        var categories = (await _unitOfWork.CategoryRepository.GetAll()).Take(10).ToList();
        if (categories.Any()) return NotFound(categories);
        return Ok(categories);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var category = await _unitOfWork.CategoryRepository.Get(x => x.Id == id);
        if (category is not null) return Ok(category);
        _logger.LogWarning($"Categoria de ID = {id} não encontrada.");
        return NotFound("Categoria não encontrada");

    }

    [HttpPost]
    public async Task<IActionResult> Post(Category category)
    {
        if (category is null)
        {
            _logger.LogWarning("Dados inválidos");
            return BadRequest("Dados Inválidos");
        }

        var categoryCreated = await _unitOfWork.CategoryRepository.Create(category);
        return new CreatedAtRouteResult("GetCategory", new { id = categoryCreated.Id }, categoryCreated);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult<Category>?> Put(int id, Category category)
    {
        if (await _unitOfWork.CategoryRepository.Get(x => x.Id == id) is null)
            return NotFound("Categoria não encontrada");
        if (id != category.Id) return BadRequest("Dados inválidos");
        await _unitOfWork.CategoryRepository.Update(category);
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Category>> Delete(int id)
    {
        var category = await _unitOfWork.CategoryRepository.Get(x => x.Id == id);
        if (category is null) return NotFound("Categoria não encontrada");

        var categoryDeleted = await _unitOfWork.CategoryRepository.Delete(category);
        return Ok(categoryDeleted);
    }
}