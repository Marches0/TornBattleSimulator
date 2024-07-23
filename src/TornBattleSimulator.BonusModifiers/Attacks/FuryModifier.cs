using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Attacks;

public class FuryModifier : IAttacksModifier
{
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Fury;

    public List<ThunderdomeEvent> MakeAttack(ThunderdomeContext context, PlayerContext active, PlayerContext other, WeaponContext weapon, Func<List<ThunderdomeEvent>> attackAction)
    {
        // Just a single attack.
        // Not supposed to be on loaded weapons,
        return weapon.RequiresReload
            ? new List<ThunderdomeEvent>(0)
            : attackAction();
    }
}