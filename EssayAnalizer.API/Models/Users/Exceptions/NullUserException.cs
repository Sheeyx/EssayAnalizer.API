namespace EssayAnalizer.API.Models.Users.Exceptions;

using Xeptions;

public class NullUserException : Xeption
{
    public NullUserException()
        : base(message: "User is null")
    { }
}
