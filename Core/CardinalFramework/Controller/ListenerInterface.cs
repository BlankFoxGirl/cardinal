namespace Cardinal.Controller;

public interface IListener
{
    public bool IsInit();
    public void ListenForConnections();
    public void Stop();
}