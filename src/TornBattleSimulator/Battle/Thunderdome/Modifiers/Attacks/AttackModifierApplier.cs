using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.BonusModifiers.Attacks;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;

public class AttackModifierApplier
{
    private readonly IChanceSource _chanceSource;

    public AttackModifierApplier(IChanceSource chanceSource)
    {
        _chanceSource = chanceSource;
    }

    public List<ThunderdomeEvent> MakeBonusAttacks(
        IAttacksModifier modifier,
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        Func<List<ThunderdomeEvent>> attackAction)
    {
        // This really shouldn't be like this. But
        // it'll do and I'm so lasy.
        return modifier switch
        {
            FuryModifier => Fury(context, active, other, weapon, attackAction),
            BlindfireModifier b => Blindfire(b, context, active, other, weapon, attackAction),
            RageModifier => Rage(context, active, other, weapon, attackAction),
        };
    }

    private List<ThunderdomeEvent> Fury(ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        Func<List<ThunderdomeEvent>> attackAction)
    {
        // Just a single attack.
        // Not supposed to be on loaded weapons,
        return weapon.RequiresReload
            ? new List<ThunderdomeEvent>(0)
            : attackAction();
    }

    private List<ThunderdomeEvent> Blindfire(
        BlindfireModifier blindfire,
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        Func<List<ThunderdomeEvent>> attackAction)
    {
        /*
         * Blindfire causes you to expend the remaining ammunition in your current clip in as many actions as required, in a single turn. 
         * With each successful action, the accuracy of the MG3 is reduced by 5.00.
         */
        // Not sure how this works with misses, or if the first bonus action
        // has the accuracy penalty
        List<ThunderdomeEvent> events = new();
        while (!weapon.RequiresReload)
        {
            events.AddRange(
                attackAction()
            );

            active.Modifiers.AddModifier(blindfire, null);

            // Should log in AddModifier() instead?
            events.Add(
                context.CreateEvent(active, ThunderdomeEventType.EffectBegin, new EffectBeginEvent(blindfire.Effect))
            );
        }

        return events;
    }

    public List<ThunderdomeEvent> Rage(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        Func<List<ThunderdomeEvent>> attackAction)
    {
        List<ThunderdomeEvent> events = new();
        int bonusAttacks = _chanceSource.ChooseRange(2, 9);

        while (!weapon.RequiresReload && bonusAttacks-- > 0)
        {
            events.AddRange(attackAction());
        }

        return events;
    }
}