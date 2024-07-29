using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.Core.Thunderdome.Player;

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
        Health = new(build.Health);
        PlayerType = playerType;

        Modifiers = new ModifierContext(this);
    }

    /// <summary>
    ///  The build taking part.
    /// </summary>
    public BattleBuild Build { get; }

    public IStrategy Strategy { get; }

    /// <summary>
    ///  Modifiers currently applied to the player.
    /// </summary>
    public IModifierContext Modifiers { get; }

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

    /// <inheritdoc/>
    public void FightBegin(ThunderdomeContext context) 
    {
        Modifiers.FightBegin(context);
        Weapons.FightBegin(context);
    }

    /// <inheritdoc/>
    public void OwnActionComplete(ThunderdomeContext context)
    {
        Modifiers.OwnActionComplete(context);
        Weapons.OwnActionComplete(context);
    }

    /// <inheritdoc/>
    public void OpponentActionComplete(ThunderdomeContext context)
    {
        Modifiers.OpponentActionComplete(context);
        Weapons.OpponentActionComplete(context);
    }

    /// <inheritdoc/>
    public void TurnComplete(ThunderdomeContext context)
    {
        Modifiers.TurnComplete(context);
        Weapons.TurnComplete(context);
    }
}