using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using PiShockTheSpire2.PiShockTheSpire2Code.Relics;
using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code;

[HarmonyPatch(typeof(RunManager), nameof(RunManager.OnEnded))]
class DeathPenaltyHandler
{
    [HarmonyPostfix]
    static void Postfix(bool isVictory)
    {
        // TODO: avoid triggering multiple times during online play.
        if (isVictory && Config.HealingVibrates)
        {
            _ = PiShockApiHandler.GenerateShockerOpsAsync(1, (int)Config.MaxDuration, (int)Config.MaxIntensity);
        }
        else if (!isVictory && Config.DeathPenalty)
        {
            _ = PiShockApiHandler.GenerateShockerOpsAsync(0, (int)Config.MaxDuration, (int)Config.MaxIntensity);
        }
    }
}