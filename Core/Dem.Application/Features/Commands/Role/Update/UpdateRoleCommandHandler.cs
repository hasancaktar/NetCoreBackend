using Dem.Application.Abstraction;
using Dem.Application.Exceptions;
using MediatR;

namespace Dem.Application.Features.Commands.Role.Update;

public class UpdateRoleCommandHandler(IRoleService roleService) : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
{
    public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await roleService.UpdateRole(request.Id, request.RoleName);
        if (result)
            return new() { IsSuccess = result, Message = "Rol güncellendi" };
        throw new ExceptionHandler("Rol güncelleme başarısız!");
    }
}