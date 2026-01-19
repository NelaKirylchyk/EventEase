using EventEase.Models;

namespace EventEase.Services;

public interface IEventsState
{
 ICollection<EventItem> Events { get; }
 EventItem? FindByName(string? name);
}

public sealed class EventsState : IEventsState
{
 private readonly List<EventItem> _events =
 [
 new() { Name = "TechConf2026", Date = new DateTime(2026,5,12), Location = "Seattle, WA" },
 new() { Name = "Open Source Summit", Date = new DateTime(2026,8,3), Location = "Austin, TX" },
 new() { Name = "Cloud Expo", Date = new DateTime(2026,10,21), Location = "New York, NY" }
 ];

 public ICollection<EventItem> Events => _events;

 public EventItem? FindByName(string? name)
 {
 if (string.IsNullOrWhiteSpace(name)) return null;
 var key = name.Trim();
 return _events.FirstOrDefault(e => string.Equals(e.Name, key, StringComparison.OrdinalIgnoreCase));
 }
}
