using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using PiShockTheSpire2.PiShockTheSpire2Code.Relics;

namespace PiShockTheSpire2.PiShockTheSpire2Code;

[HarmonyPatch(typeof(Hook), nameof(Hook.AfterActEntered))]
class AddRelicAtStart
{
    [HarmonyPostfix]
    static void Postfix(IRunState runState)
    {
        if (runState.CurrentActIndex == 0)
        {
            foreach (Player player in runState.Players)
            {
                MainFile.Logger.Info("Generating PiShock Collar Relic...");
                RelicModel myCustomRelic = ModelDb.Relic<PishockCollar>();
                RelicCmd.Obtain(ModelDb.Relic<PishockCollar>().ToMutable(), player);
            }
        }
    }
}