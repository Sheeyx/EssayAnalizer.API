using System.Data;
using EssayAnalizer.API.Models.Users;
using EssayAnalizer.API.Models.Users.Exceptions;
using Microsoft.Win32.SafeHandles;

namespace EssayAnalizer.API.Services.Users;

public partial class UserService
{
    private static void ValidateUserOnAdd(User user)
    {
        ValidationUserNotNull(user);
        Validate(
            (Rule: isInvalid(user.Id), Parameter: nameof(user.Id)),
            (Rule: isInvalid(user.Name), Parameter: nameof(user.Name))
        );
    }
    private static void ValidationUserNotNull(User user)
    {
        if (user is null)
        {
            throw new NullUserException();
        }
    }

    private static dynamic isInvalid(Guid Id) => new
    {
        Condition = Id == Guid.Empty,
        Message = "Id is required"
    };

    private static dynamic isInvalid(string text) => new
    {
        Condition = String.IsNullOrWhiteSpace(text),
        Message = "Text is required"
    };

    private static void Validate(params (dynamic Rule, string Parameter)[] validations)
    {
        var invalidUserException = new InvalidUserException();

        foreach ((dynamic rule, string parameter) in validations)
        {
            if (rule.Condition)
            {
                invalidUserException.UpsertDataList(
                    key: parameter,
                    value: rule.Message
                );
            }
        }
        invalidUserException.ThrowIfContainsErrors();
    }
}