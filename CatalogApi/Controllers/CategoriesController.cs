using CatalogApi.Context;
using CatalogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CatalogApiDbContext _dbContext;

    public CategoriesController(CatalogApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id}/products")]
    public ActionResult<IEnumerable<Category>> GetCategoryProcut(int id)
    {
        try
        {
            var categories = _dbContext.Categories.Include(p => p.Products).Where(x => x.Id == id).ToList();
            if (categories is null) return NotFound();
            return Ok(categories);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao precessar a requisição");
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> Get()
    {
        try
        {
            var categories = _dbContext.Categories.ToList();
            if (categories is null) return NotFound();
            return Ok(categories);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao precessar a requisição");
        }
    }

    [HttpGet("{id}", Name = "GetCategory")]
    public ActionResult<Category> GetById(int id)
    {
        try
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category is null) return NotFound("Produto não encontrado");
            return Ok(category);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao precessar a requisição");
        }
    }

    [HttpPost]
    public ActionResult Post(Category category)
    {
        try
        {
            if (category is null) return BadRequest();
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return new CreatedAtRouteResult("GetCategory", new { id = category.Id }, category);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao precessar a requisição");
        }
    }

    [HttpPut("{id}")]
    public ActionResult<Category> Put(int id, Category category)
    {
        try
        {
            if (id != category.Id) return BadRequest();
            _dbContext.Entry(category).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(category);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao precessar a requisição");
        }
    }

    [HttpDelete]
    public ActionResult<Category> Delete(int id)
    {
        try
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category is null) return NotFound();
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();

            return Ok(category);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao precessar a requisição");
        }
    }
}