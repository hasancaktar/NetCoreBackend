using MediatR;

namespace Dem.Application.Features.Commands.Mail;

public class MailCommandRequest:IRequest<MailCommandResponse>
{
    public string To { get; set; }
    public string  Subject { get; set; }
    public string  Body { get; set; }
    public bool IsBodyHtml { get; set; }
}
