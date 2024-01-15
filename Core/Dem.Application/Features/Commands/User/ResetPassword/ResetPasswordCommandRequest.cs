using MediatR;

namespace Dem.Application.Features.Commands.User.ResetPassword;

public class ResetPasswordCommandRequest : IRequest<ResetPasswordCommandResponse>
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
}