using Limon.Hive.E.Bazar.Application.Actions.Products;
using Limon.Hive.E.Bazar.Application.Actions.Products.Command;
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
}
