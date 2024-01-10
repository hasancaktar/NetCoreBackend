using MediatR;

namespace Dem.Application.Features.Commands.LoginUser;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}