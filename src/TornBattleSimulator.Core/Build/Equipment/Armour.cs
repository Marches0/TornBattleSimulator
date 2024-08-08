namespace TornBattleSimulator.Core.Build.Equipment;

public class Armour
{
    public string Name { get; set; }

    public double Rating { get; set; }

    public List<ModifierDescription> Modifiers { get; set; }
}