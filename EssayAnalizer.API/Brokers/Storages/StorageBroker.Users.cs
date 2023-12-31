using EssayAnalizer.API.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace EssayAnalizer.API.Brokers.Storages;

public partial class StorageBroker
{
    public DbSet<User> Users { get; set; }
    
    public async ValueTask<User> InsertUserAsync(User user) =>
        await InsertAsync(user);

    public IQueryable<User> SelectAllUsers() =>
        SelectAll<User>().AsQueryable();

    public async ValueTask<User> SelectUserByIdAsync(Guid userId) =>
        await SelectAsync<User>(userId);

    public async ValueTask<User> UpdateUserAsync(User user) =>
        await UpdateAsync(user);

    public async ValueTask<User> DeleteUserAsync(User user) =>
        await DeleteAsync(user);
}