namespace HumanDesign.Domain.Enums;

public enum Sense
{
    Smell = 1,
    Taste = 2,
    OuterVision = 3,
    InnerVision = 4,
    Feeling = 5,
    Touch = 6
}

public static class SenseExtensionMethods
{
    public static int ToNumber(this Sense e) => e switch
    {
        Sense.Smell => 1,
        Sense.Taste => 2,
        Sense.OuterVision => 3,
        Sense.InnerVision => 4,
        Sense.Feeling => 5,
        Sense.Touch => 6,
        _ => throw new NotImplementedException()
    };

    public static string ToString(this Sense e) => e switch
    {
        Sense.Smell => "Smell",
        Sense.Taste => "Taste",
        Sense.OuterVision => "Outer Vision",
        Sense.InnerVision => "Inner Vision",
        Sense.Feeling => "Feeling",
        Sense.Touch => "Touch",
        _ => throw new NotImplementedException()
    };
}