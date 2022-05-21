namespace Cardinal.Exceptions;

public class EntityPropertyNotWritableException : AbstractException
{
    public EntityPropertyNotWritableException () : base("The entity property is not writable.") {}
    public EntityPropertyNotWritableException (string message) : base(message) {}
}