namespace HumanDesign.Domain.Enums;

public enum Motivation
{
    Fear = 1,
    Hope = 2,
    Desire = 3,
    Need = 4,
    Guilt = 5,
    Innocence = 6
}

public static class MotivationExtensionMethods
{
    public static int ToNumber(this Motivation m) => m switch
    {
        Motivation.Fear => 1,
        Motivation.Hope => 2,
        Motivation.Desire => 3,
        Motivation.Need => 4,
        Motivation.Guilt => 5,
        Motivation.Innocence => 6,
        _ => throw new NotImplementedException()
    };
}