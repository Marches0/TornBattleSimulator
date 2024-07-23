using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Chance;
using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Attacks;

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