namespace Cardinal.Exceptions;

public class NetworkStreamNotFoundException : AbstractException
{
    public NetworkStreamNotFoundException () : base("The NetworkStream could not be found.") {}
    public NetworkStreamNotFoundException (string message) : base(message) {}
}