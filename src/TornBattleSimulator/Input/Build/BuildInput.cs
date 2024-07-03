using TornBattleSimulator.Input.Build.Gear;
using TornBattleSimulator.Input.Build.Stats;

namespace TornBattleSimulator.Input.Build;

public class BuildInput
{
    public string? Name { get; set; }

    public uint? Health { get; set; }

    public BattleStatsInput? BattleStats { get; set; }

    public WeaponInput? Primary { get; set; }

    public WeaponInput? Secondary { get; set; }

    public WeaponInput? Melee { get; set; }

    public string? Temporary { get; set; }

    public List<StrategyInput>? Strategy { get; set; }
}