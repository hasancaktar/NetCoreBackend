using Dem.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Dem.Application.Features.Commands.User.VerifyResetToken;

public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenCommandRequest, VerifyResetTokenCommandResponse>
{
    private UserManager<Domain.Entities.Identity.User> _userManager;

    public VerifyResetTokenCommandHandler(UserManager<Domain.Entities.Identity.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<VerifyResetTokenCommandResponse> Handle(VerifyResetTokenCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Identity.User user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new ArgumentException("Kullanıcı bulunamadı");
        }

        //byte[] tokenBytes = WebEncoders.Base64UrlDecode(request.ResetToken);
        //request.ResetToken = Encoding.UTF8.GetString(tokenBytes);

        request.ResetToken = request.ResetToken.UrlDecode();

        bool state = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", request.ResetToken);

        return new()
        {
            IsSuccess = state,
            Message = "Token Doğrulandı"
        };
    }
}