using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Extensions;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

public class ModifierApplier
{
    private readonly IChanceSource _modifierChanceSource;

    public ModifierApplier(IChanceSource modifierChanceSource)
    {
        _modifierChanceSource = modifierChanceSource;
    }

    public List<ThunderdomeEvent> ApplyPreActionModifiers(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        List<PotentialModifier> potentialModifiers)
    {
        List<ThunderdomeEvent> events = new();

        foreach (PotentialModifier modifier in potentialModifiers
            .Where(m => m.Modifier.AppliesAt == ModifierApplication.BeforeAction)
            .Where(m => _modifierChanceSource.Succeeds(m.Chance)))
        {

        }

        return events;
    }

    public List<ThunderdomeEvent> ApplyPostActionModifiers(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        List<PotentialModifier> potentialModifiers)
    {
        List<ThunderdomeEvent> events = new();
        
        foreach (PotentialModifier modifier in potentialModifiers
            .Where(m => m.Modifier.AppliesAt == ModifierApplication.AfterAction)
            .Where(m => _modifierChanceSource.Succeeds(m.Chance)))
        {
            PlayerContext target = modifier.Modifier.Target == ModifierTarget.Self
                ? active
                : other;

            if (target.Modifiers.AddModifier(modifier.Modifier))
            {
                events.Add(context.CreateEvent(target, ThunderdomeEventType.EffectBegin, new EffectBeginEvent(modifier.Modifier.Effect)));
            };
        }

        return events;
    }
}