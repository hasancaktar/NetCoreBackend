using Dem.Application.Abstraction;
using Dem.Application.ModelDtos.Role;
using Dem.Domain.Entities.Identity;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dem.Persistance.Services;

public class RoleService(RoleManager<Role> _roleManager, IMapper _mapper) : IRoleService
{
    public async Task<bool> CreateRole(string roleName)
    {
        var result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = roleName });

        return result.Succeeded;
    }

    public async Task<bool> DeleteRole(string id)
    {
        IdentityResult result = await _roleManager.DeleteAsync(new() { Id = id });
        return result.Succeeded;
    }

    public async Task<bool> UpdateRole(string id, string roleName)
    {
        IdentityResult result = await _roleManager.UpdateAsync(new() { Id = id, Name = roleName });
        return result.Succeeded;
    }

    public async Task<List<GetRoleDto>> GetAllRoles()
    {
        List<GetRoleDto> roleList = _mapper.Map<List<GetRoleDto>>(await _roleManager.Roles.ToListAsync());
        //List<GetRoleDto> roleList = _mapper.Map<List<GetRoleDto>>(_roleManager.Roles.AsQueryable());
        return roleList;
    }

    public async Task<GetRoleDto> GetRoleById(string id)
    {
        GetRoleDto role = _mapper.Map<GetRoleDto>(await _roleManager.Roles.FirstOrDefaultAsync(role => role.Id == id));
        return role;
    }
}