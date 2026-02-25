public interface IAstrologyProvider
{
    Task<IEnumerable<PlanetaryActivation>> GetActivationsAsync(BirthDataRequest request);
}