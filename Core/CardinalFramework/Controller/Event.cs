namespace Cardinal.Controller;
using Cardinal.Event;
using Cardinal.Exceptions;

public class Event : AbstractController, IEvent
{
    private Cardinal.Service.ILogger log = new Cardinal.Service.Logger();
    private string Topic = "";
    public Event(string Topic)
    {
        this.Topic = Topic;
        Cardinal.Service.Redis.Subscribe(Topic, async message => {
            try {
                this.OnReceive(message.ToString());
            } catch (Exception e) {
                this.log.Error(e.Message, message.ToString());
            }
        });
        Cardinal.Event.AbstractEvent e = new AbstractEvent("Sup!","AbstractEvent","Test");
        Cardinal.Service.Redis.Publish(this.Topic, e);
    }
    public void OnReceive(string message) {
        // This processes messages.
        this.log.Debug("Received Message: ", message);
        AbstractEvent e = new AbstractEvent(message);

        AbstractEvent[] arg = {e};

        string? target = e.getTarget(message);

        if (string.IsNullOrEmpty(target)) {
            throw new InvalidEventException("No target specified.");
        }

        Activator.CreateInstance("Cardinal.Event", target, arg);
    }
}