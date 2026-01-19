namespace EventEase.Models;

public sealed class AttendanceRecord
{
 public string EventName { get; set; } = string.Empty;
 public int RegisteredCount { get; set; }
 public int CheckedInCount { get; set; }
}
