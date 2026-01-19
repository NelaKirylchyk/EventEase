using EventEase.Models;

namespace EventEase.Services;

public interface IAttendanceTracker
{
 AttendanceRecord Get(string eventName);
 void Register(string eventName);
 void CheckIn(string eventName);
}

public sealed class AttendanceTracker : IAttendanceTracker
{
 private readonly Dictionary<string, AttendanceRecord> _records = new(StringComparer.OrdinalIgnoreCase);

 public AttendanceRecord Get(string eventName)
 {
 if (!_records.TryGetValue(eventName, out var rec))
 {
 rec = new AttendanceRecord { EventName = eventName };
 _records[eventName] = rec;
 }
 return rec;
 }

 public void Register(string eventName)
 {
 var rec = Get(eventName);
 rec.RegisteredCount++;
 }

 public void CheckIn(string eventName)
 {
 var rec = Get(eventName);
 if (rec.CheckedInCount < rec.RegisteredCount)
 {
 rec.CheckedInCount++;
 }
 }
}
