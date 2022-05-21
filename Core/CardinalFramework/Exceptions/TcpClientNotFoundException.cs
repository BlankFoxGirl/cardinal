namespace Cardinal.Exceptions;

public class TcpClientNotFoundException : AbstractException
{
    public TcpClientNotFoundException () : base("The TcpClient could not be found.") {}
    public TcpClientNotFoundException (string message) : base(message) {}
}