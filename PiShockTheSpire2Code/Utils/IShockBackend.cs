namespace PiShockTheSpire2.PiShockTheSpire2Code.Utils;

public interface IShockBackend
{
    public string BackendName { get; }
    public Task DoOperationAsync(Op operation, IEnumerable<string> shockerIds, TimeSpan duration, int intensity = 0);
}