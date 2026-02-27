namespace HumanDesign.Domain.Enums;

public enum Perspective
{
    Survival = 1,
    Possibility = 2,
    Power = 3,
    Personal = 4,
    Probability = 5,
    Wanting = 6
}

public static class PerspectiveExtensionMethods
{
    public static int ToNumber(this Perspective p) => p switch
    {
        Perspective.Survival => 1,
        Perspective.Possibility => 2,
        Perspective.Power => 3,
        Perspective.Personal => 4,
        Perspective.Probability => 5,
        Perspective.Wanting => 6,
        _ => throw new NotImplementedException()
    };
}