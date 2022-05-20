namespace Cardinal.Exceptions;

public class InvalidEventException : AbstractException
{
    public string? payload { get; }
    public InvalidEventException (string message) : base(message) {}
    public InvalidEventException (string message, string payload) : base(message) {
        this.payload = payload;
    }
}