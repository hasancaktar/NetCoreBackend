using Dem.Application.Abstraction;
using MediatR;

namespace Dem.Application.Features.Commands.Mail;

public class MailCommandHandler : IRequestHandler<MailCommandRequest, MailCommandResponse>
{
    IMailService _mailService;

    public MailCommandHandler(IMailService mailService)
    {
        _mailService = mailService;
    }

    public async Task<MailCommandResponse> Handle(MailCommandRequest request, CancellationToken cancellationToken)
    {
        await _mailService.SendMessageAsync(request.To, request.Subject, request.Body, request.IsBodyHtml);
        return new() { IsSuccess = true, Message = "Mail gönderildi" };
    }
}
