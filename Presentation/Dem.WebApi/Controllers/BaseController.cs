using Dem.WebApi.Languages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Dem.WebApi.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

    private IStringLocalizer<Language>? _localization;
    protected IStringLocalizer<Language> Localize => _localization ??= HttpContext.RequestServices.GetService<IStringLocalizer<Language>>();
}