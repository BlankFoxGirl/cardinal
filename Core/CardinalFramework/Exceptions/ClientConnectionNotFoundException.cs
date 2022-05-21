namespace Cardinal.Exceptions;

public class ClientConnectionNotFoundException : AbstractException
{
    public ClientConnectionNotFoundException () : base("The ClientConnection could not be found.") {}
    public ClientConnectionNotFoundException (string message) : base(message) {}
}