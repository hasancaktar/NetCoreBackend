using Dem.Application.Features.Commands.Product.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : BaseController
{

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct(CreateProductCommandRequest createProductCommandRequest)
    {
        var response =await Mediator.Send(createProductCommandRequest);
        return Ok(response);
    }
}

