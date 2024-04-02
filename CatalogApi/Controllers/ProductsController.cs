using CatalogApi.Models;
using CatalogApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Controllers;

[Route("api/produtos")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("/config")]
    public async Task<ActionResult<string[]>> GetConfiguration()
    {
        var config1 = _configuration["TestIConfiguration"];
        var config2 = _configuration.GetSection("TestIConfiguration");

        string[] array = { config1, config2.Value };

        return Ok(array);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var products = await _repository.GetProductsAsync().Result.Take(10).ToListAsync();
        if (!products.Any()) return NotFound(products);

        return Ok(products);
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _repository.GetProductAsync(id);
        if (product is null) return NotFound("Produto não encontrado");

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Post(Product product)
    {
        if (product is null) return StatusCode(500, "Falha ao atualizar o produto");
        var newProduct = await _repository.CreateAsync(product);

        return new CreatedAtRouteResult("GetProduct", new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Product product)
    {
        var updatedProduct = await _repository.UpdateAsync(product);
        if (!updatedProduct) return BadRequest();

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        var productDeleted = await _repository.DeleteAsync(id);
        if (!productDeleted) return BadRequest("Ocorreu um erro ao deletar o produto");

        return Ok();
    }
}