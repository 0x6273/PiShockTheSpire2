namespace PiShockTheSpire2.PiShockTheSpire2Code.Utils;

public static class ShockUtil
{
    /// <summary>
    /// Do a shock/vibrate/beep operation for select shockers.
    /// </summary>
    public static async Task DoOperationAsync(Op op, IEnumerable<string> shockerIds, TimeSpan duration, int intensity = 0)
    {
        if (Config.VibrateOnly)
        {
            op = Op.Buzz;
        }

        if (!Config.IsValid())
        {
            return;
        }
        
        intensity = RefineIntensity(intensity);
        duration  = RefineDuration(duration);

        IShockBackend backend;
        try
        {
            backend = GetBackend();
        }
        catch (Exception e)
        {
            MainFile.Logger.Error("Failed to create backend! " + e.Message);
            throw;
        }
        
        MainFile.Logger.Info($"{backend.BackendName}: sending {op} for {duration.TotalMilliseconds}ms with intensity {intensity}");
        try
        {
            await backend.DoOperationAsync(op, shockerIds, duration, intensity);
        }
        catch (Exception e)
        {
            MainFile.Logger.Error($"{backend.BackendName} Error! {e.Message}");
            throw;
        }
    }

    /// <summary>
    /// Do a shock/vibrate/beep operation for all shockers.
    /// </summary>
    public static async Task DoOperationForAllAsync(Op op, TimeSpan duration, int intensity = 0)
    {
        await DoOperationAsync(op, Config.GetAllShockerIds(), duration, intensity);
    }

    private static IShockBackend GetBackend()
    {
        // Because PiShock and OpenShock API keys are different lengths, we can auto-detect which backend to use.
        return Config.API_Key.Length switch
        {
            32 | 36 => new PiShockApiHandler(), // PiShock UUID, with or without the 4 dashes.
            64 => new OpenShockApiHandler(), // OpenShock Token.
            _ => throw new Exception("Unable to determine backend from API key")
        };
    }
    
    private static TimeSpan RefineDuration(TimeSpan duration)
    {
        if (duration < TimeSpan.FromSeconds(Config.MinDuration))
            return TimeSpan.FromSeconds(Config.MinDuration);
        if (duration > TimeSpan.FromSeconds(Config.MaxDuration))
            return TimeSpan.FromSeconds(Config.MaxDuration);

        return duration;
    }

    private static int RefineIntensity(int intensity)
    {
        if (intensity < (int)Config.MinIntensity) return (int)Config.MinIntensity;
        if (intensity > (int)Config.MaxIntensity) return (int)Config.MaxIntensity;
        
        return intensity;
    }
}

public enum Op
{
    Zap,
    Buzz,
    Beep,
    Stop,
}