using Dem.Application.Features.Commands.Product.Create;
using Dem.Application.Features.Queries.Product.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : BaseController
{

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateProductCommandRequest createProductCommandRequest)
    {
        var response =await Mediator.Send(createProductCommandRequest);
        return Ok(response);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> CreateProduct()
    {
        var response = await Mediator.Send(new ProductGetAllQueryRequest());
        return Ok(response);
    }
}

