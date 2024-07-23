using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Battle.Thunderdome.Events;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;

public class RageModifier : IAttacksModifier
{
    private readonly IChanceSource _chanceSource;

    public RageModifier(IChanceSource chanceSource)
    {
        _chanceSource = chanceSource;
    }

    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Rage;

    public List<ThunderdomeEvent> MakeAttack(ThunderdomeContext context, PlayerContext active, PlayerContext other, WeaponContext weapon, Func<List<ThunderdomeEvent>> attackAction)
    {
        List<ThunderdomeEvent> events = new();
        int bonusAttacks = _chanceSource.ChooseRange(2, 9);

        while (!weapon.RequiresReload && bonusAttacks-- > 0)
        {
            events.AddRange(attackAction());
        }

        return events;
    }
}