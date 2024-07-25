using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

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

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;

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