namespace Cardinal.Controller;

public interface IEvent
{
    public void OnReceive(string message);
    public bool IsInit();
}