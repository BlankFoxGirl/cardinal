namespace Cardinal.Exceptions;

public class EntityPropertyNotFoundException : AbstractException
{
    public EntityPropertyNotFoundException () : base("The entity property could not be found.") {}
    public EntityPropertyNotFoundException (string message) : base(message) {}
}