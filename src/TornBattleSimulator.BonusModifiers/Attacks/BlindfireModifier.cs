using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Extensions;
using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Events.Data;
using TornBattleSimulator.Shared.Thunderdome.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Attacks;

public class BlindfireModifier : IAttacksModifier
{
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Blindfire;

    public bool Stackable => true;

    public List<ThunderdomeEvent> MakeAttack(
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

            active.Modifiers.AddModifier(this, null);

            // Should log in AddModifier() instead?
            events.Add(
                context.CreateEvent(active, ThunderdomeEventType.EffectBegin, new EffectBeginEvent(Effect))
            );
        }

        return events;
    }
}