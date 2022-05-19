namespace Cardinal.Entities;

public class Event : AbstractEntity
{
    string? key { get; set; }
    string? payload { get; set ; }
}