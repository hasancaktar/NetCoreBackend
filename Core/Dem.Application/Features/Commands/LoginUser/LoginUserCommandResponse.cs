using Dem.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dem.Application.Features.Commands.LoginUser;

public class LoginUserCommandResponse
{
   
}

public class LoginSuccessCommandResponse :LoginUserCommandResponse
{
    public Token Token { get; set; }
}
public class LoginErrorCommandResponse : LoginUserCommandResponse
{
    public string Message { get; set; }
}
