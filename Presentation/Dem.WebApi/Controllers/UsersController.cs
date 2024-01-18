using Dem.Application.Features.Commands.User.CreateUser;
using Dem.Application.Features.Commands.User.LoginUser;
using Dem.Application.Features.Commands.User.ResetPassword;
using Dem.Application.Features.Commands.User.UpdatePassword;
using Dem.Application.Features.Commands.User.VerifyResetToken;
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

    [HttpPost("ResetPassowrdWithMail")]
    public async Task<IActionResult> ResetPassowrdWithMail(ResetPasswordCommandRequest resetPasswordCommandRequest)
    {
        var response = await Mediator.Send(resetPasswordCommandRequest);
        return Ok(response);
    }

    [HttpPost("VerifyResetToken")]
    public async Task<IActionResult> VerifyResetToken(VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
    {
        var response = await Mediator.Send(verifyResetTokenCommandRequest);
        return Ok(response);
    }

    [HttpPost("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordCommandRequest updatePasswordCommandRequest)
    {
        var response = await Mediator.Send(updatePasswordCommandRequest);
        return Ok(response);
    }
}