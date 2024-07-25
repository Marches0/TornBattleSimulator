using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Events.Data;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;

public class StackableStatModifierContainer : IStatsModifier, IModifierLifespan
{
    private List<ActiveModifier> _activeStacks = new();
    private readonly PlayerContext _self;

    public IStackableStatModifier Modifier { get; }
    public int Stacks => _activeStacks.Count;

    public bool Expired => false;
    public float Remaining => 1;

    public StackableStatModifierContainer(
        IStackableStatModifier modifier,
        PlayerContext target)
    {
        Modifier = modifier;
        _self = target;
    }

    public bool AddStack()
    {
        if (_activeStacks.Count >= Modifier.MaxStacks)
        {
            return false;
        }

        _activeStacks.Add(new ActiveModifier(Modifier.CreateLifespan(), Modifier));
        return true;
    }

    // The container lasts forever, the inner stacks have their own lifespan.
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => Modifier.Target;

    public ModifierApplication AppliesAt => Modifier.AppliesAt;

    public ModifierType Effect => Modifier.Effect;

    public float GetDefenceModifier() => GetStackedModifier(Modifier.GetDefenceModifier());

    public float GetDexterityModifier() => GetStackedModifier(Modifier.GetDexterityModifier());

    public float GetSpeedModifier() => GetStackedModifier(Modifier.GetSpeedModifier());

    public float GetStrengthModifier() => GetStackedModifier(Modifier.GetStrengthModifier());

    public StatModificationType Type => Modifier.Type;

    public ModifierValueBehaviour ValueBehaviour => Modifier.ValueBehaviour;

    private float GetStackedModifier(float value)
    {
        // Stacked modifiers interact additively; two 10% modifiers add to make 20%, rather than multiply to make 21%.
        // Take their differences from 0 and sum them to get their total additive contribution.
        // e.g. Two stacks of 0.9 -> -0.1 + -0.1 -> -0.2
        // Then add the 1 back.
        // -0.2 -> 0.8.
        float rawDifference = value - 1;
        return 1 + _activeStacks.Count * rawDifference;
    }

    public void TurnComplete(ThunderdomeContext context)
    {
        foreach (ActiveModifier stack in _activeStacks)
        {
            stack.CurrentLifespan.TurnComplete(context);
        }

        ExpireModifiers(context);
    }

    public void OwnActionComplete(ThunderdomeContext context)
    {
        foreach (ActiveModifier stack in _activeStacks)
        {
            stack.CurrentLifespan.OwnActionComplete(context);
        }

        ExpireModifiers(context);
    }

    public void OpponentActionComplete(ThunderdomeContext context)
    {
        foreach (ActiveModifier stack in _activeStacks)
        {
            stack.CurrentLifespan.OpponentActionComplete(context);
        }

        ExpireModifiers(context);
    }

    private void ExpireModifiers(ThunderdomeContext context)
    {
        // Should be in one central place
        var expiredEvents = _activeStacks
            .Where(m => m.CurrentLifespan.Expired)
            .Select(m => context.CreateEvent(_self, ThunderdomeEventType.EffectEnd, new EffectEndEvent(m.Modifier.Effect)));

        context.Events.AddRange(expiredEvents);

        _activeStacks = _activeStacks
            .Where(m => !m.CurrentLifespan.Expired)
            .ToList();
    }
}