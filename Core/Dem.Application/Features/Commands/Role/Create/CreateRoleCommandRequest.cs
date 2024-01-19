using MediatR;

namespace Dem.Application.Features.Commands.Role.Create;

public class CreateRoleCommandRequest : IRequest<CreateRoleCommandResponse>
{
    public string RoleName { get; set; }
}