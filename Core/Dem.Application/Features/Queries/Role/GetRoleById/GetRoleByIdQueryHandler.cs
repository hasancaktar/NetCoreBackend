using Dem.Application.Abstraction;
using Dem.Application.ModelDtos.Role;
using MediatR;

namespace Dem.Application.Features.Queries.Role.GetRoleById;

public class GetRoleByIdQueryHandler(IRoleService roleService) : IRequestHandler<GetRoleByIdQueryRequest, GetRoleDto>
{
    public async Task<GetRoleDto> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
    {
        return await roleService.GetRoleById(request.Id);
    }
}