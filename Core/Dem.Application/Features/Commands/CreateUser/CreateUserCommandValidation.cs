using FluentValidation;

namespace Dem.Application.Features.Commands.CreateUser;

public class CreateUserCommandValidation : AbstractValidator<CreateUserCommandRequest>
{
    public CreateUserCommandValidation()
    {
        RuleFor(user => user.UserName).NotEmpty().MaximumLength(15);
    }
}