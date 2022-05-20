namespace Cardinal.Event;
using Cardinal.Entities;
using Cardinal.Exceptions;

class AbstractEvent
{
    private string? IDENTIFIER;
    private string? TARGET;
    private string? PAYLOAD;


    public AbstractEvent() 
    {
    }

    public AbstractEvent(string message) 
    {
        this.parseEvent(message);
    }

    public AbstractEvent(string id, string target) 
    {
        this.setId(id);
        this.setTarget(target);
    }

    public AbstractEvent(string id, string target, string payload) 
    {
        this.setId(id);
        this.setTarget(target);
        this.setPayload(payload);
    }
    private AbstractEvent parseEvent(string message)
    {
        if (!this.eventIsValid(message)) {
            throw new InvalidMessageException("Event received is invalid.");
        }

        this.setId(this.getId(message) ?? "");
        this.setTarget(this.getTarget(message) ?? "");
        this.setPayload(this.getPayload(message) ?? "" );

        return this;
    }

    private bool eventIsValid(string message)
    {
        if (message.IndexOf("0x01:") == -1) {
            return false;
        }

        if (message.IndexOf("0x02:") == -1) {
            return false;
        }

        if (message.IndexOf("0x03:") == -1) {
            return false;
        }

        if (message.IndexOf("{|}") == -1) {
            return false;
        }

        return true;
    }

    private string? getId(string? message = null)
    {
        if (string.IsNullOrEmpty(message)) {
            return this.IDENTIFIER;
        }

        return this.getPart("0x01", message);
    }

    public string? getTarget(string? message = null)
    {
        if (string.IsNullOrEmpty(message)) {
            return this.TARGET;
        }

        return this.getPart("0x02", message);
    }

    public string? getPayload(string? message = null)
    {
        if (string.IsNullOrEmpty(message)) {
            return this.PAYLOAD;
        }

        return this.getPart("0x03", message);
    }

    private string? getPart(string identifier, string message)
    {
        string[] parts = message.Split("{|}");
        foreach(string part in parts) {
            int indexStart = part.IndexOf(identifier + ":");
            if (indexStart != -1) {
                // Found, return with this value;
                return part.Substring(indexStart + (identifier.Length + 1));
            }
        }
        return null;
    }

    public void setId(string id)
    {
        this.IDENTIFIER = id;
    }

    public void setTarget(string target)
    {
        this.TARGET = target;
    }

    public void setPayload(string payload)
    {
        this.PAYLOAD = payload;
    }

    public string compile()
    {
        string sep = "{|}";
        return "0x01:" + this.IDENTIFIER + sep + "0x02:" + this.TARGET + sep + "0x03:" + this.PAYLOAD;
    }
}