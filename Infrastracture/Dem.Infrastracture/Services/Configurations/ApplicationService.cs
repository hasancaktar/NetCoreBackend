using Dem.Application.Abstraction.Configurations;
using Dem.Application.CustomAttributes;
using Dem.Application.Enums;
using Dem.Application.ModelDtos.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using Action = Dem.Application.ModelDtos.Configuration.Action;

namespace Dem.Infrastracture.Services.Configurations;

public class ApplicationService : IApplicationService
{
    public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
    {
        //burada ApplicationServicesController'da verilen type'a o assembly'yi getiriyor.
        Assembly assembly = Assembly.GetAssembly(type);
        List<Menu> menus = new List<Menu>();
        //burada baseController'dan türeyen ne varsa onları getiriyor
        var contorllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));
        if (contorllers.Any())
        {
            foreach (var controller in contorllers)
            {
                var actions = controller.GetMethods().Where(action => action.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                if (actions.Any())
                {
                    foreach (var action in actions)
                    {
                        var attributes = action.GetCustomAttributes(true);
                        if (attributes.Any())
                        {
                            Menu menu = null;
                            var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                            if (!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                            {
                                menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                menus.Add(menu);
                            }
                            else
                                menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);
                            Action _action = new()
                            {
                                ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitionAttribute.ActionType),
                                Definition = authorizeDefinitionAttribute.Definition,
                            };
                            var httpAttrbute = attributes.FirstOrDefault(method => method.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                            if (attributes.Any())
                                _action.HttpType = httpAttrbute.HttpMethods.First();
                            else
                                _action.HttpType = Microsoft.AspNetCore.Http.HttpMethods.Get;

                            _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";

                            menu.Actions.Add(_action);
                        }
                    }
                }
            }
        }
        return menus;
    }
}