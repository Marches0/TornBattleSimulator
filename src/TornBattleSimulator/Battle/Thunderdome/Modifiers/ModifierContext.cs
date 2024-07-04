namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;
public class ModifierContext
{
    public List<IModifier> Active { get; private set; } = new();

    public void Tick(
        float tickTime)
    {
        foreach (IModifier modifier in Active)

        // handle other expiry types, e.g. "after next action"
        {
            modifier.TimeRemainingSeconds -= tickTime;

            if (modifier.TimeRemainingSeconds <= 0)
            {
                // track
            }
        }

        Active = Active
            .Where(m => m.TimeRemainingSeconds > 0)
            .ToList();
    }

    public bool AddModifier(IModifier modifier)
    {
        Active.Add(modifier);
        return true;
    }
}