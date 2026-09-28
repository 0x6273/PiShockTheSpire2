using System.Net.Http.Json;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Utils;

public class PiShockApiHandler : IShockBackend
{
    public string BackendName { get; } = "PiShock http";
    
    private static readonly HttpClient Client = new HttpClient();

    public PiShockApiHandler()
    {
        Client.DefaultRequestHeaders.Clear();
        Client.DefaultRequestHeaders.Add("accept", "application/json");
        Client.DefaultRequestHeaders.Add("X-PiShock-Api-Key", Config.API_Key);
        Client.DefaultRequestHeaders.Add("User-Agent", "PiShockTheSpire/1.0");
    }
    
    public async Task DoOperationAsync(Op operation, IEnumerable<string> shockerIds, TimeSpan duration, int intensity = 0)
    {
        foreach (var shockerId in shockerIds)
        {
            MainFile.Logger.Info("Getting ready to call Shocker with ID: " + shockerId);
            _ = ShockerOpsAsync(operation, shockerId, duration, intensity);
        }
    }
    
    private static async Task ShockerOpsAsync(Op operation, string shockerId, TimeSpan duration, int intensity)
    {
        var piShockUrl = $"https://api.pishock.com/Shockers/OperateById/{shockerId}";
        
        var operationNum = operation switch
        {
            Op.Zap => 0,
            Op.Buzz => 1,
            Op.Beep => 2,
            _ => throw new Exception("Operation not supported by PiShock API backend."),
        };
        
        var payload = new
        {
            AgentName = "PiShockTheSpire2",
            Operation = operationNum,
            Duration = (int)duration.TotalMilliseconds,
            Intensity = intensity
        };

        HttpResponseMessage response = await Client.PostAsJsonAsync(piShockUrl, payload);

        // PiShock The Spire API Log:
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        int statusCode = (int)response.StatusCode;
        string statusCodeType = response.StatusCode.ToString();

        MainFile.Logger.Info("Request Success! Status code " + statusCode + ": " + statusCodeType + " -> " + responseString);
    }
}