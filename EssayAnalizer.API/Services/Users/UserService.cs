using EssayAnalizer.API.Brokers.Loggings;
using EssayAnalizer.API.Brokers.Storages;
using EssayAnalizer.API.Models.Users;

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
        return await this.storageBroker.InsertUserAsync(user);
    }
}