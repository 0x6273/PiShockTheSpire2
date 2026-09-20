using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using static PiShockTheSpire2.PiShockTheSpire2Code.Config;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Utils;

public static class PiShockApiHandler
{
    private static readonly HttpClient _client = new HttpClient();
    private static List<string> _shockersId = new List<string>();

    public static Task GenerateShockerOpsAsync(int operation, int duration, int intensity)
    {
        if (VerifyPiShockConfigData())
        {
            if (Config.VibrateOnly)
            {
                operation = 1;
            }

            foreach (var shockerId in _shockersId)
            {
                if (shockerId != "")
                {
                    MainFile.Logger.Info("Getting ready to call Shocker with ID: " + shockerId);
                    _ = ShockerOpsAsync(operation, duration, intensity, shockerId);
                }
            }
        }

        return Task.CompletedTask;
    }

    public static async Task ShockerOpsAsync(int operation, int duration, int intensity, string shockerId)
    {
        try
        {
            duration = RefineDuration(duration);
            intensity = RefineIntensity(intensity);

            string piShockUrl = "https://api.pishock.com/Shockers/" + shockerId;

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("accept", "*/*");
            _client.DefaultRequestHeaders.Add("X-PiShock-Api-Key", Config.API_Key);
            _client.DefaultRequestHeaders.Add("X-PiShock-Username", Config.Username);

            var payload = new
            {
                AgentName = "PiShockTheSpire2",
                Operation = operation,
                Duration = duration,
                Intensity = intensity
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync(piShockUrl, payload);

            // PiShock The Spire API Log:
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            int statusCode = (int)response.StatusCode;
            string statusCodeType = response.StatusCode.ToString();

            MainFile.Logger.Info("Request Success! Status code " + statusCode + ": " + statusCodeType + " -> " + responseString);
        }
        catch (Exception ex)
        {
            MainFile.Logger.Warn("API Request Error! " + ex.Message);
        }
    }

    private static bool VerifyPiShockConfigData()
    {
        if (Config.Username == "")
        {
            MainFile.Logger.Warn("(Settings Configuration Error) Username is undefined!");
            MainFile.Logger.Warn("Please adjust the PiShockTheSpire2 Configuration menu properly.");
            return false;
        }

        if (Config.API_Key == "undefined" || Config.API_Key == "")
        {
            MainFile.Logger.Warn("(Settings Configuration Error) API Key is undefined!");
            MainFile.Logger.Warn("Please adjust the PiShockTheSpire2 Configuration menu properly.");
            return false;
        }

        GenerateShockersIdList();
        if (_shockersId.Count == 0)
        {
            MainFile.Logger.Warn("(Settings Configuration Error) All Shocker IDs are undefined!");
            return false;
        }

        if (Config.MaxIntensity < Config.MinIntensity)
        {
            MainFile.Logger.Warn("(Settings Configuration Error) The Maximum Intensity of your shocker can't be less than its Minimum Intensity!");
            MainFile.Logger.Warn("Please adjust the PiShockTheSpire2 Configuration menu properly.");
            return false;
        }

        if (Config.MaxDuration < Config.MinDuration)
        {
            MainFile.Logger.Warn("(Settings Configuration Error) The Maximum Duration of your shocker can't be less than its Minimum Duration!");
            MainFile.Logger.Warn("Please adjust the PiShockTheSpire2 Configuration menu properly.");
            return false;
        }

        return true;
    }

    private static void GenerateShockersIdList()
    {
        _shockersId.Clear();
        // In today's episode of "there is nothing more permanent than a temporal solution"
        if (Config.Shocker_ID != "") {
            _shockersId.Add(Config.Shocker_ID);
        }
        if (Config.Additional_Shocker_ID_1 != "") {
            _shockersId.Add(Config.Additional_Shocker_ID_1);
        }
        if (Config.Additional_Shocker_ID_2 != "") {
            _shockersId.Add(Config.Additional_Shocker_ID_2);
        }
        if (Config.Additional_Shocker_ID_3 != "") {
            _shockersId.Add(Config.Additional_Shocker_ID_3);
        }
        if (Config.Additional_Shocker_ID_4 != "") {
            _shockersId.Add(Config.Additional_Shocker_ID_4);
        }
    }

    private static int RefineDuration(int duration)
    {
        if (duration < (int)Config.MinDuration) return ((int)Config.MinDuration * 1000);
        if (duration > (int)Config.MaxDuration) return ((int)Config.MaxDuration * 1000);

        return (duration * 1000);
    }

    private static int RefineIntensity(int intensity)
    {
        if (intensity < (int)Config.MinIntensity) return (int)Config.MinIntensity;
        if (intensity > (int)Config.MaxIntensity) return (int)Config.MaxIntensity;

        return intensity;
    }
}