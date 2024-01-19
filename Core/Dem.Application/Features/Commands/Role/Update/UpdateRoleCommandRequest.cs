using MediatR;

namespace Dem.Application.Features.Commands.Role.Update;

public class UpdateRoleCommandRequest : IRequest<UpdateRoleCommandResponse>
{
    public string Id { get; set; }
    public string RoleName { get; set; }
}