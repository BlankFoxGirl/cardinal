using System;

namespace Cardinal.Entities;
public class AbstractEntity
{
    string? uuid { get; set; }

    public AbstractEntity()
    {
        // Creates new abstract entity.
        if (string.IsNullOrEmpty(this.uuid)) {
            // Generate UUID.
            this.newUUID();
        }
    }

    private void newUUID()
    {
        this.uuid = Guid.NewGuid().ToString();
    }


}
