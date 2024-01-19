using Dem.Application.Abstraction;
using Dem.Application.Exceptions;
using MediatR;

namespace Dem.Application.Features.Commands.Role.Delete;

public class DeleteRoleCommandHandler(IRoleService roleService) : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
{
    public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await roleService.DeleteRole(request.Id);
        if (result)
            return new() { IsSuccess = result, Message = "Rol silindi" };
        throw new ExceptionHandler("Rol silme başarısız!");
    }
}