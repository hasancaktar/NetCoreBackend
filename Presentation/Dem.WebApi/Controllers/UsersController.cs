using Dem.Application.Features.Commands.User.CreateUser;
using Dem.Application.Features.Commands.User.LoginUser;
using Dem.Application.Features.Commands.User.ResetPassword;
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

    [HttpPost("ResetPassowrd")]
    public async Task<IActionResult> ResetPassowrd(ResetPasswordCommandRequest resetPasswordCommandRequest)
    {
        var response = await Mediator.Send(resetPasswordCommandRequest);
        return Ok(response);
    }
}