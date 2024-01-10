using MediatR;

namespace Dem.Application.Features.Commands.CreateUser;

public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    public string Email { get; set; }
    public string NameSurname { get; set; }
}