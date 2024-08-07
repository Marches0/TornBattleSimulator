using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Events.Data;
public interface IEventData
{
    string Format();
}

public class UsedTemporaryEvent : IEventData
{
    private readonly TemporaryWeaponType _temporary;

    public UsedTemporaryEvent(TemporaryWeaponType temporary)
    {
        _temporary = temporary;
    }

    public string Format() => $"Used {_temporary}";
}

public class AttackHitEvent : IEventData
{
    public AttackHitEvent(WeaponType weapon, AttackResult attack)
    {
        Weapon = weapon;
        Damage = attack.Damage.DamageDealt;
        BodyPart = attack.Damage.BodyPart;
        Flags = attack.Damage.Flags;
        HitChance = attack.HitChance;
    }

    public WeaponType Weapon { get; }

    public int Damage { get; }

    public BodyPart BodyPart { get; }

    public DamageFlags Flags { get; }

    public double HitChance { get; }

    public string Format()
    {
        return $"{Damage.ToString("N0").ToColouredString("#ffee8c")} @ {HitChance:P1} dealt by {Weapon} on {BodyPart} ({Flags})";
    }
}

public class AttackMissedEvent : IEventData
{
    public AttackMissedEvent(WeaponType weapon, double hitChance)
    {
        Weapon = weapon;
        HitChance = hitChance;
    }

    public WeaponType Weapon { get; }

    public double HitChance { get; }

    public string Format()
    {
        return $"{Weapon} @ {HitChance:P1} missed";
    }
}

public class ReloadEvent : IEventData
{
    public WeaponType Weapon { get; }

    public ReloadEvent(WeaponType weapon)
    {
        Weapon = weapon;
    }

    public string Format()
    {
        return $"Reload {Weapon}";
    }
}

public class EffectBeginEvent : IEventData
{
    public ModifierType ModifierType { get; }

    public EffectBeginEvent(ModifierType modifierType)
    {
        ModifierType = modifierType;
    }

    public string Format()
    {
        return $"{ModifierType.ToString().ToColouredString("#c49bdd")} started";
    }
}

public class EffectEndEvent : IEventData
{
    public ModifierType ModifierType { get; }

    public EffectEndEvent(ModifierType modifierType)
    {
        ModifierType = modifierType;
    }

    public string Format()
    {
        return $"{ModifierType.ToString().ToColouredString("#c49bdd")} ended";
    }
}

public class DamageOverTimeEvent : IEventData
{
    public int Damage { get; }

    public ModifierType Source { get; }

    public DamageOverTimeEvent(int damage, ModifierType source)
    {
        Damage = damage;
        Source = source;
    }

    public string Format()
    {
        return $"{Damage.ToString("N0").ToColouredString("#ffee8c")} from {Source.ToString().ToColouredString("#c49bdd")}";
    }
}

public class HealEvent : IEventData
{
    public int Heal { get; }

    public ModifierType Source { get; }

    public HealEvent(int heal, ModifierType source)
    {
        Heal = heal;
        Source = source;
    }

    public string Format()
    {
        return $"{Heal.ToString("N0").ToColouredString("#c49bdd")} heal from {Source.ToString().ToColouredString("#c49bdd")}";
    }
}

public class ExtraDamageEvent : IEventData
{
    public int Damage { get; }

    public ModifierType Source { get; }

    public ExtraDamageEvent(int damage, ModifierType source)
    {
        Damage = damage;
        Source = source;
    }

    public string Format()
    {
        return $"{Damage.ToString("N0").ToColouredString("#c49bdd")} damage from {Source.ToString().ToColouredString("#c49bdd")}";
    }
}


public class FightBeginEvent : IEventData
{
    public FightBeginEvent()
    {

    }

    public string Format()
    {
        return "Fight begin".ToColouredString("#ffffff");
    }
}

public class FightEndEvent : IEventData
{
    public ThunderDomeResult Result { get; }

    public FightEndEvent(ThunderDomeResult result)
    {
        Result = result;
    }

    public string Format()
    {
        return $"{Result.ToString().ToColouredString("#ffffff")}";
    }
}

public class MissedTurn : IEventData
{
    public string Format()
    {
        return "Missed Turn".ToColouredString("#c49bdd");
    }
}

public class WeaponChargeData : IEventData
{
    public WeaponType WeaponType { get; }

    public WeaponChargeData(
        WeaponType weaponType)
    {
        WeaponType = weaponType;
    }

    public string Format()
    {
        return $"Charged {WeaponType}".ToColouredString("#c49bdd");
    }
}

public class DisarmedData : IEventData
{
    public WeaponType WeaponType { get; }

    public DisarmedData(
        WeaponType weaponType)
    {
        WeaponType = weaponType;
    }

    public string Format()
    {
        return $"Disarmed {WeaponType}".ToColouredString("#c49bdd");
    }
}