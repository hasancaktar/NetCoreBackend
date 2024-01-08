using Microsoft.AspNetCore.Mvc;

namespace Dem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
{

    [HttpGet("GetProduct")]
    public IActionResult GetProduct(int id)
    {
        var response = Mediator.Send(id);
        return Ok(response);
    }
}

