using Dem.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Dem.Application.Features.Commands.User.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, ResetPasswordCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.User> _userManager;

    public ResetPasswordCommandHandler(UserManager<Domain.Entities.Identity.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Identity.User user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new ArgumentException("Kullanıcı bulunamadı");
        }
        string token = await _userManager.GeneratePasswordResetTokenAsync(user);

        IdentityResult result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

        if (result.Succeeded)
        {
            return new()
            {
                IsSuccess = result.Succeeded,
                Message = "Şifre değiştirildi"
            };
        }
        throw new ArgumentException("Şifre değiştirme başarısız");
    }
}