using TornBattleSimulator.Shared.Build;
using TornBattleSimulator.Shared.Extensions;
using TornBattleSimulator.Shared.Thunderdome.Actions;
using TornBattleSimulator.Shared.Thunderdome.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Shared.Thunderdome.Player.Armours;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;
using TornBattleSimulator.Shared.Thunderdome.Strategy;

namespace TornBattleSimulator.Shared.Thunderdome.Player;

/// <summary>
///  A build taking part in a battle.
/// </summary>
public class PlayerContext : ITickable
{
    public PlayerContext(
        BattleBuild build,
        PlayerType playerType,
        EquippedWeapons weapons,
        ArmourSetContext armourSet,
        IStrategy strategy)
    {
        Build = build;
        Strategy = strategy;
        Weapons = weapons;
        ArmourSet = armourSet;
        Health = new((int)build.Health);
        PlayerType = playerType;

        Modifiers = new(this);
    }

    /// <summary>
    ///  The build taking part.
    /// </summary>
    public BattleBuild Build { get; }

    public IStrategy Strategy { get; }

    /// <summary>
    ///  Modifiers currently applied to the player.
    /// </summary>
    public ModifierContext Modifiers { get; }

    public PlayerHealth Health { get; set; }

    public EquippedWeapons Weapons { get; }
    public ArmourSetContext ArmourSet { get; }
    public PlayerType PlayerType { get; }

    /// <summary>
    ///  The build's current stats.
    /// </summary>
    public BattleStats Stats => new BattleStats()
    {
        Strength = Build.BattleStats.Strength,
        Defence = Build.BattleStats.Defence,
        Speed = Build.BattleStats.Speed,
        Dexterity = Build.BattleStats.Dexterity,
    }.WithModifiers(Modifiers.Active.OfType<IStatsModifier>().ToList());

    /// <summary>
    ///  The action being taken by this player in the current tick.
    /// </summary>
    public BattleAction CurrentAction { get; set; }

    public void OwnActionComplete(ThunderdomeContext context)
    {
        Modifiers.OwnActionComplete(context);
    }

    public void OpponentActionComplete(ThunderdomeContext context)
    {
        Modifiers.OpponentActionComplete(context);
    }

    public void TurnComplete(ThunderdomeContext context)
    {
        Modifiers.TurnComplete(context);
    }
}