using EssayAnalizer.API.Brokers.Loggings;
using EssayAnalizer.API.Brokers.Storages;
using EssayAnalizer.API.Models.Users;
using EssayAnalizer.API.Services.Users;
using Moq;
using Tynamix.ObjectFiller;

namespace EssayAnalizer.API.Tests.Unit.Foundations.Services;

public partial class UserServiceTests
{
    private readonly Mock<IStorageBroker> storageBrokerMock;
    private readonly Mock<ILoggingBroker> loggingBrokerMock;
    private readonly IUserService userService;

    public UserServiceTests()
    {
        this.storageBrokerMock = new Mock<IStorageBroker>();
        this.loggingBrokerMock = new Mock<ILoggingBroker>();

        this.userService = new UserService(
            storageBroker: this.storageBrokerMock.Object,
            loggingBroker: this.loggingBrokerMock.Object);
    }
    
    private static User CreateRandomUser()=>
        CreateUserFiller().Create();
    

    private static Filler<User> CreateUserFiller() =>
        new Filler<User>();

}