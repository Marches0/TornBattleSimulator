using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Extensions;

namespace TornBattleSimulator.Battle.Thunderdome.Events.Data;
public interface IEventData
{
    string Format();
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
        if (Weapon == WeaponType.Temporary)
        {
            return "Used Temporary";
        }

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