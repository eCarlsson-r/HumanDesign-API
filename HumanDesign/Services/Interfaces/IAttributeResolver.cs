public interface IAttributeResolver
{
    Task<AttributeDetail?> GetAsync(string property, string value);
}