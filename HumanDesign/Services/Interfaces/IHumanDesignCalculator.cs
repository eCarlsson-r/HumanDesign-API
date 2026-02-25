public interface IHumanDesignCalculator
{
    Task <DesignResult> CalculateAsync(BirthDataRequest request);
}