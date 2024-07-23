﻿using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Battle.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public interface IWeaponUsage
{
    public List<ThunderdomeEvent> UseWeapon(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon);
}