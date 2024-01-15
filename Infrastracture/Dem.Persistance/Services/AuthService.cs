using Dem.Application.Abstraction;
using Dem.Application.ModelDtos;

namespace Dem.Persistance.Services;

public class AuthService : IAuthService
{
    public Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
    {
        throw new NotImplementedException();
    }

    public Task<Token> RefreshTokenLoginAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task ResetPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }
}