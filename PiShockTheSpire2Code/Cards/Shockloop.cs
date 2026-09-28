using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using PiShockTheSpire2.PiShockTheSpire2Code.Powers;
using PiShockTheSpire2.PiShockTheSpire2Code.Relics;
using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Cards;

[Pool(typeof(ColorlessCardPool))]
public class Shockloop() : CustomCardModel(0, CardType.Skill,
    CardRarity.Rare, TargetType.AllAllies)
{
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    protected override bool HasEnergyCostX => true;
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int nOfMultiShocks = ResolveEnergyXValue();
        if (base.IsUpgraded)
        {
            nOfMultiShocks++;
        }

        if (Config.AllowPunishments)
        {
            IEnumerable<Creature> enumerable = from c in base.CombatState?.GetTeammatesOf(base.Owner.Creature)
                where c != null && c.IsAlive && c.IsPlayer
                select c;
            foreach (Creature crtr in enumerable)
            {
                PishockCollar? aux = crtr.Player?.GetRelic<PishockCollar>();
                await aux?.TriggerMultiShock(nOfMultiShocks)!;
            }
        }
    }
}