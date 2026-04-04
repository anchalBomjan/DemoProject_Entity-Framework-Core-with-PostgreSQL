using Application.Common.ModelViews;
using Application.Common.Responses;
using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.GetAll;
using Application.Products.GetById;
using Application.Products.Update;
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
    public async Task<ActionResult<Response<IEnumerable<ProductViewModel>>>> GetProducts()
    {
        var response = await _mediator.Send(new GetProductsQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<ProductViewModel?>>> GetProduct(int id)
    {
        var response = await _mediator.Send(new GetProductQuery { Id = id });
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<int>>> CreateProduct(CreateProductCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProduct), new { id = response.Data }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Response<bool>>> UpdateProduct(int id, UpdateProductCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(Response<bool>.Failure("URL ID does not match command ID"));
        }

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<bool>>> DeleteProduct(int id)
    {
        var response = await _mediator.Send(new DeleteProductCommand { Id = id });
        return Ok(response);
    }
}
