using Dem.Application.Features.Commands.User.CreateUser;
using Dem.Domain.Entities.Identity;

namespace Dem.Application.Abstraction;

public interface IUserService
{
    Task<CreateUserCommandResponse> CreateAsync(CreateUserCommandRequest model);

    Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime accessTokenDate, int addOnAccessTokenDate);

    Task UpdatePassword(string userId, string resetToken, string newPassword);
}