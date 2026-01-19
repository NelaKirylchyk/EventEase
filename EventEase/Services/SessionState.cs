using Microsoft.JSInterop;

namespace EventEase.Services;

public interface ISessionState
{
 Task SetAsync(string key, string value);
 Task<string?> GetAsync(string key);
 Task RemoveAsync(string key);
}

public sealed class SessionState : ISessionState, IAsyncDisposable
{
 private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
 private readonly IJSRuntime _js;

 public SessionState(IJSRuntime js)
 {
 _js = js;
 _moduleTask = new(() => _js.InvokeAsync<IJSObjectReference>("import", "./js/storage.js").AsTask());
 }

 public async Task SetAsync(string key, string value)
 {
 var module = await _moduleTask.Value;
 await module.InvokeVoidAsync("set", key, value);
 }

 public async Task<string?> GetAsync(string key)
 {
 var module = await _moduleTask.Value;
 return await module.InvokeAsync<string?>("get", key);
 }

 public async Task RemoveAsync(string key)
 {
 var module = await _moduleTask.Value;
 await module.InvokeVoidAsync("remove", key);
 }

 public async ValueTask DisposeAsync()
 {
 if (_moduleTask.IsValueCreated)
 {
 var module = await _moduleTask.Value;
 await module.DisposeAsync();
 }
 }
}
