using CatalogApi.Context;
using CatalogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Controllers;

[Route("api/produtos")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly CatalogApiDbContext _dbContext;

    public ProductsController(CatalogApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        try
        {
            var products = await _dbContext.Products.Take(10).ToListAsync();
            if (products is null) return NotFound();

            return Ok(products);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao precessar a requisição");
        }
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        try
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is null) return NotFound("Produto não encontrado");

            return Ok(product);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao precessar a requisição");
        }
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        try
        {
            if (product is null) return BadRequest();
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao precessar a requisição");
        }
    }

    [HttpPut("{id}")]
    public ActionResult<Product> Put(int id, Product product)
    {
        try
        {
            if (id != product.Id) return BadRequest();
            _dbContext.Entry(product).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(product);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao precessar a requisição");
        }
    }

    [HttpDelete]
    public ActionResult<Product> Delete(int id)
    {
        try
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) return NotFound();
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return Ok(product);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao precessar a requisição");
        }
    }
}