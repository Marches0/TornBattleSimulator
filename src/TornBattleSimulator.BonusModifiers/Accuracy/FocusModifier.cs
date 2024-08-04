using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Accuracy;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Accuracy;

public class FocusModifier : IModifier, IConditionalModifier, IAccuracyModifier, IPostAttackBehaviour
{
    private readonly double _value;

    public FocusModifier(double value)
    {
        _value = 1 + value;
    }

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.SelfWeapon;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Focus;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public bool CanActivate(
        PlayerContext active,
        PlayerContext other,
        AttackResult? attack)
    {
        return !attack!.Hit;
    }

    /// <inheritdoc/>
    public double GetAccuracyModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon) => _value;

    public List<ThunderdomeEvent> PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        AttackResult attackResult,
        bool bonusAction)
    {
        // Only clears Focus if you miss with the weapon that has it
        // Using a different weapon means it doesn't change
        // Shoving the tester here is a bit weird. If there are more modifiers
        // that act like this, change it.
        if (attackResult.Hit)
        {
            int removedCount = weapon.Modifiers.RemoveModifier(this);
            return Enumerable.Repeat(
                context.CreateEvent(active, ThunderdomeEventType.EffectEnd, new EffectEndEvent(ModifierType.Focus)),
                removedCount
                ).ToList();
        }

        return [];
    }
}