using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using PiShockTheSpire2.PiShockTheSpire2Code.Powers;
using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Cards;

[Pool(typeof(TokenCardPool))]
public class Safeword() : CustomCardModel(0, CardType.Skill,
    CardRarity.Token, TargetType.Self)
{
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<Insulation>(1m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Insulation>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain, CardKeyword.Exhaust];

    /*
     * Apply<T>(
      PlayerChoiceContext choiceContext,
      IEnumerable<Creature> targets,
      Decimal amount,
      Creature? applier,
      CardModel? cardSource,
      bool silent = false)
     *
     */
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<Insulation>(choiceContext, base.Owner.Creature, base.DynamicVars["Insulation"].BaseValue,
            base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["Insulation"].UpgradeValueBy(1m);
    }
}