using EssayAnalizer.API.Models.Users;
using EssayAnalizer.API.Models.Users.Exceptions;

namespace EssayAnalizer.API.Services.Users;

public partial class UserService
{
    private static void ValidateUserNotNull(User user)
    {
        if (user is null)
        {
            throw new NullUserException();
        }
    }
}