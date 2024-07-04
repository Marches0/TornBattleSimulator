using System.Collections.ObjectModel;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;
public class ModifierContext
{
    public ReadOnlyCollection<IModifier> Active => new ReadOnlyCollection<IModifier> (_activeModifiers.Select(m => m.Modifier).ToList());

    private List<ActiveModifier> _activeModifiers = new();

    public void Tick(ThunderdomeContext thunderdomeContext)
    {
        foreach (ActiveModifier modifier in _activeModifiers)
        {
            modifier.CurrentLifespan.Tick(thunderdomeContext);
        }

        _activeModifiers = _activeModifiers
            .Where(m => !m.CurrentLifespan.Expired)
            .ToList();
    }

    public bool AddModifier(IModifier modifier)
    {
        _activeModifiers.Add(new ActiveModifier(CreateLifespan(modifier), modifier));
        return true;
    }

    private IModifierLifespan CreateLifespan(IModifier modifier)
    {
        return modifier.Lifespan.LifespanType switch
        {
            ModifierLifespanType.Temporal => new TemporalModifierLifespan(modifier.Lifespan.Duration!.Value),
            _ => throw new NotImplementedException(modifier.Lifespan.LifespanType.ToString())
        };
    }
}