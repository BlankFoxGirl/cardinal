namespace Cardinal.Exceptions;

public class InvalidConnectionAttempt : AbstractException
{
    public InvalidConnectionAttempt () : base("An inbound connection was attempted but has failed.") {}
    public InvalidConnectionAttempt (string message) : base(message) {}
}