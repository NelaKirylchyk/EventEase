namespace EventEase.Models;

public sealed class EventItem
{
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Today;
    public string Location { get; set; } = string.Empty;
}