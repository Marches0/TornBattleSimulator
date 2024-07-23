using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Extensions;
using TornBattleSimulator.Shared.Thunderdome.Damage;
using TornBattleSimulator.Shared.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Shared.Thunderdome.Events.Data;
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
    public AttackHitEvent(WeaponType weapon, int damage, BodyPart bodyPart, DamageFlags flags, double hitChance)
    {
        Weapon = weapon;
        Damage = damage;
        BodyPart = bodyPart;
        Flags = flags;
        HitChance = hitChance;
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
        return $"{Weapon}";
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

public class StunnedData : IEventData
{
    public string Format()
    {
        return "Stunned".ToColouredString("#c49bdd");
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