namespace Dem.Application.Abstraction.Authentications;

public interface IInternalAuthencation
{
    Task<ModelDtos.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);

    Task<ModelDtos.Token> RefreshTokenLoginAsync(string refreshToken);
}