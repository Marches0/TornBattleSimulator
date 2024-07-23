using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Events;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;

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