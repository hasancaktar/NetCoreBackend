using Dem.Application.Consts;
using Dem.Application.CustomAttributes;
using Dem.Application.Enums;
using Dem.Application.Features.Commands.Product.Create;
using Dem.Application.Features.Queries.Product.GetAll;
using Dem.WebApi.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using LoggingAttribute = Dem.WebApi.CustomAttributes.LoggingAttribute;

namespace Dem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[TypeFilter(typeof(LoggingAttribute))]
public class ProductsController : BaseController
{
    [HttpPost("Create")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Product, ActionType = ActionType.Writing, Definition = "Add Product Item")]
    public async Task<IActionResult> Create(CreateProductCommandRequest createProductCommandRequest)
    {
        var response = await Mediator.Send(createProductCommandRequest);
        return Ok(response);
    }

    [HttpGet("GetAll")]
    [TypeFilter(typeof(LoggingAttribute))]
    //[Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Product, ActionType = ActionType.Reading, Definition = "Get Product Items")]
    public async Task<IActionResult> CreateProduct()
    {
        var response = await Mediator.Send(new ProductGetAllQueryRequest());
        return Ok(response);
    }

    [HttpGet("Test Localization")]
    public async Task<IActionResult> Test()
    {
        var response = new { res = Localize["Test"].Value };

        return Ok(response);
    }
}