namespace TornBattleSimulator.Mapper;

public static class EnumBuilder
{
    public static T GetAsEnum<T>(string name) where T : struct, Enum
    {
        return Enum.GetValues<T>()
            .First(t => t.ToString().Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }
}