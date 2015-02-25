
namespace NISOCountries.Core.ValueNormalizers
{
    public interface IValueNormalizer<T>
        where T : IISORecord
    {
        T Normalize(T value);
    }
}
