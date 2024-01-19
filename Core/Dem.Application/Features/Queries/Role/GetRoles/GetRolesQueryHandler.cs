using Dem.Application.Abstraction;
using Dem.Application.ModelDtos.Role;
using MediatR;

namespace Dem.Application.Features.Queries.Role.GetRoles;

public class GetRolesQueryHandler(IRoleService roleService) : IRequestHandler<GetRolesQueryRequest, List<GetRoleDto>>
{
    public async Task<List<GetRoleDto>> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
    {
        return await roleService.GetAllRoles();
    }
}