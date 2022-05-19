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

        if (e == null) {
            this.log.Verbose("Excited.");
            return;
        }

        AbstractEvent[] arg = {e};

        try {
            string? target = e.getTarget();

            if (target == null) {
                throw new Exception("No target specified.");
            }

            Activator.CreateInstance("Cardinal.Event", target, arg);
        } catch (Exception exception) {
            throw new InvalidEventException(exception.Message);
        }
    }
}