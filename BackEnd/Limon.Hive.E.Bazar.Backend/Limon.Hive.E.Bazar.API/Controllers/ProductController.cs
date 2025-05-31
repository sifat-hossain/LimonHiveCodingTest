using Limon.Hive.E.Bazar.Application.Actions.Products;
using Limon.Hive.E.Bazar.Application.Actions.Products.Command;
using Limon.Hive.E.Bazar.Application.Actions.Products.Query.PullProductById;
using Limon.Hive.E.Bazar.Application.Actions.Products.Query.PullProductByName;
using Limon.Hive.E.Bazar.Application.Actions.Products.Query.PullProducts;
using Limon.Hive.E.Bazar.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Limon.Hive.E.Bazar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProductController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<LimonHiveActionResponse<ProductModel>> Insert(ProductCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<ProductResponse> Pull(int? skip, int? take)
    {
        return await _mediator.Send(new ProductQueryRequest()
        {
            Skip = skip,
            Take = take
        });
    }

    [HttpGet("{productId}")]
    public async Task<ProductModel> GetProductById(Guid productId)
    {
        return await _mediator.Send(new PullProductByIdQueryRequest()
        {
            ProductId = productId
        });
    }

    [HttpGet("GetProductByName/{productName}")]
    public async Task<ProductResponse> GetProductByName(string productName)
    {
        return await _mediator.Send(new PullProductByNameQueryRequest()
        {
            ProductName = productName
        });
    }
}
