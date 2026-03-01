namespace HumanDesign.Domain.Enums;

public enum DesignSense
{
    Security = 1,
    Uncertainty = 2,
    Action = 3,
    Meditation = 4,
    Judgement = 5,
    Acceptance = 6
}

public static class DesignSenseExtensionMethods
{
    public static int ToNumber(this DesignSense e) => e switch
    {
        DesignSense.Security => 1,
        DesignSense.Uncertainty => 2,
        DesignSense.Action => 3,
        DesignSense.Meditation => 4,
        DesignSense.Judgement => 5,
        DesignSense.Acceptance => 6,
        _ => throw new NotImplementedException()
    };
}