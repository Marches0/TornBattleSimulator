using TornBattleSimulator.Core.Build.Equipment;

namespace TornBattleSimulator.Core.Thunderdome.Strategy;

public class StrategyDescription
{
    public WeaponType Weapon { get; set; }

    public bool Reload { get; set; }

    public List<StrategyUntil> Until { get; set; } = new();
}