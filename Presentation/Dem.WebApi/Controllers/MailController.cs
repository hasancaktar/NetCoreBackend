using Dem.Application.Features.Commands.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailController : BaseController
{
    [HttpPost("SendMail")]
    public async Task<IActionResult> SendMail(MailCommandRequest mailCommandRequest)
    {
        var response = await Mediator.Send(mailCommandRequest);
        return Ok(response);
    }
}