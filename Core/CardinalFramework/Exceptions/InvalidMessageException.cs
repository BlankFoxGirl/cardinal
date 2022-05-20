namespace Cardinal.Exceptions;

public class InvalidMessageException : AbstractException
{
    public InvalidMessageException (string message) : base(message) {}
}