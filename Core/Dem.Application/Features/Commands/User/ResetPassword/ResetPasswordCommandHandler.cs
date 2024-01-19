using Dem.Application.Abstraction;
using Dem.Application.Helpers;
using Dem.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Dem.Application.Features.Commands.User.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, ResetPasswordCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.User> _userManager;
    private IMailService _mailService;

    public ResetPasswordCommandHandler(UserManager<Domain.Entities.Identity.User> userManager, IMailService mailService)
    {
        _userManager = userManager;
        _mailService = mailService;
    }

    public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Identity.User user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new ArgumentException("Kullanıcı bulunamadı");
        }
        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        //bu yapı mail gönderirken oluşturulacak url'de olmaması gereken "(*,.)" değerleri kaldırmak için.
        //resetToken = resetToken.UrlEncode();
        //Mail göndererek şifre yenileme yapısı
        //var url = await _mailService.SendPasswordResetMailAsync(request.Email, user.Id, resetToken);



        //if (url != null)
        //{
        //    return new()
        //    {
        //        IsSuccess = true,
        //        Message = url
        //    };
        //}
        IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);
        if (result.Succeeded)
        {
            return new() { 
            IsSuccess = true,
            Message="Şifre değiştirilidi"
            };
        }
        throw new ArgumentException("Şifre değiştirme başarısız");
    }
}