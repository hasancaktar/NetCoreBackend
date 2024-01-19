using Dem.Application.ModelDtos.Role;
using MediatR;

namespace Dem.Application.Features.Queries.Role.GetRoles;

public class GetRolesQueryRequest : IRequest<List<GetRoleDto>>
{
}