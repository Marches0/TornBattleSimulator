﻿namespace TornBattleSimulator.Input.Build;

public class StrategyInput
{
    public string? Weapon { get; set; }
    public bool? Reload { get; set; }
    public List<StrategyUntilInput>? Until { get; set; }
}

public class StrategyUntilInput
{
    public string? Condition { get; set; }
    public string? StatusName { get; set; }
    public int? Count { get; set; }
}