namespace Cardinal.Exceptions;

public class TcpListenerNotFoundException : AbstractException
{
    public TcpListenerNotFoundException () : base("The TcpListener could not be found, has the server started?") {}
    public TcpListenerNotFoundException (string message) : base(message) {}
}