namespace Cardinal.Entities;

public class AbstractEntity
{
    public string? uuid { get; set; }

    public AbstractEntity()
    {
        // Creates new abstract entity.
        if (string.IsNullOrEmpty(this.uuid))
        {
            // Generate UUID.
            this.newUUID();
        }
    }

    private void newUUID()
    {
        this.uuid = Guid.NewGuid().ToString();
    }

    /// <summary>Returns the property of an Cardinal.Entities.AbstractEntity as a System.Object which needs to be cast.</summary>
    /// <param name="propertyName">String name of the property to return as a System.Object.</param>
    public Object getProperty(string propertyName)
    {
        var property = this.GetType().GetProperty(propertyName);

        if (property == null)
        {
            throw new Cardinal.Exceptions.EntityPropertyNotFoundException(
                "Property " + propertyName + " could not be found on " + this.GetType().ToString()
            );
        }

        var propertyGetter = property.GetGetMethod();

        if (property.CanRead == false || propertyGetter == null)
        {
            throw new Cardinal.Exceptions.EntityPropertyNotFoundException(
                "Property " + propertyName + " could not be read from " + this.GetType().ToString()
            );
        }

        return (Object)(propertyGetter.Invoke(this, new Object[] { }) ?? new Object());
    }

    /// <summary>Returns the property of an Cardinal.Entities.AbstractEntity as a System.Object which needs to be cast.</summary>
    /// <param name="propertyName">String name of the property to return as a System.Object.</param>
    public void setProperty(string propertyName, object value)
    {
        var property = this.GetType().GetProperty(propertyName);

        if (property == null)
        {
            throw new Cardinal.Exceptions.EntityPropertyNotFoundException(
                "Property " + propertyName + " could not be found on " + this.GetType().ToString()
            );
        }

        var propertySetter = property.GetSetMethod();
        if (propertySetter == null || !property.CanWrite)
        {
            throw new Cardinal.Exceptions.EntityPropertyNotWritableException(
                "Property " + propertyName + " could not be written to " + this.GetType().ToString()
            );
        }

        propertySetter.Invoke(this, new Object[] { value });
    }

}
