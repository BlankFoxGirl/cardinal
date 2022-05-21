namespace Cardinal.Entities;
using System.Net.Sockets;
using Cardinal.Exceptions;
public class ClientConnection : AbstractEntity
{
    private TcpClient? client;
    private NetworkStream? stream;
    private AbstractEntity? account;
    private AbstractEntity? character;

    public void setClient (TcpClient client)
    {
        this.client = client;
    }
    public void setStream (NetworkStream stream)
    {
        this.stream = stream;
    }
    public void setCharacter (object character)
    {
        this.character = character as AbstractEntity;
    }
    public void setAccount (object account)
    {
        this.account = account as AbstractEntity;
    }
    /// <summary>
    /// Obtains the account object associated with this ClientConnection
    /// or throws AccountNotFoundException.
    /// </summary>
    public AbstractEntity getAccount()
    {
        if (this.account == null)
        {
            throw new AccountNotFoundException();
        }

        return this.account;
    }

    /// <summary>
    /// Obtains the character object associated with this ClientConnection
    /// or throws CharacterNotFoundException.
    /// </summary>
    public AbstractEntity getCharacter()
    {
        if (this.character == null)
        {
            throw new CharacterNotFoundException();
        }

        return this.character;
    }

    /// <summary>
    /// Obtains the NetworkStream object associated with this ClientConnection
    /// or throws NetworkStreamNotFoundException.
    /// </summary>
    public NetworkStream getStream()
    {
        if (this.stream == null)
        {
            throw new NetworkStreamNotFoundException();
        }

        return this.stream;
    }

    /// <summary>
    /// Obtains the TcpClient object associated with this ClientConnection
    /// or throws TcpClientNotFoundException.
    /// </summary>
    public TcpClient getClient()
    {
        if (this.client == null)
        {
            throw new TcpClientNotFoundException();
        }

        return this.client;
    }

    /// <summary>
    /// Validates the ClientConnection if either NetworkStream or TcpClient not found
    /// then throws InvalidClientConnectionException().
    /// </summary>
    public void Validate()
    {
        if (this.client == null)
        {
            throw new InvalidClientConnectionException();
        }

        if (this.stream == null)
        {
            throw new InvalidClientConnectionException();
        }
    }

    /// <summary>
    /// Validates the ClientConnection and returns the results as Boolean.
    /// </summary>
    /// <returns>false if invalid, true if valid.</returns>
    public Boolean IsValid()
    {

        if (this.client == null)
        {
            return false;
        }

        if (this.stream == null)
        {
            return false;
        }

        return true;
    }
}