namespace HumanDesign.Domain.Enums;

public enum Digestion
{
    OneLeft = 11,
    OneRight = 15,
    TwoLeft = 21,
    TwoRight = 25,
    ThreeLeft = 31,
    ThreeRight = 35,
    FourLeft = 41,
    FourRight = 45,
    FiveLeft = 51,
    FiveRight = 55,
    SixLeft = 61,
    SixRight = 65
}

public static class DigestionExtensionMethods
{
    public static int ToNumber(this Digestion e) => e switch
    {
        Digestion.OneLeft => 11,
        Digestion.OneRight => 15,
        Digestion.TwoLeft => 21,
        Digestion.TwoRight => 25,
        Digestion.ThreeLeft => 31,
        Digestion.ThreeRight => 35,
        Digestion.FourLeft => 41,
        Digestion.FourRight => 45,
        Digestion.FiveLeft => 51,
        Digestion.FiveRight => 55,
        Digestion.SixLeft => 61,
        Digestion.SixRight => 65,
        _ => throw new NotImplementedException()
    };

    public static string ToValue(this Digestion e) => e switch
    {
        Digestion.OneLeft => "Appetite (Consecutive)",
        Digestion.OneRight => "Appetite (Alternating)",
        Digestion.TwoLeft => "Taste (Open)",
        Digestion.TwoRight => "Taste (Closed)",
        Digestion.ThreeLeft => "Thirst (Hot)",
        Digestion.ThreeRight => "Thirst (Cold)",
        Digestion.FourLeft => "Touch (Calm)",
        Digestion.FourRight => "Touch (Nervous)",
        Digestion.FiveLeft => "Sound (High)",
        Digestion.FiveRight => "Sound (Low)",
        Digestion.SixLeft => "Light (Direct)",
        Digestion.SixRight => "Light (InDirect)",
        _ => throw new NotImplementedException()
    };
}