using TornBattleSimulator.Shared.Build.Equipment;

namespace TornBattleSimulator.Shared.Thunderdome.Strategy;

public class StrategyDescription
{
    public WeaponType Weapon { get; set; }

    public bool Reload { get; set; }

    public List<StrategyUntil> Until { get; set; }
}