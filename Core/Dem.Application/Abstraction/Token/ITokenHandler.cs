namespace Dem.Application.Abstraction.Token;

public interface ITokenHandler
{
    ModelDtos.Token CreateAccessToken();
    string CreateRefreshToken();
}