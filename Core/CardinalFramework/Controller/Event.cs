namespace Cardinal.Controller;
using Cardinal.Event;
using Cardinal.Exceptions;

public class Event : AbstractController, IEvent
{
    private Cardinal.Service.ILogger log = new Cardinal.Service.Logger();
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