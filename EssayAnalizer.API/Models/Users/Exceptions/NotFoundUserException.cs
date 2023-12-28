using Xeptions;

namespace EssayAnalizer.API.Models.Users.Exceptions;

public class NotFoundUserException : Xeption
{
    public NotFoundUserException(Guid userId)
        : base(message: $"Couldn't found user with id: {userId}")
    { }
}