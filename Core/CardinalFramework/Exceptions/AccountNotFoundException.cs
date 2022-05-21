namespace Cardinal.Exceptions;

public class AccountNotFoundException : AbstractException
{
    public AccountNotFoundException () : base("The account could not be found.") {}
    public AccountNotFoundException (string message) : base(message) {}
}