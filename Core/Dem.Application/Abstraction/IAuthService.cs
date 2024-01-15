using Dem.Application.Abstraction.Authentications;

namespace Dem.Application.Abstraction;

public interface IAuthService : IInternalAuthencation
{
    Task ResetPasswordAsync(string email);
}