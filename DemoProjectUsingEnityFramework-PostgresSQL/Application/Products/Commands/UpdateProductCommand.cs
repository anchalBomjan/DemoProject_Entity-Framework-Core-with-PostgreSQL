using MediatR;

namespace Application.Products.Commands;

public class UpdateProductCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; } = new();
}