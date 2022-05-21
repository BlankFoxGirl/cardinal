namespace Cardinal.Exceptions;

public class InvalidClientConnectionException : AbstractException
{
    public InvalidClientConnectionException (string message) : base(message) {}
    public InvalidClientConnectionException () : base("ClientConnection is missing one or more values and is thus invalid.") {}
}