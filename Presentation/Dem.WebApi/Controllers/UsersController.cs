using Dem.Application.Features.Commands.CreateUser;
using Dem.Application.Features.Commands.LoginUser;
using Microsoft.AspNetCore.Mvc;

namespace Dem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
    {
        var response = await Mediator.Send(createUserCommandRequest);
        return Ok(response);
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
    {
        var response = await Mediator.Send(loginUserCommandRequest);
        return Ok(response);
    }
}

