using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dem.Application.Features.Commands.CreateUser;

public class CreateUserCommandResponse
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; } = string.Empty;
}
