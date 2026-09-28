using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code;

using BaseLib.Config;
using BaseLib.Config.UI;
using Godot;

[ConfigHoverTipsByDefault]
public class Config : SimpleModConfig
{
    [ConfigSection("ApiConfig")]
    [ConfigTextInput]
    public static string API_Key { get; set; } = "undefined";
    
    [ConfigHideInUI]
    public static string OpenShockApiUrl { get; set; } = ""; // For self-hosted OpenShock.

    [ConfigSection("AdditionalShockers")]
    public static string Shocker_ID { get; set; } = "";
    public static string Additional_Shocker_ID_1 { get; set; } = "";
    public static string Additional_Shocker_ID_2 { get; set; } = "";
    public static string Additional_Shocker_ID_3 { get; set; } = "";
    public static string Additional_Shocker_ID_4 { get; set; } = "";

    [ConfigSection("ShockerConfig")]
    [ConfigSlider(1, 100, Format = "{0} \u26a1")]
    public static double MinIntensity { get; set; } = 20f;

    [ConfigSlider(1, 100, Format = "{0} \u26a1")]
    public static double MaxIntensity { get; set; } = 100f;

    [ConfigSlider(0.3, 9, 0.1, Format = "{0}  s ")]
    public static double MinDuration { get; set; } = 1f;

    [ConfigSlider(0.3, 15, 0.1, Format = "{0}  s ")]
    public static double MaxDuration { get; set; } = 10f;

    public static bool AlwaysMaxPower { get; set; } = false;

    [ConfigSection("Gameplay")]
    public static bool FaultyInsulation { get; set; } = false;
    public static bool DeathPenalty { get; set; } = true;
    public static bool HealingVibrates { get; set; } = true;
    public static bool TriggerSelfDamage { get; set; } = true;
    public static bool AllowPunishments { get; set; } = true;

    [ConfigSection("Debug")]
    public static bool VibrateOnly { get; set; } = false;
    public static bool VerboseLogs { get; set; } = false;

    [ConfigButton("Vibrate")]
    public async Task SendVibrationTest()
    {
        await ShockUtil.DoOperationForAllAsync(Op.Buzz, TimeSpan.FromSeconds(MinDuration), (int)MaxIntensity);
    }

    public override void SetupConfigUI(Control optionContainer)
    {
        base.SetupConfigUI(optionContainer);

        var passwordRow = optionContainer.GetNodeOrNull<NConfigOptionRow>($"%{nameof(API_Key)}");
        if (passwordRow?.SettingControl is NConfigLineEdit API_password)
        {
            API_password.SetSecret(true);
        }
    }

    public static List<string> GetAllShockerIds()
    {
        var list = new List<string>();
        if (Shocker_ID != "") list.Add(Shocker_ID);
        if (Additional_Shocker_ID_1 != "") list.Add(Additional_Shocker_ID_1);
        if (Additional_Shocker_ID_2 != "") list.Add(Additional_Shocker_ID_2);
        if (Additional_Shocker_ID_3 != "") list.Add(Additional_Shocker_ID_3);
        if (Additional_Shocker_ID_4 != "") list.Add(Additional_Shocker_ID_4);
        return list;
    }

    public static bool IsValid()
    {
        if (API_Key is "undefined" or "")
        {
            MainFile.Logger.Warn("(Settings Configuration Error) API Key is undefined!");
            MainFile.Logger.Warn("Please adjust the PiShockTheSpire2 Configuration menu properly.");
            return false;
        }

        if (MaxIntensity < MinIntensity)
        {
            MainFile.Logger.Warn("(Settings Configuration Error) The Maximum Intensity of your shocker can't be less than its Minimum Intensity!");
            MainFile.Logger.Warn("Please adjust the PiShockTheSpire2 Configuration menu properly.");
            return false;
        }

        if (MaxDuration < MinDuration)
        {
            MainFile.Logger.Warn("(Settings Configuration Error) The Maximum Duration of your shocker can't be less than its Minimum Duration!");
            MainFile.Logger.Warn("Please adjust the PiShockTheSpire2 Configuration menu properly.");
            return false;
        }

        return true;
    }
}