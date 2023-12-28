using EssayAnalizer.API.Models.Users;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EssayAnalizer.API.Tests.Unit.Foundations.Services;

public partial class UserServiceTests
{
    [Fact]
    public async Task ShouldModifyUserAsync()
    {
        //given
        User randomUser = CreateRandomUser();
        User inputUser = randomUser;
        User persistedUser = inputUser;
        User updatedUser = inputUser;
        User expectedUser = updatedUser.DeepClone();
        Guid InpudUserId = inputUser.Id;

        this.storageBrokerMock.Setup(broker =>
                broker.SelectUserByIdAsync(InpudUserId))
            .ReturnsAsync(persistedUser);

        this.storageBrokerMock.Setup(broker =>
                broker.UpdateUserAsync(inputUser))
            .ReturnsAsync(updatedUser);

        //when
        User actualUser =
            await this.userService.ModifyUserAsync(inputUser);

        //then
        actualUser.Should().BeEquivalentTo(expectedUser);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectUserByIdAsync(InpudUserId), Times.Once());

        this.storageBrokerMock.Verify(broker =>
            broker.UpdateUserAsync(inputUser), Times.Once());

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}