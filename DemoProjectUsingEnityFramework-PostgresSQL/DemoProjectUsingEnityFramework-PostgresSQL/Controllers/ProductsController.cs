using Application.Products.Commands;
using Application.Products.Queries;
using DomainLayer.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectUsingEnityFramework_PostgresSQL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _mediator.Send(new GetProductQuery { Id = id });
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
    {
        var productId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProduct), new { id = productId }, productId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _mediator.Send(new DeleteProductCommand { Id = id });
        return NoContent();
    }
}