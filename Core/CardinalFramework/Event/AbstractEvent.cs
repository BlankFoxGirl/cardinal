namespace Cardinal.Event;
using Cardinal.Entities;
using Cardinal.Exceptions;
using Cardinal.Consts;

public class AbstractEvent
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
        this.setIdentifier(id);
        this.setTarget(target);
    }

    public AbstractEvent(string id, string target, string payload) 
    {
        this.setIdentifier(id);
        this.setTarget(target);
        this.setPayload(payload);
    }

    /// <param name="message">(Optional) event string to scan for validity.</param>
    /// <summary>Scans an event or event string to determine if the event is valid.</summary>
    /// <returns>true on success, false if not valid.</returns>
    public bool eventIsValid(string? message = null)
    {
        if (!this.hasIdentifier(message)) {
            return false;
        }

        if (!this.hasTarget(message)) {
            return false;
        }

        if (!this.hasPayload(message)) {
            return false;
        }

        if (!string.IsNullOrEmpty(message)) {
            if (message.IndexOf("{|}") == -1) {
                return false;
            }
        }

        return true;
    }

    /// <param name="message">(Optional) event string to scan for identifier.</param>
    /// <summary>Scans the event or event string for an identifier.</summary>
    /// <returns>true on success, false if not found.</returns>
    public bool hasIdentifier(string? message = null)
    {
        if (!string.IsNullOrEmpty(message)) {
            if (message.IndexOf(EventHeader.toC(EventHeader.Code.IDENTIFIER) + ":") == -1) {
                return false;
            }

            return true;
        }

        if (string.IsNullOrEmpty(this.getIdentifier())) {
            return false;
        }

        return true;
    }

    /// <param name="message">(Optional) event string to scan for target.</param>
    /// <summary>Scans the event or event string for a target.</summary>
    /// <returns>true on success, false if not found.</returns>
    public bool hasTarget(string? message = null)
    {
        if (!string.IsNullOrEmpty(message)) {
            if (message.IndexOf(EventHeader.toC(EventHeader.Code.TARGET) + ":") == -1) {
                return false;
            }

            return true;
        }

        if (string.IsNullOrEmpty(this.getTarget())) {
            return false;
        }

        return true;
    }

    /// <param name="message">(Optional) event string to scan for payload.</param>
    /// <summary>Scans the event or event string for a payload.</summary>
    /// <returns>true on success, false if not found.</returns>
    public bool hasPayload(string? message = null)
    {
        if (!string.IsNullOrEmpty(message)) {
            if (message.IndexOf(EventHeader.toC(EventHeader.Code.PAYLOAD) + ":") == -1) {
                return false;
            }

            return true;
        }

        if (string.IsNullOrEmpty(this.getPayload())) {
            return false;
        }

        return true;
    }

    /// <param name="message">(Optional) event string to scan for identifier.</param>
    /// <summary>Scans the event or event string for an identifier.</summary>
    /// <returns>With the value of the identifier.</returns>
    public string? getIdentifier(string? message = null)
    {
        if (string.IsNullOrEmpty(message)) {
            return this.IDENTIFIER;
        }

        return this.getPart(EventHeader.toC(EventHeader.Code.IDENTIFIER), message);
    }

    /// <param name="message">(Optional) event string to scan for target.</param>
    /// <summary>Scans the event or event string for a target.</summary>
    /// <returns>With the value of the target.</returns>
    public string? getTarget(string? message = null)
    {
        if (string.IsNullOrEmpty(message)) {
            return this.TARGET;
        }

        return this.getPart(EventHeader.toC(EventHeader.Code.TARGET), message);
    }

    /// <param name="message">(Optional) event string to scan for payload.</param>
    /// <summary>Scans the event or event string for a payload.</summary>
    /// <returns>With the value of the payload.</returns>
    public string? getPayload(string? message = null)
    {
        if (string.IsNullOrEmpty(message)) {
            return this.PAYLOAD;
        }

        return this.getPart(EventHeader.toC(EventHeader.Code.PAYLOAD), message);
    }

    /// <param name="message">New event identifier value.</param>
    /// <summary>Sets the value of the event identifier.</summary>
    public void setIdentifier(string id)
    {
        this.IDENTIFIER = id;
    }

    /// <param name="message">New event target value.</param>
    /// <summary>Sets the value of the event target.</summary>
    public void setTarget(string target)
    {
        this.TARGET = target;
    }

    /// <param name="message">New event payload value.</param>
    /// <summary>Sets the value of the event payload.</summary>
    public void setPayload(string payload)
    {
        this.PAYLOAD = payload;
    }

    /// <summary>Compiles an event string from the values of the current event.</summary>
    /// <returns>An event string representing the current event.</returns>
    public string compile()
    {
        string response = "";

        response += this.compilePart(response, EventHeader.Code.IDENTIFIER);
        response += this.compilePart(response, EventHeader.Code.TARGET);
        response += this.compilePart(response, EventHeader.Code.PAYLOAD);
        if (response.Length < 5) {
            return "";
        }

        return response.Substring(0, response.Length - 3);
    }

    /// <param name="message">Event string to be parsed into the current event.</param>
    /// <summary>Parses event string into the current event object.</summary>
    private AbstractEvent parseEvent(string message)
    {
        if (!this.eventIsValid(message)) {
            throw new InvalidMessageException("Event received is invalid.");
        }

        this.setProperty(EventHeader.toS(EventHeader.Code.IDENTIFIER), this.getIdentifier(message) ?? "");
        this.setProperty(EventHeader.toS(EventHeader.Code.TARGET), this.getTarget(message) ?? "");
        this.setProperty(EventHeader.toS(EventHeader.Code.PAYLOAD), this.getPayload(message) ?? "");

        return this;
    }
    private string compilePart(string response, EventHeader.Code code)
    {
        if (!this.hasProperty(EventHeader.toS(code))) {
            return "";
        }

        string returnResponse = this.toKey(EventHeader.toC(code));
        returnResponse += this.padd(this.getProperty(EventHeader.toS(code)));

        return returnResponse;
    }

    private string ucFirst(string input)
    {
        return input[0].ToString().ToUpper() + input.Substring(1).ToLower();
    }
    private bool hasProperty(string valueId)
    {
        // Value ucfirst.
        valueId = "has" + this.ucFirst(valueId);
        var method = this.GetType().GetMethod(valueId);
        if (method != null) {
            var resp = method.Invoke(this, new object[]{ "" }) ?? false;
            return (bool)resp;
        }

        return false;
    }
    private string getProperty(string valueId, string arg = "")
    {
        // Value ucfirst.
        valueId = "get" + this.ucFirst(valueId);
        var method = this.GetType().GetMethod(valueId);

        if (method != null) {
            return (string)(method.Invoke(this, new object[]{ arg }) ?? "");
        }

        return "";
    }
    private void setProperty(string valueId, string value)
    {
        // Value ucfirst.
        valueId = "set" + this.ucFirst(valueId);
        var method = this.GetType().GetMethod(valueId);
        if (method != null) {
            method.Invoke(this, new object[]{value});
        }
        return;
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

    private string padd(string value)
    {
        string sep = "{|}";
        return value + sep;
    }

    private string toKey(string constant)
    {
        return constant + ":";
    }
}