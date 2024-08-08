using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Core.Thunderdome.Player.Armours;

public class ArmourSetContext
{
    public ArmourSetContext(List<ArmourContext> armour)
    {
        Armour = armour;
    }

    public List<ArmourContext> Armour { get; }

    public List<PotentialModifier> PotentialModifiers => Armour
        .SelectMany(a => a.PotentialModifiers)
        .ToList();
}