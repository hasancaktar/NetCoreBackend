using Dem.Application.ModelDtos;

namespace Dem.Application.Features.Commands.User.LoginUser;

public class LoginUserCommandResponse
{

}

public class LoginSuccessCommandResponse : LoginUserCommandResponse
{
    public Token Token { get; set; }
}
public class LoginErrorCommandResponse : LoginUserCommandResponse
{
    public string Message { get; set; }
}
