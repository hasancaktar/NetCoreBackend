using Dem.Application.Exceptions;
using Dem.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Dem.Application.Features.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    readonly UserManager<User> _userManager;
    public CreateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _userManager.CreateAsync(new()
        {
            Id= Guid.NewGuid().ToString(),
            UserName = request.UserName,
            Email = request.Email,
            NameSurname = request.NameSurname,

        }, request.Password);

        CreateUserCommandResponse response = new() { IsSuccess = result.Succeeded };

        if (result.Succeeded)
            response.Message = "Kullanıcı başarıyla eklendi";

        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description} | ";
        return response;
    }
}

