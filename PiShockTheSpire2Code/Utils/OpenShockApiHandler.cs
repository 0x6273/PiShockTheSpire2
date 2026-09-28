using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static PiShockTheSpire2.PiShockTheSpire2Code.Config;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Utils;

public class OpenShockApiHandler : IShockBackend
{
    public string BackendName { get; } = "OpenShock http";
    
    private static readonly HttpClient Client = new HttpClient();

    private readonly Uri _apiUrlBase;

    public OpenShockApiHandler()
    {
        _apiUrlBase = Config.OpenShockApiUrl is "" or "undefined"
            ? new Uri("https://api.openshock.app")
            : new Uri(Config.OpenShockApiUrl);
        
        Client.DefaultRequestHeaders.Clear();
        Client.DefaultRequestHeaders.Add("Open-Shock-Token", Config.API_Key);
        Client.DefaultRequestHeaders.Add("User-Agent", "PiShockTheSpire/1.0");
    }
    
    public async Task DoOperationAsync(Op operation, IEnumerable<string> shockerIds, TimeSpan duration, int intensity = 0)
    {
        var operationStr = operation switch
        {
            Op.Zap => "Shock",
            Op.Buzz => "Vibrate",
            Op.Beep => "Sound",
            Op.Stop => "Stop",
            _ => throw new Exception("Operation not supported by PiShock API backend."),
        };
        
        var payload = new {
            customName = "PiShockTheSpire2",
            shocks = shockerIds.Select(id => new
            {
                id = id,
                duration = (int)duration.TotalMilliseconds,
                intensity = intensity,
                type = operationStr,
            }).ToList(),
        };

        var json = JsonSerializer.Serialize(payload);
        MainFile.Logger.Info($"Request JSON: {json}");
        
        var message = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_apiUrlBase, "2/shockers/control"),
            Headers = { {"Accept", "application/json"} },
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };
        
        HttpResponseMessage response = await Client.SendAsync(message);
        
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var statusCode = (int)response.StatusCode;
        var statusCodeType = response.StatusCode.ToString();

        MainFile.Logger.Info("Request Success! Status code " + statusCode + ": " + statusCodeType + " -> " + responseString);
    }
}