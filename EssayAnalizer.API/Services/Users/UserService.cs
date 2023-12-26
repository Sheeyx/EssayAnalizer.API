using EssayAnalizer.API.Brokers.Loggings;
using EssayAnalizer.API.Brokers.Storages;
using EssayAnalizer.API.Models.Users;
using EssayAnalizer.API.Models.Users.Exceptions;

namespace EssayAnalizer.API.Services.Users;

public class UserService : IUserService
{
    private readonly IStorageBroker storageBroker;
    private readonly ILoggingBroker loggingBroker;

    public UserService(
        IStorageBroker storageBroker,
        ILoggingBroker loggingBroker)
    {
        this.storageBroker = storageBroker;
        this.loggingBroker = loggingBroker;
    }

    public async ValueTask<User> AddUserAsync(User user)
    {
        try
        {
            if (user is null)
            {
                throw new NullUserException();
            }
            return await this.storageBroker.InsertUserAsync(user);
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