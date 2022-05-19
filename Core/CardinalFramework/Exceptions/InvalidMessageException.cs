namespace Cardinal.Exceptions;

class InvalidMessageException : AbstractException
{
    public InvalidMessageException (string message) : base(message) {}
}