using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code;

using BaseLib.Config;
using BaseLib.Config.UI;
using Godot;

[ConfigHoverTipsByDefault]
public class Config : SimpleModConfig
{
    [ConfigSection("ApiConfig")]
    public static string Username { get; set; } = "undefined";
    [ConfigTextInput]
    public static string API_Key { get; set; } = "undefined";

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

    [ConfigSlider(1, 9, Format = "{0}  s ")]
    public static double MinDuration { get; set; } = 1f;

    [ConfigSlider(1, 15, Format = "{0}  s ")]
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
        await PiShockApiHandler.GenerateShockerOpsAsync(1, (int)MaxDuration, (int)MaxIntensity);
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
}