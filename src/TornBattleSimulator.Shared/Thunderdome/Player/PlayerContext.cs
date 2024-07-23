using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Battle.Thunderdome.Player;
using TornBattleSimulator.Battle.Thunderdome.Player.Armours;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;
using TornBattleSimulator.Extensions;

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