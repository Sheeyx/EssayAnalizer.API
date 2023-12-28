using EssayAnalizer.API.Models.Users;
using EssayAnalizer.API.Models.Users.Exceptions;

namespace EssayAnalizer.API.Services.Users;

public partial class UserService
{
    private delegate ValueTask<User> ReturningUserFunction();

    private async ValueTask<User> TryCatch(ReturningUserFunction returningUserFunction)
    {
        try
        {
            return await returningUserFunction();
        }
        catch (NullUserException nullUserException)
        {
            var userValidationException =
                new UserValidationException(nullUserException);
            
            this.loggingBroker.LogError(userValidationException);
            throw userValidationException;
        }
    }
}