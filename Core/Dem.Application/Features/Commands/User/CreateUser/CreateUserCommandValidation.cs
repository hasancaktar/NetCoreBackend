using FluentValidation;

namespace Dem.Application.Features.Commands.User.CreateUser;

public class CreateUserCommandValidation : AbstractValidator<CreateUserCommandRequest>
{
    public CreateUserCommandValidation()
    {
        RuleFor(user => user.UserName).NotEmpty().MaximumLength(15);
    }
}