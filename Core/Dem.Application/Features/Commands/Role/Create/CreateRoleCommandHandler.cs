using Dem.Application.Abstraction;
using Dem.Application.Exceptions;
using MediatR;

namespace Dem.Application.Features.Commands.Role.Create;

public class CreateRoleCommandHandler(IRoleService roleService) : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
{
    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await roleService.CreateRole(request.RoleName);
        if (result)
            return new() { IsSuccess = result, Message = "Rol eklendi" };
        throw new ArgumentException("Rol ekleme başarısız!");
    }
}