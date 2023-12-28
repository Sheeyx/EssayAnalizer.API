using Xeptions;

namespace EssayAnalizer.API.Models.Users.Exceptions;

public class InvalidUserException : Xeption
{
    public InvalidUserException()
    : base(message:"User is invalid.")
    { }
}