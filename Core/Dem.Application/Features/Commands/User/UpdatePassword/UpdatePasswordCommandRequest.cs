﻿using MediatR;

namespace Dem.Application.Features.Commands.User.UpdatePassword;

public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse>
{
    public string UserId { get; set; }
    public string ResetToken { get; set; }
    public string NewPassword { get; set; }
}