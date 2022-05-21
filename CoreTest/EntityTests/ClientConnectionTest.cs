namespace CoreTest.Entity;
using Cardinal.Entities;

public class ClientConnectionTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ClientConnection_Generates_UUID()
    {
        ClientConnection ae = new ClientConnection();
        Assert.True(ae.uuid != null);
    }

    [Test]
    public void ClientConnection_Empty_Cannot_Validate()
    {
        ClientConnection ae = new ClientConnection();
        Assert.That(() => {
            ae.Validate();
        }, Throws.InstanceOf<Cardinal.Exceptions.InvalidClientConnectionException>());
    }

    [Test]
    public void ClientConnection_Empty_Cannot_Validate_No_Stream()
    {
        ClientConnection ae = new ClientConnection();
        System.Net.Sockets.TcpClient t = new System.Net.Sockets.TcpClient();
        ae.setClient(t);
        Assert.That(() => {
            ae.Validate();
        }, Throws.InstanceOf<Cardinal.Exceptions.InvalidClientConnectionException>());
    }

    [Test]
    public void ClientConnection_Empty_Is_Not_Valid()
    {
        ClientConnection ae = new ClientConnection();
        Assert.That(ae.IsValid(), Is.False);
    }

    [Test]
    public void ClientConnection_Empty_Is_Not_Valid_No_Stream()
    {
        ClientConnection ae = new ClientConnection();
        System.Net.Sockets.TcpClient t = new System.Net.Sockets.TcpClient();
        ae.setClient(t);
        Assert.That(ae.IsValid(), Is.False);
    }

    [Test]
    public void ClientConnection_No_Account()
    {
        ClientConnection ae = new ClientConnection();
        Assert.That(() => {
            ae.getAccount();
        }, Throws.InstanceOf<Cardinal.Exceptions.AccountNotFoundException>());
    }

    [Test]
    public void ClientConnection_No_Character()
    {
        ClientConnection ae = new ClientConnection();
        Assert.That(() => {
            ae.getCharacter();
        }, Throws.InstanceOf<Cardinal.Exceptions.CharacterNotFoundException>());
    }

    [Test]
    public void ClientConnection_Can_Set_Character()
    {
        ClientConnection ae = new ClientConnection();
        Assert.That(() => {
            ae.setCharacter(new Object());
        }, Throws.Nothing);
    }

    [Test]
    public void ClientConnection_Can_Get_Character()
    {
        ClientConnection cc = new ClientConnection();
        AbstractEntity ae = new AbstractEntity();
        cc.setCharacter(ae);
        Assert.That(() => {
            cc.getCharacter();
        }, Throws.Nothing);
    }

    [Test]
    public void ClientConnection_Can_Set_Account()
    {
        ClientConnection ae = new ClientConnection();
        Assert.That(() => {
            ae.setAccount(new Object());
        }, Throws.Nothing);
    }

    [Test]
    public void ClientConnection_Can_Get_Account()
    {
        ClientConnection cc = new ClientConnection();
        AbstractEntity ae = new AbstractEntity();
        cc.setAccount(ae);
        Assert.That(() => {
            cc.getAccount();
        }, Throws.Nothing);
    }

    [Test]
    public void ClientConnection_Can_Set_Stream()
    {
        ClientConnection ae = new ClientConnection();
        Assert.That(() => {
            ae.setStream(new Object() as System.Net.Sockets.NetworkStream);
        }, Throws.Nothing);
    }

    [Ignore("Cannot set a valid network stream.")]
    [Test]
    public void ClientConnection_Can_Get_Stream()
    {
        ClientConnection cc = new ClientConnection();
        object ae = new object();
        cc.setStream(ae as System.Net.Sockets.NetworkStream);
        Assert.That(() => {
            cc.getStream();
        }, Throws.Nothing);
    }

    [Test]
    public void ClientConnection_Can_Set_Client()
    {
        ClientConnection ae = new ClientConnection();
        System.Net.Sockets.TcpClient t = new System.Net.Sockets.TcpClient();
        Assert.That(() => {
            ae.setClient(t);
        }, Throws.Nothing);
    }
}