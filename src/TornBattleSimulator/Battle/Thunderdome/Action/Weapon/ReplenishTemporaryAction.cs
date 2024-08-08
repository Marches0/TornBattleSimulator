using TornBattleSimulator.BonusModifiers.Ammo;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class ReplenishTemporaryAction : IAction
{
    public List<ThunderdomeEvent> PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        if (active.Weapons.Temporary != null)
        {
            active.Weapons.Temporary.Ammo.MagazineAmmoRemaining = 1;
        }

        // todo: get the correct weapon? maybe inject into IAction.
        var storage = active.Weapons.Melee.Modifiers.Active.OfType<StorageModifier>().First();
        storage.Consumed = true;

        return [context.CreateEvent(active, ThunderdomeEventType.ReplenishTemporary, new ReplenishedTemporaryData())];
    }
}