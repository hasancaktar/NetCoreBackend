using Dem.Application.ModelDtos.Role;

namespace Dem.Application.Abstraction;

public interface IRoleService
{
    Task<List<GetRoleDto>> GetAllRoles();

    Task<GetRoleDto> GetRoleById(string id);

    Task<bool> CreateRole(string roleName);

    Task<bool> DeleteRole(string id);

    Task<bool> UpdateRole(string id, string roleName);
}