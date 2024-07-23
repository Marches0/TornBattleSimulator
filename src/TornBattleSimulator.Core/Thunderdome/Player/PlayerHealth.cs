namespace TornBattleSimulator.Core.Thunderdome.Player;

public class PlayerHealth
{
    public PlayerHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public int CurrentHealth { get; set; }
    public int MaxHealth { get; }
}