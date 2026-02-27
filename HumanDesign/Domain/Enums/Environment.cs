namespace HumanDesign.Domain.Enums;

public enum Environment
{
    Caves = 1,
    Markets = 2,
    Kitchens = 3,
    Mountains = 4,
    Valleys = 5,
    Shores = 6
}

public static class EnvironmentExtensionMethods
{
    public static int ToNumber(this Environment e) => e switch
    {
        Environment.Caves => 1,
        Environment.Markets => 2,
        Environment.Kitchens => 3,
        Environment.Mountains => 4,
        Environment.Valleys => 5,
        Environment.Shores => 6,
        _ => throw new NotImplementedException()
    };
}