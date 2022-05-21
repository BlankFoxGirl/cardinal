namespace Cardinal.Exceptions;

public class InvalidLogLevelException : AbstractException
{
    public InvalidLogLevelException () : base("The Log Level could not be found.") {}
    public InvalidLogLevelException (string message) : base(message) {}
}