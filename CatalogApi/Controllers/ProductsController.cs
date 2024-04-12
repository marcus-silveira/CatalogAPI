using CatalogApi.Models;
using CatalogApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers;

[Route("api/produtos")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
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
    [HttpGet("{id}/category")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoryProduct(int id)
    {
        var categories = await _unitOfWork.ProductRepository.GetProductsByCategoryAsync(id);
        if (!categories.Any()) return NotFound(categories);
        return Ok(categories);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var products = (await _unitOfWork.ProductRepository.GetAll()).Take(10).ToList();
        if (!products.Any()) return NotFound(products);

        return Ok(products);
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _unitOfWork.ProductRepository.Get(x => x.Id == id);
        if (product is null) return NotFound("Produto não encontrado");

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Post(Product product)
    {
        if (product is null) return StatusCode(500, "Falha ao atualizar o produto");
        var newProduct = await _unitOfWork.ProductRepository.Create(product);

        return new CreatedAtRouteResult("GetProduct", new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Product product)
    {
        if (await _unitOfWork.ProductRepository.Get(x => x.Id == product.Id) is null) return NotFound("Produto não encontrada"); 
        await _unitOfWork.ProductRepository.Update(product);

        return Ok(product);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        var product = await _unitOfWork.ProductRepository.Get(x => x.Id == id);
        if (product is null) return NotFound("Produto não encontrada");

        var productDeleted = await _unitOfWork.ProductRepository.Delete(product);
        return Ok(productDeleted);
    }
}