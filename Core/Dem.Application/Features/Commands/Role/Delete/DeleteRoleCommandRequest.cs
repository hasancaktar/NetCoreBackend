using MediatR;

namespace Dem.Application.Features.Commands.Role.Delete;

public class DeleteRoleCommandRequest : IRequest<DeleteRoleCommandResponse>
{
    public string Id { get; set; }
}