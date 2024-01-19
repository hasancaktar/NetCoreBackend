using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Dem.Application.Features.Commands.Role.Create;
using Dem.Application.Features.Commands.Role.Delete;
using Dem.Application.Features.Commands.Role.Update;
using Dem.Application.Features.Queries.Role.GetRoleById;
using Dem.Application.Features.Queries.Role.GetRoles;
using Microsoft.AspNetCore.Mvc;

namespace Dem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : BaseController
{
    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateAsync(CreateRoleCommandRequest createRoleCommandRequest)
    {
        var response = await Mediator.Send(createRoleCommandRequest);
        return Ok(response);
    }

    [HttpPut("UpdateRole")]
    public async Task<IActionResult> UpdateAsync(UpdateRoleCommandRequest updateRoleCommandRequest)
    {
        var response = await Mediator.Send(updateRoleCommandRequest);
        return Ok(response);
    }

    [HttpDelete("DeleteRole")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var response = await Mediator.Send(new DeleteRoleCommandRequest { Id = id });
        return Ok(response);
    }

    [HttpGet("GetAllRoles")]
    [Benchmark]
    [MemoryDiagnoser]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await Mediator.Send(new GetRolesQueryRequest());
        return Ok(response);
    }

    [HttpGet("GetRoleById")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var response = await Mediator.Send(new GetRoleByIdQueryRequest { Id = id });
        return Ok(response);
    }
}