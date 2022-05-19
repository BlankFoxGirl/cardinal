namespace Cardinal.Entities;

public class Config : AbstractEntity
{
    public int? PORT { get; set; }
    public string? IP_ADDRESS { get; set; }
    public string? IDENTIFIER { get; set; }
    public bool IS_LISTENER { get; set; }
    public string? Queue { get; set; }

    public Config() {
        this.IS_LISTENER = true;
    }
}