namespace Cardinal.Controller;

interface IEvent
{
    public void OnReceive(string message) {}
}