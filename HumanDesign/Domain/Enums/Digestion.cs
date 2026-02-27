namespace HumanDesign.Domain.Enums;

public enum Digestion
{
    Appetite = 1,
    Taste = 2,
    Thirst = 3,
    Touch = 4,
    Sound = 5,
    Light = 6
}

public static class DigestionExtensionMethods
{
    public static int ToNumber(this Digestion e) => e switch
    {
        Digestion.Appetite => 1,
        Digestion.Taste => 2,
        Digestion.Thirst => 3,
        Digestion.Touch => 4,
        Digestion.Sound => 5,
        Digestion.Light => 6,
        _ => throw new NotImplementedException()
    };
}