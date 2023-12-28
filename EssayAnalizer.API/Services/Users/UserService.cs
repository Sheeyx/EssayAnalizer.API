using EssayAnalizer.API.Brokers.Loggings;
using EssayAnalizer.API.Brokers.Storages;
using EssayAnalizer.API.Models.Users;
using EssayAnalizer.API.Models.Users.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EssayAnalizer.API.Services.Users;

public partial class UserService : IUserService
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

    public ValueTask<User> AddUserAsync(User user) =>
        TryCatch(async () =>
        {
            ValidateUserOnAdd(user);
            return await this.storageBroker.InsertUserAsync(user);

        });

    public IQueryable<User> GetAllUsers()
    {
        return this.storageBroker.SelectAllUsers();
    }
    
    public async ValueTask<User> ModifyUserAsync(User user)
    {
        ValidateUserOnModify(user);

        User maybeUser =
            await this.storageBroker.SelectUserByIdAsync(user.Id);

        ValidateAgainstStorageUserOnModify(user, maybeUser);

        return await this.storageBroker.UpdateUserAsync(user);
    }


}