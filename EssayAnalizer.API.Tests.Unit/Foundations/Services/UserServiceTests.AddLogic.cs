using EssayAnalizer.API.Models.Users;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EssayAnalizer.API.Tests.Unit.Foundations.Services;

public partial class UserServiceTests
{
    [Fact]
    public async Task ShouldAddUserAsync()
    {
        //given
        User randomUser = CreateRandomUser();
        User inputUser = randomUser;
        User persistedUser = inputUser;
        User expectedUser = persistedUser.DeepClone();

        this.storageBrokerMock.Setup(broker =>
                broker.InsertUserAsync(inputUser))
                    .ReturnsAsync(persistedUser);
        
        //when

        User actualUser = 
            await this.userService.AddUserAsync(inputUser);
        
        //then
        actualUser.Should().BeEquivalentTo(expectedUser);
        this.storageBrokerMock.Verify(broker=>
            broker.InsertUserAsync(inputUser), Times.Once);
        
        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}