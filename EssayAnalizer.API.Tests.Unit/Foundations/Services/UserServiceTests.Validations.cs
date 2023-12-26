using System.Net.Sockets;
using EssayAnalizer.API.Models.Users;
using EssayAnalizer.API.Models.Users.Exceptions;
using FluentAssertions;
using Moq;

namespace EssayAnalizer.API.Tests.Unit.Foundations.Services;

public partial class UserServiceTests
{
    [Fact]
    public async Task ShouldThrowValidationExceptionOnAddIfUserIsNullAndLogItAsync()
    {
        //given
        User nullUser = null;
        var nullUserException = new NullUserException();
        
        var expectedUserValidationException
            = new UserValidationException(nullUserException);
        
        //when
        ValueTask<User> addUserTask =
            this.userService.AddUserAsync(nullUser);

        UserValidationException actualUserValidationException =
            await Assert.ThrowsAsync<UserValidationException>(addUserTask.AsTask);
        
        //then
        actualUserValidationException.Should().BeEquivalentTo(
            expectedUserValidationException);
        
        this.loggingBrokerMock.Verify(broker => 
            broker.LogError(It.Is(SameExceptionAs(expectedUserValidationException))), Times.Once);
        
        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("   ")]

    public async Task ShouldThrowValidationExceptionOnAddIfUserIsInvalidAndLogItAsync(string invalidText)
    {
        //given
        var invalidUser = new User
        {
            Name = invalidText
        };

        var invalidUserException = new InvalidUserException();
        
        invalidUserException.AddData(
            key:nameof(User.Id),
            values: "Id is required");
        
        invalidUserException.AddData(
            key:nameof(User.Name),
            values: "Text is required");
        
        var expectedUserValidationException = 
            new UserValidationException(invalidUserException);
        
        //when

        ValueTask<User> addUserTask =
            this.userService.AddUserAsync(invalidUser);

        UserValidationException actualUserValidationException =
            await Assert.ThrowsAsync<UserValidationException>(addUserTask.AsTask);
        
        //then
        actualUserValidationException.Should().BeEquivalentTo(
            expectedUserValidationException); 
        
        this.loggingBrokerMock.Verify(broker=>
            broker.LogError(It.Is(SameExceptionAs(expectedUserValidationException))),Times.Once);
        
        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }
}