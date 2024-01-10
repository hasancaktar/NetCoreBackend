﻿using Dem.Application.Abstraction.Token;
using Dem.Application.Exceptions;
using Dem.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Dem.Application.Features.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    readonly ITokenHandler _tokenHandler;

    public LoginUserCommandHandler(ITokenHandler tokenHandler, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
    {

        _tokenHandler = tokenHandler;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

        if (user == null)
            throw new ExceptionHandler("Kullanıcı adı veya şifre hatalı");

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
        {
            ModelDtos.Token token = _tokenHandler.CreateAccessToken();
            return new LoginSuccessCommandResponse { Token = token };
        }
        //return new LoginErrorCommandResponse { Message = "Kullanıcı adı veya şifre hatalı" };
        throw new ExceptionHandler("Kimlik doğrulama başarısız");
    }
}
