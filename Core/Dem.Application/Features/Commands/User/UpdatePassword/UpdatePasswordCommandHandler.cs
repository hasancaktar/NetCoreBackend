using Dem.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Dem.Application.Features.Commands.User.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.User> _userManager;

    public UpdatePasswordCommandHandler(UserManager<Domain.Entities.Identity.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Identity.User user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new ArgumentException("Kullanıcı bulunamadı");
        }

        request.ResetToken = request.ResetToken.UrlDecode();
        IdentityResult result = await _userManager.ResetPasswordAsync(user, request.ResetToken, request.NewPassword);
        if (result.Succeeded)
        {
            await _userManager.UpdateSecurityStampAsync(user);
            return new()
            {
                IsSuccess = result.Succeeded,
                Message = "Şifre güncellendi"
            };
        }
        throw new ArgumentException("Şifre güncelleme başarısız");
    }
}