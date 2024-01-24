using FluentValidation;

namespace Dem.Application.Features.Commands.Product.Create;

public class CreateProductCommandValidation : AbstractValidator<CreateProductCommandRequest>
{
    public CreateProductCommandValidation()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}