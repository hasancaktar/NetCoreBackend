using Dem.Application.Abstraction.Configurations;
using Dem.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;

namespace Dem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationServicesController(IApplicationService _applicationService) : ControllerBase
{
    [HttpGet("GetAuthorizeDefinitionEndpoints")]
    [AuthorizeDefinition()]
    public IActionResult GetAuthorizeDefinitionEndpoints()
    {
        var datas = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
        return Ok(datas);
    }
}