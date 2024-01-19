using Dem.Application.ModelDtos.Configuration;

namespace Dem.Application.Abstraction.Configurations;

public interface IApplicationService
{
    List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
}