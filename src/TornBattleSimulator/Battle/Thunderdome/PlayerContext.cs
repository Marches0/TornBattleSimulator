using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Thunderdome.Player;
using TornBattleSimulator.Battle.Thunderdome.Stats.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome;

/// <summary>
///  A build taking part in a battle.
/// </summary>
public class PlayerContext
{
    public PlayerContext(BattleBuild build)
    {
        Build = build;
        PrimaryAmmo = build.Primary != null ? new CurrentAmmo()
        {
            Magazines = build.Primary.Ammo.Magazines,
            MagazinesRemaining = build.Primary.Ammo.Magazines,

            MagazineSize = build.Primary.Ammo.MagazineSize,
            MagazineAmmoRemaining = build.Primary.Ammo.MagazineSize
        }: null;

        SecondaryAmmo = build.Secondary != null ? new CurrentAmmo()
        {
            Magazines = build.Secondary.Ammo.Magazines,
            MagazinesRemaining = build.Secondary.Ammo.Magazines,

            MagazineSize = build.Secondary.Ammo.MagazineSize,
            MagazineAmmoRemaining = build.Secondary.Ammo.MagazineSize
        } : null;

        Health = (int)build.Health;

        _currentTickStats = new Lazy<BattleStats>(GetCurrentStats);
    }

    /// <summary>
    ///  The build taking part.
    /// </summary>
    public BattleBuild Build { get; }

    /// <summary>
    ///  Stat modifiers currently applied to the build.
    /// </summary>
    public List<IStatsModifier> StatModifiers { get; private set; } = new List<IStatsModifier>();

    public int Health { get; set; }

    public CurrentAmmo? PrimaryAmmo { get; set; }

    public CurrentAmmo? SecondaryAmmo { get; set; }

    /// <summary>
    ///  The build's current stats.
    /// </summary>
    public BattleStats Stats => _currentTickStats.Value;

    /// <summary>
    ///  The action being taken by this player in the current tick.
    /// </summary>
    public BattleAction CurrentAction { get; set; }

    public void Tick(ThunderdomeContext context)
    {
        // Clear the stats every tick, so we can reevaluate modifiers.
        _currentTickStats = new Lazy<BattleStats>(GetCurrentStats);
        CurrentAction = 0;

        foreach (IStatsModifier modifier in StatModifiers)
        {
            modifier.TimeRemainingSeconds -= context.AttackInterval;

            if (modifier.TimeRemainingSeconds <= 0)
            {
                // track
            }
        }

        StatModifiers = StatModifiers
            .Where(m => m.TimeRemainingSeconds > 0)
            .ToList();
    }

    private Lazy<BattleStats> _currentTickStats;

    private BattleStats GetCurrentStats()
    {
        BattleStats baseStats = new()
        {
            Strength = Build.BattleStats.Strength,
            Defence = Build.BattleStats.Defence,
            Speed = Build.BattleStats.Speed,
            Dexterity = Build.BattleStats.Dexterity,
        };

        return StatModifiers
            .Aggregate(baseStats, (stats, modifier) => stats.Apply(modifier));
    }
}