using EssayAnalizer.API.Models.Users;

namespace EssayAnalizer.API.Services.Users;

public interface IUserService
{
    ValueTask<User> AddUserAsync(User user);
    IQueryable<User> GetAllUsers();
    ValueTask<User> ModifyUserAsync(User user);

}