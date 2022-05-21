namespace Cardinal.Exceptions;

public class CharacterNotFoundException : AbstractException
{
    public CharacterNotFoundException () : base("The character could not be found.") {}
    public CharacterNotFoundException (string message) : base(message) {}
}