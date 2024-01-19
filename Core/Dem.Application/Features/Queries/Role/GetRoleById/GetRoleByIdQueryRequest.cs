using Dem.Application.ModelDtos.Role;
using MediatR;

namespace Dem.Application.Features.Queries.Role.GetRoleById;

public class GetRoleByIdQueryRequest : IRequest<GetRoleDto>
{
    public string Id { get; set; }
}